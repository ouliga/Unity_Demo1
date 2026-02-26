using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Inventory_AirShip_Attribute_Information
{
    public GroupType _groupType {  get; private set; }
    public int _airShip_Healthy {  get; private set; }
    public int _airShip_Weight { get; private set; }
    public int _airShip_LiftForce { get; private set; }

    public Inventory_AirShip_Attribute_Information(GroupType groupType)
    {
        _groupType = groupType;
        _airShip_Healthy = 0;
        _airShip_Weight = 0;
        _airShip_LiftForce = 0;
    }
    public void Update_Attribute_Information(Inventory_Slot[,] inventory_EquipBag_Cube, Inventory_Slot[,] inventory_EquipBag_Equipment)
    {
        Reset_Attribute_Information();
        for(int i = 0;i < inventory_EquipBag_Cube.GetLength(0); i++)
        {
            for(int j = 0;j < inventory_EquipBag_Cube.GetLength(1); j++)
            {
                Item item = inventory_EquipBag_Cube[i, j]._item;
                if (item == null) continue;
                _airShip_Healthy += item._healthy;
                _airShip_Weight += item._weight;
                _airShip_LiftForce += item._liftForce;
            }
        }
        for (int i = 0; i < inventory_EquipBag_Equipment.GetLength(0); i++)
        {
            for (int j = 0; j < inventory_EquipBag_Equipment.GetLength(1); j++)
            {
                Item item = inventory_EquipBag_Equipment[i, j]._item;
                if (item == null) continue;
                _airShip_Healthy += item._healthy;
                _airShip_Weight += item._weight;
                _airShip_LiftForce += item._liftForce;
            }
        }
    }
    private void Reset_Attribute_Information()
    {
        _airShip_Healthy = 0;
        _airShip_Weight = 0;
        _airShip_LiftForce = 0;
    }
}
