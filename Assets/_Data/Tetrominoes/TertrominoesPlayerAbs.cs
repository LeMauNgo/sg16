using UnityEngine;

public class TertrominoesPlayerAbs : SaiBehaviour
{
    [SerializeField] protected TetrominoPlayer tetrominoCtrl;
    public TetrominoPlayer TetrominoCtrl => tetrominoCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTetrominoCtrl();
    }
    protected virtual void LoadTetrominoCtrl()
    {
        if (this.tetrominoCtrl != null) return;
        this.tetrominoCtrl = GetComponentInParent<TetrominoPlayer>();
        Debug.LogWarning(gameObject.name + " LoadTetrominoCtrl", gameObject);
    }
}
