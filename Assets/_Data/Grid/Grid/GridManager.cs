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
    protected override void Start()
    {
        base.Start();
        this.DebugGrid();
    }
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
                    gridRows[y].row[x].gameObject.SetActive(false);
                    gridRows[y].row[x].TertrominoesVisual.Despawn();
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
                gridRows[y].row[x].gameObject.SetActive(false);
                gridRows[y].row[x].TertrominoesVisual.Despawn();
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
        for (int y = startY; y < 22; y++)
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
    public bool IsValidPosition(TetrominoCtrl tetromino, Vector3Int spawnPosition)
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
}
