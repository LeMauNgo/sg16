using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class InventoryTesting : SaiBehaviour
{
    protected override void Start()
    {
        base.Start();
        //this.AddTestItems(ItemCode.HealthPotion, 100);
        //this.AddTestItems(ItemCode.Gold , 9999999);
    }

    [ProButton]
    public virtual void AddTestItems(ItemCode itemCode, int count)
    {
        for (int i = 0; i < count; i++)
        {
            InventoriesManager.Instance.AddItem(itemCode, 1);
        }
    }

    [ProButton]
    public virtual void RemoveTestItems(ItemCode itemCode, int count)
    {
        for (int i = 0; i < count; i++)
        {
            InventoriesManager.Instance.RemoveItem(itemCode, 1);
        }
    }
}
