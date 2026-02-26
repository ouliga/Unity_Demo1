using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Load_EnemyAirShip_Data : Button_Base
{
    [SerializeField] private string filename;
    public override void OnClick()
    {
        Data_AirShip data_AirShip = null;
        if(Data_Manager.Instance != null)
        {
            data_AirShip = Data_Manager.Instance.LoadData_AirShip(filename);
        }
        if(Inventory_Manager.Instance != null)
        {
            Inventory_Manager.Instance.Load_AirShip_Data_Enemy(data_AirShip);
        }


    }
}
