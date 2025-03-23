using UnityEngine;

public class TertrominoesAbs : SaiBehaviour
{
    [SerializeField] protected TetrominoCtrl tetrominoCtrl;
    public TetrominoCtrl TetrominoCtrl => tetrominoCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTetrominoCtrl();
    }
    protected virtual void LoadTetrominoCtrl()
    {
        if (this.tetrominoCtrl != null) return;
        this.tetrominoCtrl = GetComponentInParent<TetrominoCtrl>();
        Debug.LogWarning(gameObject.name + " LoadTetrominoCtrl", gameObject);
    }
}
