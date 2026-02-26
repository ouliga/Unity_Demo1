using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Save : Button_Base
{
    public override void OnClick()
    {
        if (Data_Manager.Instance != null)
        {
            if (Inventory_Manager.Instance != null)
            {
                Inventory_Manager.Instance.Save_Inventory_Data();
            }
            if(Shop_Manager.Instance != null)
            {
                Shop_Manager.Instance.Save_Shop_Data();
            }
            if(Map_Manager.Instance != null)
            {
                Map_Manager.Instance.Save_Map_Data();
            }
            Data_Manager.Instance.SaveData();
        }

    }

}
