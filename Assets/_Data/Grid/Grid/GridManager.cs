using System.Collections.Generic;
using UnityEngine;

public class GridManager : SaiSingleton<GridManager>
{
    [SerializeField] protected int width = 20;
    public int With => width;
    [SerializeField] protected int height = 22; 
    public int Height => height;
    [SerializeField] protected int depth = 1; 
    public int Depth => depth;
    [SerializeField] private List<RowData> gridRows = new List<RowData>();
    public List<RowData> GridRows => gridRows;
    protected virtual void DebugGrid()
    {
        for (int y = 0; y < height; y++)
        {
            RowData newRow = new RowData();
            for (int x = 0; x < width; x++)
            {
                newRow.row.Add(null);
            }
            gridRows.Add(newRow);
        }
    }
    public virtual void GenerateGrid(int gridWidth, int gridHeight)
    {
        this.width = gridWidth;
        this.height = gridHeight;
        this.DebugGrid();
        GameManager.Instance.StartGame();
        this.SpawnWalls(gridWidth, gridHeight);

    }
    public bool IsInsideGrid(Vector3Int pos)
    {
        return pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height && pos.z == 0;
    }

    public void PlaceBlock(CubeTetrominoes block)
    {
        Vector3Int pos = Vector3Int.FloorToInt(block.transform.position);

        if (pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height)
        {
            gridRows[pos.y].row[pos.x] = block;
        }
    }

