using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TertrominoesVisual : TertrominoesAbs
{
    [SerializeField] protected List<CubeTetrominoes> cubes;
    public List<CubeTetrominoes> Cubes => cubes;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCube();
    }
    protected virtual void LoadCube()
    {
        if (cubes.Count > 0) return;
        this.cubes = GetComponentsInChildren<CubeTetrominoes>().ToList();
        this.CreateVisuals();
        Debug.LogWarning(gameObject.name + " LoadCube", gameObject);
    }
    private void CreateVisuals()
    {
        for (int i = 0; i < 4; i++)
        {
            tetrominoCtrl.Blocks[i] = cubes[i];
            cubes[i].gameObject.SetActive(true);
        }
    }
    public void UpdateVisuals()
    {
        for (int i = 0; i < 4; i++)
        {
            if (this.tetrominoCtrl.Blocks[i] == null) continue;
            tetrominoCtrl.Blocks[i].transform.position = new Vector3(tetrominoCtrl.Cells[i].x, tetrominoCtrl.Cells[i].y, tetrominoCtrl.Cells[i].z);
        }
    }
    protected virtual bool isDespawn()
    {
        foreach(CubeTetrominoes tetromino in this.cubes)
        {
            if (tetromino.IsShow) return false;
        }
        return true;
    }
    private void OnEnable()
    {
        this.CreateVisuals();
        this.UpdateVisuals();
    }
    public virtual void Despawn()
    {
        if (!this.isDespawn()) return;
        this.tetrominoCtrl.Despawn.DoDespawn();
    }
}
