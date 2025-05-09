using UnityEngine;

public class CubeChestTetrominoesCtrl : CubeTetrominoes
{
    [SerializeField] protected ChestCtrl chestCtrl;
    public override string GetName()
    {
        return CubeCode.CubeChestTetrominoes.ToString();
    }
    public virtual void SetChestCtrl(ChestCtrl chestCtrl)
    {
        this.chestCtrl = chestCtrl;
    }
    public virtual void OpenChest()
    {
        if (chestCtrl != null)
        {
            chestCtrl.TryOpenChest();
        }
    }
    private void OnDisable()
    {
            chestCtrl = null;
    }
}
