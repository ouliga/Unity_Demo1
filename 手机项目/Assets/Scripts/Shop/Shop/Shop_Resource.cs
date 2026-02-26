using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Resource
{
    public int _shop_Level { get; private set; }
    public int _money_Num { get; private set; }
    public int _component_Num { get; private set; }
    public int _item_Buy_Price_Money { get; private set; }
    public int _item_Sell_Price_Money { get; private set; }
    public int _item_Buy_Price_Component { get; private set; }
    public int _item_Sell_Price_Component { get; private set; }

    public int _shop_LevelUp_Cost { get; private set; }
    public int _shop_Refresh_Cost { get; private set; }

    private Shop_Weight_Rate _shop_Weight_Rate;

    public Shop_Resource()
    {
        _shop_Level = 1;
        _money_Num = 1000;
        _component_Num = 10;
        _item_Buy_Price_Money = 25;
        _item_Sell_Price_Money = 10;
        _item_Buy_Price_Component = 1;
        _item_Sell_Price_Component = 1;
        _shop_Refresh_Cost = 10;

        _shop_Weight_Rate = new Shop_Weight_Rate();
        Update_Shop_Resource_By_ShopLevel();
    }

    public bool Check_Cost_Money(int money_Num)
    {
        return money_Num <= _money_Num;
    }
    public void Cost_Money(int money_Num)
    {
        _money_Num -= money_Num;
    }
    public bool Check_Cost_Component(int component_Num)
    {
        return component_Num <= _component_Num;
    }
    public void Cost_Component(int component_Num)
    {
        _component_Num -= component_Num;
    }

    public bool Check_Buy_Item_Cube()
    {
        return _money_Num >= _item_Buy_Price_Money;
    }
    public void Buy_Item_Cube()
    {
        _money_Num -= _item_Buy_Price_Money;
    }
    public void Sell_Item_Cube()
    {
        _money_Num += _item_Sell_Price_Money;
    }

    public bool Check_Buy_Item_Equipment()
    {
        if(_component_Num >= _item_Buy_Price_Component && _money_Num >= _item_Buy_Price_Money)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Buy_Item_Equipment()
    {
        _money_Num -= _item_Buy_Price_Money;
        _component_Num -= _item_Buy_Price_Component;
    }

    public void Sell_Item_Equipment()
    {
        _money_Num += _item_Sell_Price_Money;
        _component_Num += _item_Sell_Price_Component;
    }

    public bool Check_Refresh_Shop()
    {
        return _money_Num >= _shop_Refresh_Cost;
    }
    public void Refresh_Shop_Cost()
    {
        _money_Num -= _shop_Refresh_Cost;
    }

    //商店升级
    public bool Check_Shop_LevelUp()
    {
        if (_shop_Level + 1 > 4)
        {
            Debug.Log("商店已达最高级");
            return false;
        }
        return _shop_LevelUp_Cost <= _money_Num;
    }
    public void Shop_LevelUp()
    {
        Cost_Shop_LevelUp();
        _shop_Level++;
        Update_Shop_Resource_By_ShopLevel();
    }
    private void Update_Shop_Resource_By_ShopLevel()
    {
        Update_Shop_LevelUp_Cost();
        Update_Shop_Weight_Rate();
    }

    private void Cost_Shop_LevelUp()
    {
        _money_Num -= _shop_LevelUp_Cost;
    }
    private void Update_Shop_LevelUp_Cost()
    {
        switch (_shop_Level)
        {
            case 1:
                _shop_LevelUp_Cost = 150;
                break;
            case 2:
                _shop_LevelUp_Cost = 200;
                break;
            case 3:
                _shop_LevelUp_Cost = 250;
                break;
            case 4:
                _shop_LevelUp_Cost = 300;
                break;
        }
    }
    //商店物品权重比率
    public float Get_Shop_Item_Weight(int rare)
    {
        if(_shop_Weight_Rate != null)
        {
            return _shop_Weight_Rate.Get_Shop_Item_Weight(rare);
        }
        return 0;
    }
    private void Update_Shop_Weight_Rate()
    {
        if (_shop_Weight_Rate != null)
        {
            _shop_Weight_Rate.Update_By_Shop_Level(_shop_Level);
        }
    }
    //获得关卡奖励
    public void Get_Shop_Reward(Map_Node map_Node)
    {
        if (map_Node == null) return;
        RoomType roomType = map_Node._roomType;
        if(roomType == RoomType.Normal)
        {
            _money_Num += 100;
            _component_Num += 1;
        }
        else if(roomType == RoomType.Hard)
        {
            _money_Num += 100;
            _component_Num += 2;

        }
        else if(roomType == RoomType.Boss)
        {
            _money_Num += 200;
            _component_Num += 3;
        }
    }

    //读取数据
    public void Load_Data(int shop_Level,int shop_Money_Num, int shop_Component_Num)
    {
        _shop_Level = shop_Level;
        _money_Num = shop_Money_Num;
        _component_Num = shop_Component_Num;
        Update_Shop_Resource_By_ShopLevel();
    }
}
