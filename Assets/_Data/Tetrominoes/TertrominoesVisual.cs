using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TertrominoesVisual : TertrominoesPlayerAbs
{
    [SerializeField] protected List<CubeTetrominoes> cubes;
    public List<CubeTetrominoes> Cubes => cubes;
    public void CreateVisuals()
    {
        this.cubes.Clear();
        for (int i = 0; i < 4; i++)
        {
            this.cubes.Add(this.SpawnCube() as CubeTetrominoes);
        }
        this.UpdateVisuals();
        this.tetrominoCtrl.SetCells();

    }
    protected virtual CubeCtrl SpawnCube()
    {
        CubeCtrl cubeCtrl = CubeSpawnerManager.Instance.Spanwer.PoolPrefabs.GetByName(CubeCode.CubeTetrominoes.ToString());
        CubeCtrl cube = CubeSpawnerManager.Instance.Spanwer.Spawn(cubeCtrl, this.transform.position);
        cube.gameObject.SetActive(true);
        return cube;
    }    
    public void UpdateVisuals()
    {
        for (int i = 0; i < 4; i++)
        {
            this.cubes[i].transform.position = new Vector3(tetrominoCtrl.Cells[i].x, tetrominoCtrl.Cells[i].y, tetrominoCtrl.Cells[i].z);
        }
    }
}
