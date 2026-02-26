using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelectType
{
    None,BuyItem, SellItem
}
public class Shop_Manager : Singleton<Shop_Manager>
{
    private Data_SO _data_Item;

    private Inventory_Slot[] _shop_Items;
    public int _shop_Items_MaxNum { get; private set; }

    private List<Item> _all_Items;
    private Shop_UI_Manager _shop_UI_Manager;

    //商店资源
    private Shop_Resource _shop_Resource;
    //资源数量变化委托
    public delegate void Subscribe_Shop_Resource_Update(Shop_Resource shop_Resource_Information);
    public Subscribe_Shop_Resource_Update _subscribe_Shop_Resource_Update;

    //选中物品的index
    private int _select_Sell_Item_Index = -1;
    private int _select_Buy_Item_Index = -1;

    private void OnEnable()
    {
        if(Scene_Manager.Instance != null)
        {
            Scene_Manager.Instance._subscribe += Set_Shop_Manager;
            Scene_Manager.Instance._subscribe += Set_Shop_UI_Manager;
            Scene_Manager.Instance._subscribe += Initialize_Shop_UI_Slots;
            //Scene_Manager.Instance._subscribe += Update_Shop_UI_Slots;
            Scene_Manager.Instance._subscribe += Set_Shop_Information_Manager;
            Scene_Manager.Instance._subscribe += Update_Shop_Resource_Information;
            //Scene_Manager.Instance._subscribe += Update_Shop_Refresh_Price_Information;
            //Scene_Manager.Instance._subscribe += Update_Shop_LevelUp_Price_Information;
        }
    }
    private void OnDisable()
    {
        if (Scene_Manager.Instance != null)
        {
            Scene_Manager.Instance._subscribe -= Set_Shop_Manager;
            Scene_Manager.Instance._subscribe -= Set_Shop_UI_Manager;
            Scene_Manager.Instance._subscribe -= Initialize_Shop_UI_Slots;
            //Scene_Manager.Instance._subscribe -= Update_Shop_UI_Slots;
            Scene_Manager.Instance._subscribe -= Set_Shop_Information_Manager;
            Scene_Manager.Instance._subscribe -= Update_Shop_Resource_Information;
            //Scene_Manager.Instance._subscribe -= Update_Shop_Refresh_Price_Information;
            //Scene_Manager.Instance._subscribe -= Update_Shop_LevelUp_Price_Information;
        }
    }
    public void Set_Shop_Manager(Scene_Type scene_Type)
    {
        if (Game_Manager.Instance != null)
        {
            _data_Item = Game_Manager.Instance.Get_Data_Item();
        }
        if (scene_Type == Scene_Type.WorldScene)
        {
            if(!_has_Initialize)
            {
                Debug.Log("初始化商店管理器");
                _shop_Items_MaxNum = 16;//默认商店格数
                _shop_Items = new Inventory_Slot[_shop_Items_MaxNum];
                for (int i = 0; i < _shop_Items_MaxNum; i++)
                {
                    _shop_Items[i] = new Inventory_Slot();
                }

                _all_Items = Shop_Get_All_Items();
                _shop_Resource = new Shop_Resource();
                Load_Shop_Data();
                
                _select_Buy_Item_Index = -1;
                _select_Sell_Item_Index = -1;

                _has_Initialize = true;
                Debug.Log("商店管理器初始化成功");
            }
            else
            {
                Debug.Log("商店管理器已经初始化");
            }

        }
        else if(scene_Type == Scene_Type.StartScene)
        {
            Destroy(gameObject);
        }
    }
    //资源管理

