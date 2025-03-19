using UnityEngine;

public class TetrisStick : MonoBehaviour
{
    public Vector3Int[] cells = new Vector3Int[4];
    private int rotationState = 0;
    protected Vector3Int position = new Vector3Int(0, 9, 0); // Vị trí khởi đầu
    private float fallTimer = 0f;
    private float fallInterval = 1f; // Thời gian rơi tự động
    private bool isControl = true;
    private GameObject[] blocks = new GameObject[4];
    public GameObject blockPrefab; // Prefab khối lập phương 3D

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

    private void UpdateVisuals()
    {
        for (int i = 0; i < 4; i++)
        {
            blocks[i].transform.position = new Vector3(cells[i].x, cells[i].y, cells[i].z);
        }
    }

    public void Rotate()
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
            if (!TetrisGrid.IsInsideGrid(checkPos))
            {
                return false;
            }

            int gridX = checkPos.x + TetrisGrid.width / 2;
            int gridY = checkPos.y + TetrisGrid.height / 2;
            int gridZ = checkPos.z;

            if (gridX >= 0 && gridX < TetrisGrid.width && gridY >= 0 && gridY < TetrisGrid.height && gridZ >= 0 && gridZ < TetrisGrid.depth)
            {
                if (TetrisGrid.grid[gridX, gridY, gridZ] != null)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void PlaceOnGrid()
    {
        foreach (var cell in cells)
        {
            int gridX = cell.x + TetrisGrid.width / 2;
            int gridY = cell.y + TetrisGrid.height / 2;
            int gridZ = cell.z;
            if (gridX >= 0 && gridX < TetrisGrid.width && gridY >= 0 && gridY < TetrisGrid.height && gridZ >= 0 && gridZ < TetrisGrid.depth)
            {
                TetrisGrid.grid[gridX, gridY, gridZ] = blocks[0].transform;
            }
        }
        TetrisGrid.ClearFullRows();
        this.isControl = false;
        TetrisSpawner tetrisSpawner = FindFirstObjectByType<TetrisSpawner>();
        tetrisSpawner.SpawnNewBlock();
    }
}




