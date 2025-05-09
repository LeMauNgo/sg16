using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCtrl : ItemDropCtrl
{
    public override ItemCode GetItemCode()
    {
        return ItemCode.Gold;
    }
}
