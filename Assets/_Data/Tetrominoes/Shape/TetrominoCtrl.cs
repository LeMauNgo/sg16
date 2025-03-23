using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class TetrominoCtrl : PoolObj
{
    [SerializeField] protected TetrominoMover mover;
    public TetrominoMover Mover => mover;
    [SerializeField] protected GridManager gridManager;
    public GridManager GridManager => gridManager;
    [SerializeField] protected GameObject[] blocks = new GameObject[4];
    public GameObject[] Blocks => blocks; 

    [SerializeField] protected Vector3Int[,] rotationOffsets;
    public Vector3Int[,] RotationOffsets => rotationOffsets;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMover();
        this.LoadGridManager();
        this.SetOffset();
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
    protected abstract void SetOffset();

    public virtual bool IsRotatable()
    {
        return true;
    }
}
