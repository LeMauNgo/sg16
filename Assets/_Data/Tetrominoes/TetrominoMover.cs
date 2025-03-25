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
        if(InputManager.Instance.IsLeft) this.Move(Vector3Int.left);
        if(InputManager.Instance.IsDown) this.Move(Vector3Int.down);
        if(InputManager.Instance.IsRight) this.Move(Vector3Int.right);
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
            PlaceOnGrid();
        }
    }

    protected virtual bool IsValidPosition(Vector3Int newPosition)
    {
        foreach (var cell in this.tetrominoCtrl.Cells)
        {
            Vector3Int checkPos = cell + newPosition - this.tetrominoCtrl.Position;
            if (!tetrominoCtrl.GridManager.IsInsideGrid(checkPos))
            {
                return false;
            }
            if (checkPos.x >= 0 && checkPos.x < tetrominoCtrl.GridManager.With && checkPos.y >= 0 && checkPos.y < tetrominoCtrl.GridManager.Height && checkPos.z >= 0 && checkPos.z < tetrominoCtrl.GridManager.Depth)
            {
                if (tetrominoCtrl.GridManager.GridRows[checkPos.y].row[checkPos.x] != null)
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
            if (this.tetrominoCtrl.Cells[i].x >= 0 && this.tetrominoCtrl.Cells[i].x < tetrominoCtrl.GridManager.With && this.tetrominoCtrl.Cells[i].y >= 0 && this.tetrominoCtrl.Cells[i].y < tetrominoCtrl.GridManager.Height && this.tetrominoCtrl.Cells[i].z >= 0 && this.tetrominoCtrl.Cells[i].z < tetrominoCtrl.GridManager.Depth)
            {
                tetrominoCtrl.GridManager.PlaceBlock(tetrominoCtrl.Blocks[i].transform);
            }
        }
        tetrominoCtrl.GridManager.ClearFullRows();
        TetrominoManager.Instance.Spanwer.SpawnTetromino();
        this.isMoving = false;
        this.tetrominoCtrl.Rotation.SetRotation(false);
    }
}
