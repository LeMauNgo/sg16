using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCurrencyCtrl : InventoryCtrl
{
    public override InventoryType GetName()
    {
        return InventoryType.Currency;
    }
}
