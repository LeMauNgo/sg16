using UnityEngine;
using System.Collections.Generic;

public class TetrisGrid : MonoBehaviour
{
    public int width = 11; // -5 đến 5 (tổng 11 ô)
    public int height = 21; // -10 đến 10 (tổng 21 ô)
    public int depth = 1; // Trục Z chỉ có 1

    [System.Serializable]
    public class RowData
    {
        public List<Transform> row = new List<Transform>();
    }

    [SerializeField] private List<RowData> gridRows = new List<RowData>();
    public Transform[,,] grid;

    private int xOffset = 5; // Để dịch chuyển chỉ mục index trong mảng
    private int yOffset = 10; // Dịch chuyển theo trục Y

    void Awake()
    {
        grid = new Transform[width, height, depth];
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
    // Kiểm tra xem một vị trí có nằm trong lưới hay không
    public bool IsInsideGrid(Vector3Int pos)
    {
        int gridX = pos.x + xOffset;
        int gridY = pos.y + yOffset;
        return gridX >= 0 && gridX < width && gridY >= 0 && gridY < height && pos.z == 0;
    }

    // Đặt một khối vào lưới
    public void PlaceBlock(Transform block)
    {
        Vector3Int pos = Vector3Int.FloorToInt(block.position);
        int gridX = pos.x + xOffset;
        int gridY = pos.y + yOffset;

        if (gridX >= 0 && gridX < width && gridY >= 0 && gridY < height)
        {
            grid[gridX, gridY, 0] = block;
            gridRows[gridY].row[gridX] = block; // Cập nhật danh sách để hiển thị trong Inspector
            Debug.Log(gridX + "--------"+  gridY);
        }
    }

    // Kiểm tra xem một hàng có đầy không
    public bool IsRowFull(int y)
    {
        int gridY = y + yOffset;
        if (gridY < 0 || gridY >= height) return false;

        for (int x = -5; x <= 5; x++)
        {
            int gridX = x + xOffset;
            if (gridX < 0 || gridX >= width || grid[gridX, gridY, 0] == null)
                return false;
        }
        return true;
    }

    // Xóa một hàng
    public void ClearRow(int y)
    {
        int gridY = y + yOffset;
        if (gridY < 0 || gridY >= height) return;

        for (int x = -5; x <= 5; x++)
        {
            int gridX = x + xOffset;
            if (gridX >= 0 && gridX < width && grid[gridX, gridY, 0] != null)
            {
                Destroy(grid[gridX, gridY, 0].gameObject);
                grid[gridX, gridY, 0] = null;
                gridRows[gridY].row[gridX] = null; // Cập nhật danh sách hiển thị
            }
        }
    }

    public void MoveRowDown(int y)
    {
        int gridY = y + yOffset;
        if (gridY <= 0 || gridY >= height) return;

        for (int x = -5; x <= 5; x++)
        {
            int gridX = x + xOffset;
            if (gridX >= 0 && gridX < width && grid[gridX, gridY, 0] != null)
            {
                grid[gridX, gridY - 1, 0] = grid[gridX, gridY, 0];
                grid[gridX, gridY, 0].position += Vector3.down;
                grid[gridX, gridY, 0] = null;
                gridRows[gridY - 1].row[gridX] = gridRows[gridY].row[gridX];
                gridRows[gridY].row[gridX] = null;
            }
        }
    }

    public void MoveAllRowsDown(int startY)
    {
        for (int y = startY; y <= 10; y++)
        {
            MoveRowDown(y);
        }
    }

    public void ClearFullRows()
    {
        for (int y = -10; y <= 10; y++)
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