    private void Update_Resource_Information()
    {
        Debug.Log($"尝试更改资源信息(金钱：{_shop_Resource._money_Num},零件：{_shop_Resource._component_Num})");
        if (_shop_Resource == null) return;
        if(_subscribe_Shop_Resource_Update != null)
        {
            _subscribe_Shop_Resource_Update.Invoke(_shop_Resource);
        }
    }
    //升级商店
    public bool Check_LevelUp_Shop()
    {
        if (_shop_Resource == null) return false;
        return _shop_Resource.Check_Shop_LevelUp();
    }
    public void LevelUp_Shop()
    {
        if(_shop_Resource == null) return;
        _shop_Resource.Shop_LevelUp();
        Update_Resource_Information();
    }
    //刷新商店
    public bool Check_Refresh_Shop()
    {
        if(_shop_Resource == null) return false;
        return _shop_Resource.Check_Refresh_Shop();
    }
    public void Refresh_Shop()
    {
        for (int i = 0; i < _shop_Items_MaxNum; i++)
        {
            _shop_Items[i].ResetSlot();
        }
        for (int i = 0; i < _shop_Items_MaxNum; i++)
        {
            Item item = Get_Random_Item(_all_Items);
            Add_Shop_Item(item, 1);
        }
        Reset_Select_Item();
    }
    public void Refresh_Shop_Cost()
    {
        if (_shop_Resource == null) return;
        _shop_Resource.Refresh_Shop_Cost();
        Update_Resource_Information();
    }
    //获得奖励
    public void Get_Shop_Reward(Map_Node map_Node)
    {
        if (_shop_Resource == null) return;
        _shop_Resource.Get_Shop_Reward(map_Node);
        Update_Resource_Information();
    }
    //根据id获得商品
    public Item Get_Shop_Item(int index)
    {
        if (index < 0 || index >= _shop_Items_MaxNum)
        {
            Debug.Log("获得的商品超过商店的界限");
            return null;
        }
        return _shop_Items[index]._item;
    }
    //添加和移除商品//
    public void Add_Shop_Item(Item item,int num)
    {
        if(item == null)
        {
            Debug.Log("无法添加空物品");
            return;
        }
        for(int i=0;i< _shop_Items_MaxNum; i++)
        {
            if (_shop_Items[i]._item == null)
            {
                Debug.Log($"添加商品(Index{i},Id:{item._id})");
                _shop_Items[i]._item = item;
                _shop_Items[i]._item_Num = num;
                Update_Shop_UI_Slot(i);
                break;
            }
        }
    }
    public bool Check_Remove_Shop_Item(int index, int num)
    {
        if (index <0||index >= _shop_Items_MaxNum)
        {
            Debug.Log("索引超出了商品列表范围");
            return false; 
        }
        if(_shop_Items[index]._item == null)
        {
            Debug.Log("不存在该商品");
            return false;
        }
        if(num > _shop_Items[index]._item_Num)
        {
            Debug.Log("无法购买超出物品的数量");
            return false;
        }
        return true;
    }
    public void Remove_Shop_Item(int index,int num)
    {
        if(num == _shop_Items[index]._item_Num)
        {
            _shop_Items[index].ResetSlot();
        }
        else
        {
            _shop_Items[index]._item_Num -= num;
        }
        Update_Shop_UI_Slot(index);
    }
    //选择,购买,出售商品
    public bool Check_HaveSelect_Item(SelectType selectType,int index)
    {
        if(selectType == SelectType.BuyItem)
        {
            if(_select_Buy_Item_Index == index)
            {
                return true;
            }
            else
            {
                _select_Buy_Item_Index = index;
                _select_Sell_Item_Index = -1;
                return false;
            }
        }
        else if (selectType == SelectType.SellItem)
        {
            if(_select_Sell_Item_Index == index)
            {
                return true;
            }
            else
            {
                _select_Sell_Item_Index = index;
                _select_Buy_Item_Index = -1;
                return false;
            }
        }
        return false;
    }
    //重置物品的选择
    public void Reset_Select_Item()
    {
        _select_Buy_Item_Index = -1;
        _select_Sell_Item_Index = -1;
        Reset_Shop_SelectTip();
        Reset_Shop_Item_Price_Information();
    }
    public bool Check_Buy_Item(Item item)
    {
        if (item == null)
        {
            return false;
        }
        if(_shop_Resource == null)return false;
        if (item._type == Item_Type.Cube)
        {
            return _shop_Resource.Check_Buy_Item_Cube();
        }
        else if (item._type == Item_Type.Equipment)
        {
            return _shop_Resource.Check_Buy_Item_Equipment();
        }
        return false;
    }
    public void Buy_Item(Item item)
    {
        if (item == null) return;
        if (_shop_Resource == null) return;
        if (item._type == Item_Type.Cube)
        {
            _shop_Resource.Buy_Item_Cube();   
        }
        else if(item._type == Item_Type.Equipment)
        {
            _shop_Resource.Buy_Item_Equipment();
        }
        Update_Resource_Information();
    }
    public void Sell_Item(Item item)
    {
        if (item == null) return;
        if( _shop_Resource == null) return;
        if (item._type == Item_Type.Cube)
        {
            _shop_Resource.Sell_Item_Cube();
        }
        else if (item._type == Item_Type.Equipment)
        {
            _shop_Resource.Sell_Item_Equipment();
        }
        Update_Resource_Information();
    }

    //获得全部商品类别//
    private List<Item> Shop_Get_All_Items()
    {
        Debug.Log("获取所有物品");
        List<Item> items = new List<Item>();

        foreach (ExcelData excelData in _data_Item._data)
        {
            Item item = new Item(excelData._id, _data_Item);
            items.Add(item);
        }
        return items;
    }

    //得到随机的商品//
    public Item Get_Random_Item(List<Item> items)
    {
        if (items == null)
        {
            Debug.Log("物品列表未初始化");
            return null;
        }
        if(_shop_Resource == null) return null;

        float all_Weight = 0;
        foreach(Item item in items)
        {
            if (item._isCore) continue;
            all_Weight = all_Weight + _shop_Resource.Get_Shop_Item_Weight(item._rare);
        }
        float random_Weight = Random.Range(0, all_Weight);

        float current_Weight = 0;
        foreach(Item item in items)
        {
            if (item._isCore) continue;
            current_Weight = current_Weight + _shop_Resource.Get_Shop_Item_Weight(item._rare);
            if(current_Weight > random_Weight)
            {
                //return new Item(item._id, _data_Item);
                return item;
            }
        }

        return null;
    }

