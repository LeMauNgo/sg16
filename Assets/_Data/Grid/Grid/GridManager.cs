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
    public Vector3 GridToWorldPosition(Vector3Int gridPos)
    {
        return new Vector3(
            gridPos.x,
            gridPos.y,
            0f // hoặc z nếu bạn dùng 3D
        );
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

        this.PlaceBlock(pos, block);
    }
    protected virtual void PlaceBlock(Vector3Int pos, CubeTetrominoes block)
    {
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
    public void ClearBlock(Vector3Int pos)
    {
        if (pos.x < 0 || pos.x >= width || pos.y < 0 || pos.y >= height) return;
        if (gridRows[pos.y].row[pos.x] != null)
        {
            gridRows[pos.y].row[pos.x].Despawn.DoDespawn();
            gridRows[pos.y].row[pos.x] = null;
        }
    }
    public void ClearRow(int y)
    {
        if (y < 0 || y >= height) return;

        for (int x = 0; x < width; x++)
        {
            if (x >= 0 && x < width && gridRows[y].row[x] != null)
            {
                ChestCtrl chestCtrl = gridRows[y].row[x] as ChestCtrl;
                if (gridRows[y].row[x] == null) continue;
                if (chestCtrl != null) continue;
                gridRows[y].row[x].Despawn.DoDespawn();
                gridRows[y].row[x] = null;
            }
        }

        this.OpenChestsInRow(y); // Mở rương trong hàng này
    }
    protected virtual void OpenChestsInRow(int y)
    {
        // Kiểm tra nếu chỉ số hàng nằm ngoài giới hạn
        if (y < 0 || y >= height) return;

        // Duyệt qua từng ô trong hàng
        for (int x = 0; x < width; x++)
        {
            // Lấy đối tượng ChestCtrl tại ô hiện tại
            if (gridRows[y].row[x] is ChestCtrl chestCtrl)
            {
                // Mở rương
                chestCtrl.TryOpenChest();
            }
        }
    }
    public void MoveRowDown(int y, int startY)
    {
        if (y < 0 || y >= height) return;

        for (int x = 0; x < width; x++)
        {
            // Nếu cột này có chứa rương, bỏ qua (không cho rơi block phía trên)
            //if (this.IsColumnContainingChest(x, startY)) continue;

            // Nếu có block ở [y][x]
            if (gridRows[y].row[x] != null)
            {
                // Kiểm tra vị trí dưới có trống không
                if (gridRows[y - 1].row[x] == null)
                {
                    gridRows[y - 1].row[x] = gridRows[y].row[x];
                    gridRows[y - 1].row[x].SetMoveDownPosition(new Vector3Int(x, y - 1, 0)); // cập nhật vị trí
                    gridRows[y].row[x] = null;
                }
            }
        }
    }
    public void MoveAllRowsDown(int startY)
    {
        for (int y = startY; y < height; y++)
        {
            MoveRowDown(y, startY);
            //Debug.Log(startY);
        }
    }
    protected virtual bool IsColumnContainingChest(int x, int Row)
    {
        if (x < 0 || x >= width) return false;
        for (int y = 0; y < height; y++)
        {
            if (gridRows[y].row[x] is ChestCtrl)
            {
                return true; 
            }
        }
        return false; 
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
    public bool IsOccupied(Vector3Int gridPos)
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
        int chestCount = levelData.chestCount;

        List<Vector3Int> chestPositions = new List<Vector3Int>();

        // 1. Spawn chest trước
        for (int i = 0; i < chestCount; i++)
        {
            Vector3Int chestPos = new Vector3Int(Random.Range(1, width - 2), 3 + i*5, 0);
            chestPositions.Add(chestPos);
            SpawnChest(chestPos);
        }

        // 2. Spawn garbage rows
        for (int y = 0; y < rows; y++)
        {
            int holeX = Random.Range(1, width - 1);

            // Nếu holeX trùng vị trí bất kỳ rương nào → random lại
            while (IsHoleOnAnyChest(holeX, y + rowStartY, chestPositions))
            {
                holeX = Random.Range(1, width - 1);
            }

            for (int x = 0; x < width; x++)
            {
                if (x == holeX) continue;

                // Nếu là ô trùng với bất kỳ rương nào → bỏ qua
                if (IsBlockOnAnyChest(x, y + rowStartY, chestPositions))
                    continue;

                SpawnBlock(new Vector3Int(x, y + rowStartY, 0));
            }
        }
    }
    private bool IsHoleOnAnyChest(int holeX, int y, List<Vector3Int> chestPositions)
    {
        foreach (var chest in chestPositions)
        {
            // Rương chiếm 2x2 → check nếu holeX nằm trong khoảng x của rương, và y trùng
            if (holeX == chest.x || holeX == chest.x + 1)
            {
                if (y == chest.y || y == chest.y + 1)
                    return true;
            }
        }
        return false;
    }

    private bool IsBlockOnAnyChest(int x, int y, List<Vector3Int> chestPositions)
    {
        foreach (var chest in chestPositions)
        {
            if ((x == chest.x || x == chest.x + 1) && (y == chest.y || y == chest.y + 1))
                return true;
        }
        return false;
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
        ChestCtrl chestCtrl = this.SpawnChestPrefabs(bottomLeft);
        foreach (var pos in chestBlocks)
        {
            //this.SpawnBlock(pos);
            this.PlaceBlock(pos,chestCtrl);
        }
    }
    protected virtual ChestCtrl SpawnChestPrefabs(Vector3Int pos)
    {
        CubeCtrl effectPrefabs = CubeSpawnerManager.Instance.Spanwer.PoolPrefabs.GetByName(CubeCode.Chest.ToString());
        CubeCtrl effectCtrl = CubeSpawnerManager.Instance.Spanwer.Spawn(effectPrefabs, pos);
        ChestCtrl chestCtrl = effectCtrl as ChestCtrl;
        chestCtrl.SetBottomLeftPosition(pos);
        effectCtrl.gameObject.SetActive(true);
        return chestCtrl;
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
