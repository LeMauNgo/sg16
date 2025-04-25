using UnityEngine;
using UnityEngine.UIElements;

public class TertrominoesRotation : TertrominoesPlayerAbs
{
    [SerializeField] protected bool isRotation;
    public virtual void SetRotation(bool isRotat)
    {
        this.isRotation = isRotat;
    }
    private void Update()
    {
        if (!this.isRotation || !GameManager.Instance.IsPlaying) return;
        if (tetrominoCtrl.PlayerID == 1)
        {
            if (InputManager.Instance.Player1State == PlayerState.Rotate) this.Rotate();
        }
        else if (tetrominoCtrl.PlayerID == 2)
        {
            if (InputManager.Instance.Player2State == PlayerState.Rotate) this.Rotate();
        }
    }
    protected virtual void Rotate()
    {
        int newState = (this.tetrominoCtrl.RotationState + 1) % tetrominoCtrl.RotationOffsets.Count;
        Vector3Int[] newCells = new Vector3Int[4];

        for (int i = 0; i < 4; i++)
        {
            newCells[i] = tetrominoCtrl.RotationOffsets[newState].cellPositions[i] + this.tetrominoCtrl.Position;
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
            if (!GridManager.Instance.IsInsideGrid(cell) || GridManager.Instance.GridRows[cell.y].row[cell.x] != null)
            {
                return false;
            }
        }
        return true;
    }
}
