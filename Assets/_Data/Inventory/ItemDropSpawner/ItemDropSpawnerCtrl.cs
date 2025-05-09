using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropSpawnerCtrl : SaiSingleton<ItemDropSpawnerCtrl>
{
    [SerializeField] protected ItemDropSpawner spawner;
    public ItemDropSpawner Spawner => spawner;
    [SerializeField] protected ItemDropPrefabs prefabs;
    public ItemDropPrefabs Prefabs => prefabs;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawner();
        this.LoadItemDropPrefabs();
    }
    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GetComponent<ItemDropSpawner>();
        Debug.LogWarning(gameObject.name + "LoadSpawner", gameObject);
    }
    protected virtual void LoadItemDropPrefabs()
    {
        if (this.prefabs != null) return;
        this.prefabs = GetComponentInChildren<ItemDropPrefabs>();
        Debug.LogWarning(gameObject.name + "LoadItemDropPrefabs", gameObject);
    }
    public virtual ItemDropCtrl Drop(ItemCode itemCode, Vector3 position, int dropCount)
    {
        ItemDropCtrl prefabs = this.Prefabs.GetByName(itemCode.ToString());
        ItemDropCtrl itemDropCtrl = this.Spawner.Spawn(prefabs, position);
        itemDropCtrl.SetDropCount(dropCount);
        itemDropCtrl.gameObject.SetActive(true);
        return itemDropCtrl;
    }
    public virtual void DropMany(ItemCode itemCode, Vector3 dropPosition, int dropCount)
    {
        for(int i = 0; i < dropCount; i++)
        {
            this.Drop(itemCode, dropPosition, 1);
        }
    }
}
