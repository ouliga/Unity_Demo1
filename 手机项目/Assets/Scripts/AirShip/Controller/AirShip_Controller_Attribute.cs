using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip_Controller_Attribute
{
    public int _airShip_Healthy { get; private set; }
    public int _airShip_Healthy_Max { get; private set; }
    public int _airShip_Weight { get; private set; }
    public int _airShip_LiftForce { get; private set; }

    public AirShip_Controller_Attribute()
    {
        _airShip_Healthy = 0;
        _airShip_Healthy_Max = 0;
        _airShip_Weight = 0;
        _airShip_LiftForce = 0;
    }
    public void Update_AirShip_Attributes(AirShip_Component[,] airShip_Components)
    {
        if (airShip_Components == null)
        {
            Debug.Log("飞船组件数组未初始化");
            return;
        }

        Reset_AirShip_Attributes();
        int airShip_MaxNum_X = airShip_Components.GetLength(0);
        int airShip_MaxNum_Y = airShip_Components.GetLength(1);

        for (int i = 0; i < airShip_MaxNum_X; i++)
        {
            for (int j = 0; j < airShip_MaxNum_Y; j++)
            {
                if (airShip_Components[i, j] == null) continue;
                if (!airShip_Components[i, j]._isAlive) continue;
                _airShip_Healthy += airShip_Components[i, j]._healthy;
                _airShip_Weight += airShip_Components[i, j]._weight;
                _airShip_LiftForce += airShip_Components[i, j]._liftForce;
            }
        }
    }
    private void Reset_AirShip_Attributes()
    {
        _airShip_Healthy = 0;
        _airShip_Weight = 0;
        _airShip_LiftForce = 0;
    }
}
