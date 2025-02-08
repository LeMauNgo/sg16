using System.Collections.Generic;
using UnityEngine;

public abstract class TetrominoCtrl : PoolObj
{
    [SerializeField] protected TetrominoMover mover;
    public TetrominoMover Mover { get { return mover; } }

    [SerializeField] protected List<CubeCollision> cubes;
    public List<CubeCollision> Cubes { get { return cubes; } }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMover();
        this.LoadCubes();
    }

    protected virtual void LoadMover()
    {
        if (this.mover != null) return;
        this.mover = GetComponentInChildren<TetrominoMover>();
        Debug.LogWarning(transform.name + ": LoadMover", gameObject);
    }

    protected virtual void LoadCubes()
    {
        if (this.cubes.Count > 0) return;
        CubeCollision[] arrayCubes = transform.GetComponentsInChildren<CubeCollision>();
        this.cubes = new List<CubeCollision>(arrayCubes);
        Debug.LogWarning(transform.name + ": LoadCubes", gameObject);
    }

    public virtual bool IsRotatable()
    {
        return true;
    }
}
