using UnityEngine;

public class TetrisStick : MonoBehaviour
{
    public Vector3Int[] cells = new Vector3Int[4];
    public int rotationState = 0;
    protected Vector3Int position = new Vector3Int(0, 9, 0); // Vị trí khởi đầu
    private float fallTimer = 0f;
    private float fallInterval = 1f; // Thời gian rơi tự động
    private bool isControl = true;
    private GameObject[] blocks = new GameObject[4];
    public GameObject blockPrefab; // Prefab khối lập phương 3D
    public GridManager grid;

    private static readonly Vector3Int[,] rotationOffsets = new Vector3Int[2, 4]
    {
        { new Vector3Int(0, 1, 0), new Vector3Int(1, 1, 0), new Vector3Int(2, 1, 0), new Vector3Int(3, 1, 0) }, // Horizontal
        { new Vector3Int(2, 0, 0), new Vector3Int(2, 1, 0), new Vector3Int(2, 2, 0), new Vector3Int(2, 3, 0) }  // Vertical
    };

    private void Start()
    {
        SetCells(rotationState);
        CreateVisuals();
    }

    private void Update()
    {
        if (!this.isControl) return;
        HandleInput();
        HandleAutoFall();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) Move(Vector3Int.left);
        if (Input.GetKeyDown(KeyCode.RightArrow)) Move(Vector3Int.right);
        if (Input.GetKeyDown(KeyCode.UpArrow)) Rotate();
        if (Input.GetKeyDown(KeyCode.DownArrow)) Move(Vector3Int.down);
    }

    private void HandleAutoFall()
    {
        fallTimer += Time.deltaTime;
        if (fallTimer >= fallInterval)
        {
            fallTimer = 0f;
            Move(Vector3Int.down);
        }
    }

    protected virtual void SetCells(int state)
    {
        for (int i = 0; i < 4; i++)
        {
            cells[i] = rotationOffsets[state, i] + position;
        }
    }

    private void CreateVisuals()
    {
        for (int i = 0; i < 4; i++)
        {
            blocks[i] = Instantiate(blockPrefab, transform);
            blocks[i].gameObject.SetActive(true);
        }
        UpdateVisuals();
    }

    protected void UpdateVisuals()
    {
        for (int i = 0; i < 4; i++)
        {
            blocks[i].transform.position = new Vector3(cells[i].x, cells[i].y, cells[i].z);
        }
    }

    public virtual void Rotate()
    {
        int newState = (rotationState + 1) % 2;
        SetCells(newState);
        rotationState = newState;
        UpdateVisuals();
    }

    public void Move(Vector3Int direction)
    {
        Vector3Int newPosition = position + direction;
        if (IsValidPosition(newPosition))
        {
            position = newPosition;
            SetCells(rotationState);
            UpdateVisuals();
        }
        else if (direction == Vector3Int.down)
        {
            PlaceOnGrid();
        }
    }

    private bool IsValidPosition(Vector3Int newPosition)
    {
        foreach (var cell in cells)
        {
            Vector3Int checkPos = cell + newPosition - position;
            if (!grid.IsInsideGrid(checkPos))
            {
                return false;
            }
            if (checkPos.x >= 0 && checkPos.x < grid.With && checkPos.y >= 0 && checkPos.y < grid.Height && checkPos.z >= 0 && checkPos.z < grid.Depth)
            {
                if (grid.grid[checkPos.x, checkPos.x, checkPos.x] != null)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void PlaceOnGrid()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i].x >= 0 && cells[i].x < grid.With && cells[i].y >= 0 && cells[i].y < grid.Height && cells[i].z >= 0 && cells[i].z < grid.Depth)
            {
                grid.PlaceBlock(blocks[i].transform);
            }
        }
        grid.ClearFullRows();
        this.isControl = false;
        TetrisSpawner tetrisSpawner = FindFirstObjectByType<TetrisSpawner>();
        tetrisSpawner.SpawnNewBlock();
    }
}






