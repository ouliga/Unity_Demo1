using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip_Controller_Buff_Components_Add_Healthy : AirShip_Controller_Buff_Base
{
    private int _add_Healthy;
    public AirShip_Controller_Buff_Components_Add_Healthy(int add_Healthy)
    {
        _add_Healthy = add_Healthy;
        _buff_Description = $"所有组件增加{add_Healthy}生命值";
    }
    public override void Buff_AirShip_Components(AirShip_Component[,] airShip_Components)
    {
        if (airShip_Components != null)
        {
            int maxNum_X = airShip_Components.GetLength(0);
            int maxNum_Y = airShip_Components.GetLength(1);

            for (int i = 0; i < maxNum_X; i++)
            {
                for (int j = 0; j < maxNum_Y; j++)
                {
                    if (airShip_Components[i, j] == null) continue;
                    airShip_Components[i, j].Change_Healthy(_add_Healthy);
                }
            }
        }
    }
    public override void Buff_AirShip_Controller_Fly(AirShip_Controller_Fly airShip_Controller_Fly)
    {
        
    }
}
