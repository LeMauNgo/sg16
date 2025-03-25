using UnityEngine;
using UnityEngine.UIElements;

public class TertrominoesRotation : TertrominoesAbs
{
    [SerializeField] protected bool isRotation;
    public virtual void SetRotation(bool isRotat)
    {
        this.isRotation = isRotat;
    }
    private void OnEnable()
    {
        this.isRotation = true;
    }
    private void Update()
    {
        if (!this.isRotation) return;
        if (InputManager.Instance.IsRotat) this.Rotate();
    }
    protected virtual void Rotate()
    {
        int newState = (this.tetrominoCtrl.RotationState + 1) % tetrominoCtrl.RotationOffsets.GetLength(0);
        Vector3Int[] newCells = new Vector3Int[4];

        for (int i = 0; i < 4; i++)
        {
            newCells[i] = tetrominoCtrl.RotationOffsets[newState, i] + this.tetrominoCtrl.Position;
        }

        if (IsValidRotation(newCells))
        {
            this.tetrominoCtrl.SetCells(newCells);
            this.tetrominoCtrl.SetRotationState(newState);
            this.tetrominoCtrl.TertrominoesVisual.UpdateVisuals();
        }
    }
    private bool IsValidRotation(Vector3Int[] newCells)
    {
        foreach (var cell in newCells)
        {
            if (!tetrominoCtrl.GridManager.IsInsideGrid(cell) || tetrominoCtrl.GridManager.GridRows[cell.y].row[cell.x] != null)
            {
                return false;
            }
        }
        return true;
    }
}
