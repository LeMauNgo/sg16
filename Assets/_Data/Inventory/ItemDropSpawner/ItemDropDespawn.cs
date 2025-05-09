using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropDespawn : Despawn<ItemDropCtrl>
{
    [SerializeField] protected ItemDropCtrl itemDropCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemDropCtrl();
    }
    protected virtual void LoadItemDropCtrl()
    {
        if (this.itemDropCtrl != null) return;
        this.itemDropCtrl = GetComponentInParent<ItemDropCtrl>();
        Debug.LogWarning(gameObject.name + "LoadItemDropCtrl", gameObject);
    }
    public override void DoDespawn()
    {
        base.DoDespawn();
        InventoriesManager.Instance.AddItem(this.itemDropCtrl.GetItemCode(), this.itemDropCtrl.DropCount);
    }
    
}
