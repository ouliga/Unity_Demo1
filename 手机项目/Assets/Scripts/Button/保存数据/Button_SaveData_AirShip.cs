using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_SaveData_AirShip : Button_Base
{
    public override void OnClick()
    {
        Data_AirShip data_AirShip = null;

        if (Inventory_Manager.Instance != null)
        {
            data_AirShip = Inventory_Manager.Instance.Save_AirShip_Data();
        }
        if (Data_Manager.Instance != null)
        {
            Data_Manager.Instance.SaveData_AirShip(data_AirShip);
        }
    }
}
