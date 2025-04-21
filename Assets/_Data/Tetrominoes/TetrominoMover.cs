using UnityEngine;
using UnityEngine.UIElements;

public class TetrominoMover : TertrominoesAbs
{
    private float fallTimer = 0f;
    private float fallInterval = 1f;
    [SerializeField]  private bool isMoving;
    private void OnEnable()
    {
        this.isMoving = true;
    }
    private void Update()
    {
        if (!isMoving) return;
        this.HandleAutoFall();
        this.MoveDirection();
    }
    protected virtual void MoveDirection()
    {
        if (tetrominoCtrl.PlayerID == 1)
        {
            if (InputManager.Instance.Player1State == PlayerState.Left) this.Move(Vector3Int.left);
            if (InputManager.Instance.Player1State == PlayerState.Down) this.Move(Vector3Int.down);
            if (InputManager.Instance.Player1State == PlayerState.Right) this.Move(Vector3Int.right);
        }
        else if (tetrominoCtrl.PlayerID == 2)
        {
            if (InputManager.Instance.Player2State == PlayerState.Left) this.Move(Vector3Int.left);
            if (InputManager.Instance.Player2State == PlayerState.Down) this.Move(Vector3Int.down);
            if (InputManager.Instance.Player2State == PlayerState.Right) this.Move(Vector3Int.right);
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

    protected virtual void Move(Vector3Int direction)
    {
        Vector3Int newPosition = this.tetrominoCtrl.Position + direction;
        if (IsValidPosition(newPosition))
        {
            this.tetrominoCtrl.SetPosition(newPosition);
            this.tetrominoCtrl.SetCells();
        }
        else if (direction == Vector3Int.down)
        {
            this.isMoving = false;
            PlaceOnGrid();
        }
    }

    protected virtual bool IsValidPosition(Vector3Int newPosition)
    {
        foreach (var cell in this.tetrominoCtrl.Cells)
        {
            Vector3Int checkPos = cell + newPosition - this.tetrominoCtrl.Position;
            if (!GridManager.Instance.IsInsideGrid(checkPos))
            {
                return false;
            }
            if (checkPos.x >= 0 && checkPos.x < GridManager.Instance.With && checkPos.y >= 0 && checkPos.y < GridManager.Instance.Height && checkPos.z >= 0 && checkPos.z < GridManager.Instance.Depth)
            {
                if (GridManager.Instance.GridRows[checkPos.y].row[checkPos.x] != null)
                {
                    return false;
                }
            }
        }
        return true;
    }

    protected virtual void PlaceOnGrid()
    {
        for (int i = 0; i < this.tetrominoCtrl.Cells.Length; i++)
        {
            if (this.tetrominoCtrl.Cells[i].x >= 0 && this.tetrominoCtrl.Cells[i].x < GridManager.Instance.With && this.tetrominoCtrl.Cells[i].y >= 0 && this.tetrominoCtrl.Cells[i].y < GridManager.Instance.Height && this.tetrominoCtrl.Cells[i].z >= 0 && this.tetrominoCtrl.Cells[i].z < GridManager.Instance.Depth)
            {
                GridManager.Instance.PlaceBlock(tetrominoCtrl.Blocks[i]);
            }
        }
        GridManager.Instance.ClearFullRows();
        if(this.tetrominoCtrl.PlayerID == 1)
        {
            TetrominoManager.Instance.Spanwer.SpawnTetromino(1, new Vector3Int(3, 20, 0));
        }
        else if (this.tetrominoCtrl.PlayerID == 2)
        {
            TetrominoManager.Instance.Spanwer.SpawnTetromino(2, new Vector3Int(13,20,0));
        }
        this.tetrominoCtrl.Rotation.SetRotation(false);
    }
}