    //商店UI//
    public void Set_Shop_UI_Manager(Scene_Type scene_Type)
    {
        if(scene_Type == Scene_Type.WorldScene)
        {
            Debug.Log("初始化商店UI管理器");
            _shop_UI_Manager = Shop_UI_Manager.Instance;
            if (_shop_UI_Manager == null)
            {
                Debug.Log("商店UI管理器未初始化");
                return;
            }
            _shop_UI_Manager.Set_Shop_UI_Manager(_shop_Items_MaxNum);
        }

    }
    public void Initialize_Shop_UI_Slots(Scene_Type scene_Type)
    {
        if(scene_Type == Scene_Type.WorldScene)
        {
            if (_shop_UI_Manager == null) return;
            _shop_UI_Manager.Initialize_Shop_UI_Slots();
        }

    }

    public void Update_Shop_UI_Slots()
    {
        //if(scene_Type == Scene_Type.WorldScene)
        //{

        //}
        if (_shop_UI_Manager == null) return;
        _shop_UI_Manager.Update_Shop_UI_Slots(_shop_Items);
    }
    private void Update_Shop_UI_Slot(int index)
    {
        if (_shop_UI_Manager == null) return;
        _shop_UI_Manager.Update_Shop_UI_Slot(_shop_Items, index);
    }
    public void Update_Shop_SelectTip(int index)
    {
        if (_shop_UI_Manager == null) return;
        _shop_UI_Manager.Update_Shop_SelectTip(index);
    }
    private void Reset_Shop_SelectTip()
    {
        if (_shop_UI_Manager == null) return;
        _shop_UI_Manager.Reset_SelectTip();
    }

    //商店信息管理
    public void Set_Shop_Information_Manager(Scene_Type scene_Type)
    {
        if(scene_Type == Scene_Type.WorldScene)
        {
            Debug.Log("初始化商店信息管理器");
            if(Shop_Information_Manager.Instance == null)
            {
                Debug.Log("商店信息管理器未初始化");
                return;
            }
            Shop_Information_Manager.Instance.Set_Shop_Information_Manager();
            Debug.Log("初始化商店信息管理器成功");
        }
    }
    public void Update_Shop_Resource_Information(Scene_Type scene_Type)
    {
        if(scene_Type == Scene_Type.WorldScene)
        {
            if (Shop_Information_Manager.Instance == null) return;
            Shop_Information_Manager.Instance.Update_Shop_Resource_Information(_shop_Resource);
        }
    }
    public void Update_Item_Information(Item item)
    {
        if(Shop_Information_Manager.Instance == null) return;
        Shop_Information_Manager.Instance.Update_Information_Item(item);
    }
    public void Reset_Item_Information()
    {
        if (Shop_Information_Manager.Instance == null) return;
        Shop_Information_Manager.Instance.Reset_Information_Item();
    }
    public void Update_Shop_Item_Price_Information(Item item)
    {
        if (Shop_Information_Manager.Instance == null) return;
        Shop_Information_Manager.Instance.Update_Shop_Item_Price_Information(_shop_Resource, item);
    }
    public void Reset_Shop_Item_Price_Information()
    {
        if (Shop_Information_Manager.Instance == null) return;
        Shop_Information_Manager.Instance.Reset_Shop_Item_Price_Information();
    }
    public void Update_Shop_Refresh_Price_Information()
    {
        if (Shop_Information_Manager.Instance == null) return;
        Shop_Information_Manager.Instance.Update_Shop_Refresh_Price_Information(_shop_Resource);
        //if (scene_Type == Scene_Type.WorldScene)
        //{

        //}
    }
    public void Update_Shop_LevelUp_Price_Information()
    {
        //if (scene_Type == Scene_Type.WorldScene)
        //{

        //}
        if (Shop_Information_Manager.Instance == null) return;
        Shop_Information_Manager.Instance.Update_Shop_LevelUp_Price_Information(_shop_Resource);
    }
    //数据保存与读取
    public void Save_Shop_Data()
    {
        if (Data_Manager.Instance == null) return;
        Data_Manager.Instance.Save_Shop_Data(_shop_Items, _shop_Resource);
    }

    public void Load_Shop_Data()
    {
        if(Data_Manager.Instance == null) return;
        if (Data_Manager.Instance.Check_Load_Data())
        {
            Data_Manager.Instance.Load_Shop_Data(_shop_Items, _shop_Resource);
        }
        else
        {
            Refresh_Shop();
        }
    }
}
