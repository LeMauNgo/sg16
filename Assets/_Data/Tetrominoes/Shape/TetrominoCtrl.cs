using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class TetrominoCtrl : PoolObj
{
    [SerializeField] protected TetrominoMover mover;
    public TetrominoMover Mover => mover;
    [SerializeField] protected GridManager gridManager;
    public GridManager GridManager => gridManager;
    [SerializeField] protected TertrominoesVisual visual;
    public TertrominoesVisual TertrominoesVisual => visual;
    [SerializeField] protected TertrominoesRotation rotation;
    public TertrominoesRotation Rotation => rotation;
    [SerializeField] protected GameObject[] blocks = new GameObject[4];
    public GameObject[] Blocks => blocks; 

    [SerializeField] protected Vector3Int[,] rotationOffsets;
    public Vector3Int[,] RotationOffsets => rotationOffsets;
    [SerializeField] protected int rotationState;
    public int RotationState => rotationState;
    [SerializeField] protected Vector3Int[] cells = new Vector3Int[4];
    public Vector3Int[] Cells => cells;
    [SerializeField] protected Vector3Int position;
    public Vector3Int Position => position;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMover();
        this.LoadGridManager();
        this.SetOffset();
        this.LoadTertrominoesVisual();
        this.LoadTertrominoesRotation();
    }
    private void OnEnable()
    {
        this.rotationState = 0;
        this.position = new Vector3Int(3, 20, 0);
        this.SetCells();
    }
    protected virtual void LoadMover()
    {
        if (this.mover != null) return;
        this.mover = GetComponentInChildren<TetrominoMover>();
        Debug.LogWarning(transform.name + ": LoadMover", gameObject);
    }
    protected virtual void LoadGridManager()
    {
        if(this.gridManager != null) return;
        this.gridManager = FindFirstObjectByType<GridManager>();
        Debug.LogWarning(gameObject.name + " LoadGridManager", gameObject);
    }
    protected virtual void LoadTertrominoesVisual()
    {
        if (this.visual != null) return;
        this.visual = GetComponentInChildren<TertrominoesVisual>();
        Debug.LogWarning(gameObject.name + " LoadTertrominoesVisual", gameObject);
    }
    protected virtual void LoadTertrominoesRotation()
    {
        if (this.rotation != null) return;
        this.rotation = GetComponentInChildren<TertrominoesRotation>();
        Debug.LogWarning(gameObject.name + " LoadTertrominoesRotation", gameObject);
    }
    protected abstract void SetOffset();
    public virtual void SetPosition(Vector3Int pos)
    {
        this.position = pos;
    }
    public virtual void SetCells()
    {
        for (int i = 0; i < 4; i++)
        {
            cells[i] = this.RotationOffsets[rotationState, i] + position;
        }
        this.TertrominoesVisual.UpdateVisuals();
    }
    public virtual void SetRotationState(int state)
    {
        this.rotationState = state;
    }
    public virtual void SetCells(Vector3Int[] vector3s)
    {
        this.cells = vector3s;
    }
    public virtual bool IsRotatable()
    {
        return true;
    }
}
