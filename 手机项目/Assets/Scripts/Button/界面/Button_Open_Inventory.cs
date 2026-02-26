using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Open_Inventory : Button_Base
{
    public override void OnClick()
    {
        if (UI_Panel_Manager.Instance != null)
        {
            UI_Panel_Manager.Instance.Open_Inventory_Panel();
        }
    }
}
