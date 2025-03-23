using UnityEngine;
using UnityEngine.UIElements;

public class TetrominoMover : TertrominoesAbs
{
    [SerializeField] protected int rotationState = 0;
    public int RotationState => rotationState;
    [SerializeField] protected Vector3Int[] cells = new Vector3Int[4];
    public Vector3Int[] Cells => cells;
    protected Vector3Int position = new Vector3Int(0, 9, 0);
    private float fallTimer = 0f;
    private float fallInterval = 1f;
    private bool isControl = true;
    private void Update()
    {
        if (!isControl) return;
        this.HandleAutoFall();
        this.MoveDirection();
    }
    protected virtual void MoveDirection()
    {
        if(InputManager.Instance.IsLeft) this.Move(Vector3Int.left);
        if(InputManager.Instance.IsDown) this.Move(Vector3Int.down);
        if(InputManager.Instance.IsRight) this.Move(Vector3Int.right);
        if(InputManager.Instance.IsRotat) this.Move(Vector3Int.left);
    }
    protected virtual void SetCells(int state)
    {
        for (int i = 0; i < 4; i++)
        {
            cells[i] = this.tetrominoCtrl.RotationOffsets[state, i] + position;
        }
    }
    protected virtual void HandleAutoFall()
    {
        fallTimer += Time.deltaTime;
        if (fallTimer >= fallInterval)
        {
            fallTimer = 0f;
            Move(Vector3Int.down);
        }
    }
    protected virtual void Rotate()
    {
        int newState = (rotationState + 1) % 2;
        this.SetCells(newState);
        rotationState = newState;
    }
    protected virtual void Move(Vector3Int direction)
    {
        Vector3Int newPosition = position + direction;
        if (IsValidPosition(newPosition))
        {
            position = newPosition;
        }
        else if (direction == Vector3Int.down)
        {
            PlaceOnGrid();
        }
    }

    protected virtual bool IsValidPosition(Vector3Int newPosition)
    {
        foreach (var cell in cells)
        {
            Vector3Int checkPos = cell + newPosition - position;
            if (!tetrominoCtrl.GridManager.IsInsideGrid(checkPos))
            {
                return false;
            }
            if (checkPos.x >= 0 && checkPos.x < tetrominoCtrl.GridManager.With && checkPos.y >= 0 && checkPos.y < tetrominoCtrl.GridManager.Height && checkPos.z >= 0 && checkPos.z < tetrominoCtrl.GridManager.Depth)
            {
                if (tetrominoCtrl.GridManager.grid[checkPos.x, checkPos.x, checkPos.x] != null)
                {
                    return false;
                }
            }
        }
        return true;
    }

    protected virtual void PlaceOnGrid()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i].x >= 0 && cells[i].x < tetrominoCtrl.GridManager.With && cells[i].y >= 0 && cells[i].y < tetrominoCtrl.GridManager.Height && cells[i].z >= 0 && cells[i].z < tetrominoCtrl.GridManager.Depth)
            {
                tetrominoCtrl.GridManager.PlaceBlock(tetrominoCtrl.Blocks[i].transform);
            }
        }
        tetrominoCtrl.GridManager.ClearFullRows();
        this.isControl = false;
        TetrisSpawner tetrisSpawner = FindFirstObjectByType<TetrisSpawner>();
        tetrisSpawner.SpawnNewBlock();
    }
}
