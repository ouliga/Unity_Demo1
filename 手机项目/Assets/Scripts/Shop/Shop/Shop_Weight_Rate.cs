using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shop_Weight_Rate
{
    //public int _shop_Level { get; private set; }
    public float _weight_Rate_0 {  get; private set; }
    public float _weight_Rate_1 { get; private set; }
    public float _weight_Rate_2 { get; private set; }
    public float _weight_Rate_3 { get; private set; }
    private float _weight_Base = 20;

    public void Update_By_Shop_Level(int shop_Level)
    {
        Update_Weight(shop_Level);
    }

    public float Get_Shop_Item_Weight(int rare)
    {
        float weight = 0f;
        switch (rare)
        {
            case 0:
                weight = _weight_Base * _weight_Rate_0;
                break;
            case 1:
                weight = _weight_Base * _weight_Rate_1;
                break;
            case 2:
                weight = _weight_Base * _weight_Rate_2;
                break;
            case 3:
                weight = _weight_Base * _weight_Rate_3;
                break;
            default:
                break;
        }
        return weight;
    }
    private void Update_Weight(int shop_Level)
    {
        switch (shop_Level)
        {
            case 1:
                _weight_Rate_0 = 1;
                _weight_Rate_1 = 0;
                _weight_Rate_2 = 0;
                _weight_Rate_3 = 0;
                break;
            case 2:
                _weight_Rate_0 = 2;
                _weight_Rate_1 = 1;
                _weight_Rate_2 = 0;
                _weight_Rate_3 = 0;
                break;
            case 3:
                _weight_Rate_0 = 3;
                _weight_Rate_1 = 2;
                _weight_Rate_2 = 1;
                _weight_Rate_3 = 0;
                break;
            case 4:
                _weight_Rate_0 = 4;
                _weight_Rate_1 = 3;
                _weight_Rate_2 = 2;
                _weight_Rate_3 = 1;
                break;
        }
    }
}
