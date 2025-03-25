using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class GridManager : SaiBehaviour
{
    [SerializeField] protected int width = 10;
    public int With => width;
    [SerializeField] protected int height = 22; 
    public int Height => height;
    [SerializeField] protected int depth = 1; 
    public int Depth => depth;
    [SerializeField] private List<RowData> gridRows = new List<RowData>();
    public List<RowData> GridRows => gridRows;
    //public Transform[,,] grid;
    protected override void Start()
    {
        base.Start();
        this.DebugGrid();
    }
    protected virtual void DebugGrid()
    {
        //grid = new Transform[width, height, depth];
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
    public bool IsInsideGrid(Vector3Int pos)
    {
        return pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height && pos.z == 0;
    }

    public void PlaceBlock(Transform block)
    {
        Vector3Int pos = Vector3Int.FloorToInt(block.position);

        if (pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height)
        {
            gridRows[pos.y].row[pos.x] = block;
        }
    }

    public bool IsRowFull(int y)
    {
        if (y < 0 || y >= height) return false;

        for (int x = 0; x < 10; x++)
        {
            if (x < 0 || x >= width || gridRows[y].row[x] == null)
                return false;
        }
        return true;
    }

    public void ClearRow(int y)
    {
        if (y < 0 || y >= height) return;

        for (int x = 0; x < 10; x++)
        {
            if (x >= 0 && x < width && gridRows[y].row[x] != null)
            {
                gridRows[y].row[x].gameObject.SetActive(false);
                gridRows[y].row[x] = null;
                //gridRows[y].row[x] = null;
            }
        }
    }

    public void MoveRowDown(int y)
    {
        if (y < 0 || y >= height) return;

        for (int x = 0; x < 10; x++)
        {
            if (x >= 0 && x < width && gridRows[y].row[x] != null)
            {
                gridRows[y - 1].row[x] = gridRows[y].row[x];
                gridRows[y].row[x].transform.position += Vector3.down;
                gridRows[y].row[x] = null;
            }
        }
    }
    //protected 
    public void MoveAllRowsDown(int startY)
    {
        for (int y = startY; y < 22; y++)
        {
            MoveRowDown(y);
        }
    }

    public void ClearFullRows()
    {
        for (int y = -10; y < 10; y++)
        {
            if (IsRowFull(y))
            {
                ClearRow(y);
                MoveAllRowsDown(y + 1);
                y--;
            }
        }
    }
}
