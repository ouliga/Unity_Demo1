using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DataState
{
    NewData, OldData
}
public class Data
{
    public DataState _dataState { get; private set; }
    public string _name;
    //游戏数据
    public int _data_Game_Difficulty_Degree;
    //仓库数据
    public int[,] _data_Inventory_Bag;
    public int[,] _data_Inventory_EquipBag_Cube;
    public int[,] _data_Inventory_EquipBag_Equipment;
    //商店数据
    public int[,] _data_Shop_Items;
    public int _data_Shop_Money_Num;
    public int _data_Shop_Component_Num;
    public int _data_Shop_Level;
    //地图数据
    public int[,] _data_Map_Node_Type;
    public bool[,,,] _data_Map_Node_Line;
    public bool[,] _data_Map_Node_Controller_Active;

    public Data(string name)
    {
        _name = name;
    }
    public void Update_DataState(DataState dataState)
    {
        _dataState = dataState;
    }
    //游戏数据
    public void Save_Game_Difficulty_Degree_Data(int data_Game_Difficulty_Degree)
    {
        _data_Game_Difficulty_Degree = data_Game_Difficulty_Degree;
    }
    public void Load_Game_Difficulty_Degree_Data(Game_Manager_Buff game_Manager_Buff)
    {
        if(game_Manager_Buff == null)
        {
            Debug.LogError("无法读取游戏难度");
            return;
        }
        game_Manager_Buff.Config_Buff_By_Game_Difficulty_Degree(_data_Game_Difficulty_Degree);
        Debug.Log($"游戏难度为：{_data_Game_Difficulty_Degree}级");
    }
    //背包数据
    public void Save_Inventory_Bag_Data(Inventory_Slot[] inventory_Bag)
    {
        if(inventory_Bag == null)
        {
            Debug.Log("无法保存背包数据");
            return;
        }
        int inventory_Bag_MaxNum = inventory_Bag.Length;
        _data_Inventory_Bag = new int[inventory_Bag_MaxNum, 2];
        for (int i = 0; i < inventory_Bag_MaxNum; i++)
        {
            if (inventory_Bag[i]._item == null)
            {
                _data_Inventory_Bag[i, 0] = -1;
            }
            else
            {
                //[i,0]保存物品ID
                _data_Inventory_Bag[i, 0] = inventory_Bag[i]._item._id;
                //[i,1]保存物品数量
                _data_Inventory_Bag[i, 1] = inventory_Bag[i]._item_Num;
            }
        }
    }
    public void Load_Inventory_Bag_Data(Inventory_Slot[] inventory_Bag)
    {
        if (inventory_Bag == null) return;
        Data_SO data_Item = Game_Manager.Instance.Get_Data_Item();
        if (data_Item == null) return;

        int inventory_Bag_MaxNum = inventory_Bag.Length;
        for (int i = 0; i < inventory_Bag_MaxNum; i++)
        {
            if (_data_Inventory_Bag[i, 0] == -1) continue;
            Item item = new Item(_data_Inventory_Bag[i, 0], data_Item);
            int item_Num = _data_Inventory_Bag[i, 1];
            inventory_Bag[i]._item = item;
            inventory_Bag[i]._item_Num = item_Num;
        }
    }
    public void Save_Inventory_EquipBag_Data(Inventory_Slot[,] inventory_EquipBag_Cube, Inventory_Slot[,] inventory_EquipBag_Equipment)
    {
        if(inventory_EquipBag_Cube == null || inventory_EquipBag_Equipment == null)
        {
            Debug.Log("无法保存装备栏数据");
            return;
        }
        int inventory_Equip_MaxNum_X = inventory_EquipBag_Cube.GetLength(0);
        int inventory_Equip_MaxNum_Y = inventory_EquipBag_Cube.GetLength(1);
        _data_Inventory_EquipBag_Cube = new int[inventory_Equip_MaxNum_X, inventory_Equip_MaxNum_Y];
        _data_Inventory_EquipBag_Equipment = new int[inventory_Equip_MaxNum_X, inventory_Equip_MaxNum_Y];
        for (int i = 0; i < inventory_Equip_MaxNum_X; i++)
        {
            for (int j = 0; j < inventory_Equip_MaxNum_Y; j++)
            {
                if (inventory_EquipBag_Cube[i, j]._item != null)
                {
                    _data_Inventory_EquipBag_Cube[i, j] = inventory_EquipBag_Cube[i, j]._item._id;
                }
                else
                {
                    _data_Inventory_EquipBag_Cube[i, j] = -1;
                }

                if (inventory_EquipBag_Equipment[i, j]._item != null)
                {
                    _data_Inventory_EquipBag_Equipment[i, j] = inventory_EquipBag_Equipment[i, j]._item._id;
                }
                else
                {
                    _data_Inventory_EquipBag_Equipment[i, j] = -1;
                }
            }
        }
    }
    public void Load_Inventory_EquipBag_Data(Inventory_Slot[,] inventory_EquipBag_Cube, Inventory_Slot[,] inventory_EquipBag_Equipment)
    {
        if (inventory_EquipBag_Cube == null || inventory_EquipBag_Equipment == null) return;
        int inventory_EquipBag_MaxNum_X = inventory_EquipBag_Cube.GetLength(0);
        int inventory_EquipBag_MaxNum_Y = inventory_EquipBag_Cube.GetLength(1);
        Data_SO data_Item = Game_Manager.Instance.Get_Data_Item();

        for (int i = 0; i < inventory_EquipBag_MaxNum_X; i++)
        {
            for (int j = 0; j < inventory_EquipBag_MaxNum_Y; j++)
            {
                if (_data_Inventory_EquipBag_Cube[i, j] != -1)
                {
                    Item item = new Item(_data_Inventory_EquipBag_Cube[i, j], data_Item);
                    inventory_EquipBag_Cube[i,j]._item = item;
                    inventory_EquipBag_Cube[i, j]._item_Num = 1;
                }
                if (_data_Inventory_EquipBag_Equipment[i, j] != -1)
                {
                    Item item = new Item(_data_Inventory_EquipBag_Equipment[i, j], data_Item);
                    inventory_EquipBag_Equipment[i, j]._item = item;
                    inventory_EquipBag_Equipment[i, j]._item_Num = 1;
                }
            }
        }
    }
    //商店数据
    public void Save_Shop_Data(Inventory_Slot[] shop_Items,Shop_Resource shop_Resource)
    {
        if(shop_Items == null || shop_Resource == null)
        {
            Debug.Log("商店数据未传输完整");
            return;
        }
        int shop_Items_MaxNum = shop_Items.Length;
        _data_Shop_Items = new int[shop_Items_MaxNum,2];
        for (int i = 0; i < shop_Items_MaxNum; i++)
        {
            if (shop_Items[i]._item == null)
            {
                _data_Shop_Items[i, 0] = -1;
            }
            else
            {
                //[i,0]保存物品ID
                _data_Shop_Items[i, 0] = shop_Items[i]._item._id;
                //[i,1]保存物品数量
                _data_Shop_Items[i, 1] = shop_Items[i]._item_Num;
            }
        }
        _data_Shop_Money_Num = shop_Resource._money_Num;
        _data_Shop_Component_Num = shop_Resource._component_Num;
        _data_Shop_Level = shop_Resource._shop_Level;
        Debug.Log("商店数据已保存");
    }
    public void Load_Shop_Data(Inventory_Slot[] shop_Items,Shop_Resource shop_Resource)
    {
        int shop_Items_MaxNum = shop_Items.Length;
        Data_SO data_Item = Game_Manager.Instance.Get_Data_Item();
        for (int i = 0; i < shop_Items_MaxNum; i++)
        {
            if (_data_Shop_Items[i, 0] == -1) continue;
            shop_Items[i]._item = new Item(_data_Shop_Items[i, 0], data_Item);
            shop_Items[i]._item_Num = _data_Shop_Items[i, 1];
        }
        shop_Resource.Load_Data(_data_Shop_Level,_data_Shop_Money_Num, _data_Shop_Component_Num);
    }
    //存储地图数据
    public void Save_Map_Data(Map_Node[,] map_Nodes)
    {
        if (map_Nodes == null) return;
        int layer_MaxNum = map_Nodes.GetLength(0);
        int layer_Node_MaxNum = map_Nodes.GetLength(1);
        if(_data_Map_Node_Type == null)
        {
            _data_Map_Node_Type = new int[layer_MaxNum, layer_Node_MaxNum];
        }
        if(_data_Map_Node_Line == null)
        {
            _data_Map_Node_Line = new bool[layer_MaxNum, layer_Node_MaxNum, layer_MaxNum, layer_Node_MaxNum];
        }
        if(_data_Map_Node_Controller_Active == null)
        {
            _data_Map_Node_Controller_Active = new bool[layer_MaxNum, layer_Node_MaxNum];
        }
        for(int layer=0; layer<layer_MaxNum; layer++)
        {
            for(int index=0;index<layer_Node_MaxNum; index++)
            {
                Map_Node map_Node = map_Nodes[layer, index];
                _data_Map_Node_Type[layer, index] = Get_RoomType_Index(map_Node._roomType);
                foreach(Map_Node map_Node_Next in map_Node._nextNodes)
                {
                    _data_Map_Node_Line[layer, index, map_Node_Next._layer, map_Node_Next._index] = true;
                }
                _data_Map_Node_Controller_Active[layer, index] = map_Node._controller_Active;


            }
        }
    }
    //读取地图数据
    public void Load_Map_Data(Map_Node[,] map_Nodes)
    {
        if (_dataState == DataState.NewData) return;
        if (map_Nodes == null) return;
        int layer_MaxNum = map_Nodes.GetLength(0);
        int layer_Node_MaxNum = map_Nodes.GetLength(1);
        //读取地图节点类型和节点激活状态
        for(int layer = 0; layer < layer_MaxNum; layer++)
        {
            for(int index=0;index < layer_Node_MaxNum; index++)
            {
                Map_Node map_Node = map_Nodes[layer, index];
                RoomType roomType = Get_RoomType_ByIndex(_data_Map_Node_Type[layer,index]);
                map_Node.Change_RoomType(roomType);

                bool controller_Active = _data_Map_Node_Controller_Active[layer, index];
                map_Node.Change_Active_Controller(controller_Active);
            }
        }
        //读取节点间的连接关系
        for (int layer = 0; layer < layer_MaxNum-1; layer++)
        {
            for (int index = 0; index < layer_Node_MaxNum; index++)
            {
                Map_Node map_Node_Current = map_Nodes[layer, index];
                for(int index_Next = 0; index_Next < layer_Node_MaxNum; index_Next++)
                {
                    Map_Node map_Node_Next = map_Nodes[layer+1, index_Next];
                    bool is_Connect = _data_Map_Node_Line[layer,index,layer+1,index_Next];
                    if(is_Connect)
                    {
                        map_Node_Current.AddNextRoom(map_Node_Next);
                    }
                }
            }
        }
        Debug.Log("读取地图数据");

    }
    private int Get_RoomType_Index(RoomType roomType)
    {
        if(roomType == RoomType.None) return 0;
        else if(roomType == RoomType.Start) return 1;
        else if(roomType == RoomType.Normal) return 2;
        else if(roomType == RoomType.Hard) return 3;
        else if(roomType == RoomType.Shop) return 4;
        else if(roomType == RoomType.Boss) return 5;
        return - 1;

    }
    private RoomType Get_RoomType_ByIndex(int index)
    {
        if(index == 0) return RoomType.None;
        else if(index == 1) return RoomType.Start;
        else if(index == 2) return RoomType.Normal;
        else if(index == 3) return RoomType.Hard;
        else if(index == 4) return RoomType.Shop;
        else if(index == 5) return RoomType.Boss;
        return RoomType.None;
    }
}
