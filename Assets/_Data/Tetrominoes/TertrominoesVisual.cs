using UnityEngine;

public class TertrominoesVisual : TertrominoesAbs
{
    [SerializeField] protected Transform cubesPrefab;
    private void OnEnable()
    {
        this.CreateVisuals();
    }
    private void CreateVisuals()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject blockObj = Instantiate(this.cubesPrefab.gameObject, transform);
            tetrominoCtrl.Blocks[i] = blockObj;
            blockObj.SetActive(true);
        }
    }

    protected void UpdateVisuals()
    {
        for (int i = 0; i < 4; i++)
        {
            tetrominoCtrl.Blocks[i].transform.position = new Vector3(tetrominoCtrl.Mover.Cells[i].x, tetrominoCtrl.Mover.Cells[i].y, tetrominoCtrl.Mover.Cells[i].z);
        }
    }
}
