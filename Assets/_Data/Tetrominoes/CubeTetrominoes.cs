using UnityEngine;

public class CubeTetrominoes : SaiBehaviour
{
    [SerializeField] protected TertrominoesVisual tertrominoesVisual;
    [SerializeField] protected bool isShow = true;
    public bool IsShow => isShow;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTertrominoesVisual();
    }
    protected virtual void LoadTertrominoesVisual()
    {
        if (this.tertrominoesVisual != null) return;
        this.tertrominoesVisual = GetComponentInParent<TertrominoesVisual>();
        Debug.LogWarning(gameObject.name + " LoadTertrominoesVisual", gameObject);
    }
    private void OnEnable()
    {
        this.isShow = true;
    }
    private void OnDisable()
    {
        this.isShow = false;
    }
}
