using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data_AirShip
{
    public int[,] _data_AirShip_Cube;
    public int[,] _data_AirShip_Equipment;

    public void Save_AirShip_Data(Inventory_Slot[,] inventory_EquipBag_Cube, Inventory_Slot[,] inventory_EquipBag_Equipment)
    {

        //保存装备栏数据

        int inventory_Equip_MaxNum_X = inventory_EquipBag_Cube.GetLength(0);
        int inventory_Equip_MaxNum_Y = inventory_EquipBag_Cube.GetLength(1);
        _data_AirShip_Cube = new int[inventory_Equip_MaxNum_X, inventory_Equip_MaxNum_Y];
        _data_AirShip_Equipment = new int[inventory_Equip_MaxNum_X, inventory_Equip_MaxNum_Y];
        for (int i = 0; i < inventory_Equip_MaxNum_X; i++)
        {
            for (int j = 0; j < inventory_Equip_MaxNum_Y; j++)
            {
                if (inventory_EquipBag_Cube[i, j]._item != null)
                {
                    _data_AirShip_Cube[i, j] = inventory_EquipBag_Cube[i, j]._item._id;
                }
                else
                {
                    _data_AirShip_Cube[i, j] = -1;
                }

                if (inventory_EquipBag_Equipment[i, j]._item != null)
                {
                    _data_AirShip_Equipment[i, j] = inventory_EquipBag_Equipment[i, j]._item._id;
                }
                else
                {
                    _data_AirShip_Equipment[i, j] = -1;
                }
            }
        }
    }
}