    public bool IsRowFull(int y)
    {
        if (y < 0 || y >= height) return false;

        for (int x = 0; x < width; x++)
        {
            if (x < 0 || x >= width || gridRows[y].row[x] == null)
                return false;
        }
        return true;
    }
    public virtual void ClearGrid()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (gridRows[y].row[x] != null)
                {
                    gridRows[y].row[x].Despawn.DoDespawn();
                    gridRows[y].row[x] = null;
                }
            }
        }
    }
    public void ClearRow(int y)
    {
        if (y < 0 || y >= height) return;

        for (int x = 0; x < width; x++)
        {
            if (x >= 0 && x < width && gridRows[y].row[x] != null)
            {
                gridRows[y].row[x].Despawn.DoDespawn();
                gridRows[y].row[x] = null;
            }
        }
    }

    public void MoveRowDown(int y)
    {
        if (y < 0 || y >= height) return;

        for (int x = 0; x < width; x++)
        {
            if (x >= 0 && x < width && gridRows[y].row[x] != null)
            {
                gridRows[y - 1].row[x] = gridRows[y].row[x];
                gridRows[y].row[x].transform.position += Vector3.down;
                gridRows[y].row[x] = null;
            }
        }
    }
    public void MoveAllRowsDown(int startY)
    {
        for (int y = startY; y < height; y++)
        {
            MoveRowDown(y);
        }
    }

    public void ClearFullRows()
    {
        int rowCount = 0;
        for (int y = 0; y < height; y++)
        {
            if (IsRowFull(y))
            {
                rowCount++;
                ClearRow(y);
                MoveAllRowsDown(y + 1);
                y--;
            }
        }
        ScoreManager.Instance.AddScore(rowCount);
    }
    protected bool IsOccupied(Vector3Int gridPos)
    {
        if (gridPos.x < 0 || gridPos.x >= width || gridPos.y < 0 || gridPos.y >= height)
            return true;

        return gridRows[gridPos.y].row[gridPos.x] != null;
    }
    public bool IsValidPosition(TetrominoPlayer tetromino, Vector3Int spawnPosition)
    {
        foreach (CubeTetrominoes block in tetromino.TertrominoesVisual.Cubes)
        {
            Vector3Int gridPos = Vector3Int.RoundToInt(block.transform.position);
            Debug.Log(gridPos);
            if (!IsInsideGrid(gridPos) || IsOccupied(gridPos))
            {
                return false;
            }
        }
        return true;
    }
    private void SpawnWalls(int gridWidth, int gridHeight)
    {
        for (int y = 0; y < gridHeight; y++)
        {
            SpawnWallBlock(new Vector3Int(-1, y));
            SpawnWallBlock(new Vector3Int(gridWidth, y));
        }

        for (int x = -1; x <= gridWidth; x++)
        {
            SpawnWallBlock(new Vector3Int(x, -1));
        }
    }

    private void SpawnWallBlock(Vector3Int pos)
    {
        CubeCtrl effectPrefabs = CubeSpawnerManager.Instance.Spanwer.PoolPrefabs.GetByName(CubeCode.Wall.ToString());
        CubeCtrl effectCtrl = CubeSpawnerManager.Instance.Spanwer.Spawn(effectPrefabs, pos);
        effectCtrl.gameObject.SetActive(true);
    }

    private void SpawnBlock(Vector3Int pos)
    {
        CubeCtrl cubeCtrl = CubeSpawnerManager.Instance.Spanwer.PoolPrefabs.GetByName(CubeCode.CubeTetrominoes.ToString());
        CubeCtrl cube = CubeSpawnerManager.Instance.Spanwer.Spawn(cubeCtrl, pos);
        CubeTetrominoes cubeTetrominoes = cube as CubeTetrominoes;
        this.PlaceBlock(cubeTetrominoes);
        cube.gameObject.SetActive(true);
    }
    public void SpawnChestAndGarbageRows(LevelData levelData)
    {
        int rows = levelData.garbageRowCount;
        int rowStartY = 0;

        // 1. Spawn chest trước
        Vector3Int chestPos = new Vector3Int(Random.Range(0, width - 1), 1); // Rương ở ngay trên đỉnh hàng rác
        SpawnChest(chestPos);

        // 2. Spawn garbage rows
        for (int y = 0; y < rows; y++)
        {
            int holeX = Random.Range(0, width);

            // Nếu holeX trùng với rương -> random lại
            while (IsHoleOnChest(holeX, chestPos))
            {
                holeX = Random.Range(0, width);
            }

            for (int x = 0; x < width; x++)
            {
                // Nếu là ô rương thì bỏ qua
                if (IsBlockOnChest(x, rowStartY + y, chestPos))
                    continue;

                // Nếu là lỗ thì bỏ qua
                if (x == holeX)
                    continue;

                Vector3Int pos = new Vector3Int(x, rowStartY + y);
                SpawnBlock(pos);
            }
        }
    }

    // Hàm spawn 4 khối rương 2x2
    private void SpawnChest(Vector3Int bottomLeft)
    {
        Vector3Int[] chestBlocks = new Vector3Int[]
        {
        bottomLeft,
        bottomLeft + Vector3Int.right,
        bottomLeft + Vector3Int.up,
        bottomLeft + Vector3Int.right + Vector3Int.up
        };
        this.SpawnChestPrefabs(bottomLeft);
        foreach (var pos in chestBlocks)
        {
            this.SpawnBlock(pos);
        }
    }
    protected virtual void SpawnChestPrefabs(Vector3Int pos)
    {
        CubeCtrl effectPrefabs = CubeSpawnerManager.Instance.Spanwer.PoolPrefabs.GetByName(CubeCode.Chest.ToString());
        CubeCtrl effectCtrl = CubeSpawnerManager.Instance.Spanwer.Spawn(effectPrefabs, pos);
        ChestCtrl chestCtrl = effectCtrl as ChestCtrl;
        chestCtrl.bottomLeftPosition = pos;
        effectCtrl.gameObject.SetActive(true);
    }
    // Kiểm tra nếu vị trí hole nằm đè lên ô rương
    private bool IsHoleOnChest(int holeX, Vector3Int chestPos)
    {
        return holeX == chestPos.x || holeX == chestPos.x + 1;
    }

    // Kiểm tra nếu block spawn trùng với vị trí rương
    private bool IsBlockOnChest(int x, int y, Vector3Int chestPos)
    {
        return (x == chestPos.x || x == chestPos.x + 1) &&
               (y == chestPos.y || y == chestPos.y + 1);
    }
}
