using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Slot
{
    public Item _item { get; set; }
    public int _item_Num {  get; set; }
    public bool _is_Hidden {  get; set; }

    public void ResetSlot()
    {
        _item = null;
        _item_Num = 0;
        _is_Hidden = false;
    }
}
