using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_EquipBag
{
    private Data_SO _data_Item;
    public GroupType _groupType { get; private set; }
    //装备栏大小
    public int _inventory_EquipBag_MaxNum_X { get; private set; }
    public int _inventory_EquipBag_MaxNum_Y { get; private set; }
    //装备栏
    public Inventory_Slot[,] _inventory_EquipBag_Cube { get; private set; }
    public Inventory_Slot[,] _inventory_EquipBag_Equipment { get; private set; }
    //装备栏UI
    private Inventory_UI_Manager _inventory_UI_Manager;
    //装备栏合理性检测
    private BFS_Mark _BFS_Mark;
    //飞船属性
    public Inventory_AirShip_Attribute_Information _airShip_Attribute_Information { get; private set; }
    public Inventory_EquipBag(GroupType groupType)
    {
        if(Game_Manager.Instance != null)
        {
            _data_Item = Game_Manager.Instance.Get_Data_Item();
        }
        _groupType = groupType;
        _inventory_UI_Manager = Inventory_UI_Manager.Instance;
        _airShip_Attribute_Information = new Inventory_AirShip_Attribute_Information(_groupType);
    }
    public void Initialize_Inventory_EquipBag(int inventory_EquipBag_MaxNum_X, int inventory_EquipBag_MaxNum_Y)
    {
        Debug.Log("初始化装备仓库");
        _inventory_EquipBag_MaxNum_X = inventory_EquipBag_MaxNum_X;
        _inventory_EquipBag_MaxNum_Y = inventory_EquipBag_MaxNum_Y;
        _inventory_EquipBag_Cube = new Inventory_Slot[_inventory_EquipBag_MaxNum_X, _inventory_EquipBag_MaxNum_Y];
        _inventory_EquipBag_Equipment = new Inventory_Slot[_inventory_EquipBag_MaxNum_X, _inventory_EquipBag_MaxNum_Y];


        for (int i = 0; i < _inventory_EquipBag_MaxNum_X; i++)
        {
            for(int j = 0;j<_inventory_EquipBag_MaxNum_Y; j++)
            {
                _inventory_EquipBag_Cube[i, j] = new Inventory_Slot();
                _inventory_EquipBag_Equipment[i,j] = new Inventory_Slot();
            }
        }
        //初始化BFS
        _BFS_Mark = new BFS_Mark(inventory_EquipBag_MaxNum_X, inventory_EquipBag_MaxNum_Y);
        Load_Inventory_EquipBag_Data();
        Debug.Log("装备仓库初始化成功");
    }
    public Item Get_Item_Cube(int index_X, int index_Y)
    {
        if (_inventory_EquipBag_Cube[index_X, index_Y] == null) return null;
        return _inventory_EquipBag_Cube[index_X, index_Y]._item;
    }
    public Item Get_Item_Equipment(int index_X, int index_Y)
    {
        if (_inventory_EquipBag_Equipment[index_X,index_Y] == null) return null;
        return _inventory_EquipBag_Equipment[index_X, index_Y]._item;
    }
    //装备栏的添加和移除
    public bool Check_AddItem_Cube(int index_X, int index_Y, Item item)
    {
        if (item._type != Item_Type.Cube)
        {
            Debug.Log("无法添加非方块物品");
            return false;
        }
        if (_inventory_EquipBag_Cube[index_X, index_Y]._item == null)
        {
            return true;
        }
        Debug.Log("无法添加方块");
        return false;
    }
    public void AddItem_Cube(int index_X, int index_Y, Item item)
    {
        if (item._type != Item_Type.Cube)
        {
            Debug.Log("无法添加非方块物品");
            return;
        }
        Debug.Log("尝试添加方块物品id：" + item._id + "于(" + index_X + "," + index_Y + ")");
        if (_inventory_EquipBag_Cube[index_X, index_Y]._item == null)
        {
            _inventory_EquipBag_Cube[index_X, index_Y]._item = item;
            _inventory_EquipBag_Cube[index_X, index_Y]._item_Num = 1;
            //Update_UI_CubeBag_ByIndex(index_X, index_Y);
            Debug.Log("添加方块成功");
            return;
        }   
    }
    public bool Check_AddItem_Equipment(int index_X, int index_Y, Item item)
    {
        if (item._type != Item_Type.Equipment)
        {
            Debug.Log("无法添加非装备物品");
            return false;
        }
        if (_inventory_EquipBag_Equipment[index_X, index_Y]._item == null)
        {
            return true;
        }
        Debug.Log("无法添加装备");
        return false;
    }
    public void AddItem_Equipment(int index_X, int index_Y, Item item)
    {
        if (item._type != Item_Type.Equipment)
        {
            return;
        }
        Debug.Log("尝试添加装备物品id：" + item._id + "于(" + index_X + "," + index_Y + ")");
        if (_inventory_EquipBag_Equipment[index_X, index_Y]._item == null)
        {
            _inventory_EquipBag_Equipment[index_X, index_Y]._item = item;
            _inventory_EquipBag_Equipment[index_X, index_Y]._item_Num = 1;
            //Update_UI_EquipmentBag_ByIndex(index_X, index_Y);
            Debug.Log("装备成功");
            return;
        }
        Debug.Log("无法添加装备");
    }
    public bool Check_RemoveItem_Cube(int index_X, int index_Y)
    {
        if (_inventory_EquipBag_Cube[index_X, index_Y]._item == null) return false;
        return true;
    }
    public void RemoveItem_Cube(int index_X, int index_Y)
    {
        if (_inventory_EquipBag_Cube[index_X, index_Y]._item == null) return;
        _inventory_EquipBag_Cube[index_X, index_Y].ResetSlot();

        //Update_UI_CubeBag_ByIndex(index_X, index_Y);
        Debug.Log("成功移除(" + index_X + "," + index_Y + ")位置上的方块");
    }
    public bool Check_RemoveItem_Equipment(int index_X, int index_Y)
    {
        if (_inventory_EquipBag_Equipment[index_X, index_Y]._item == null) return false;
        return true;
    }
    public void RemoveItem_Equipment(int index_X, int index_Y)
    {
        if (_inventory_EquipBag_Equipment[index_X, index_Y]._item == null) return;
        _inventory_EquipBag_Equipment[index_X, index_Y].ResetSlot();

        //Update_UI_EquipmentBag_ByIndex(index_X, index_Y);
        Debug.Log("成功移除(" + index_X + "," + index_Y + ")位置上的装备");
    }
    //装备栏UI
    public void Initialize_UI_EquipBag()
    {
        if (_inventory_UI_Manager == null)
        {
            _inventory_UI_Manager = Inventory_UI_Manager.Instance;
        }
        _inventory_UI_Manager.Initialize_UI_EquipBag(_inventory_EquipBag_MaxNum_X,_inventory_EquipBag_MaxNum_Y);
    }
    public void Update_UI_CubeBag_ByIndex(int index_X, int index_Y)
    {
        if (_inventory_UI_Manager == null)
        {
            _inventory_UI_Manager = Inventory_UI_Manager.Instance;
        }
        _inventory_UI_Manager.Update_UI_CubeBag_ByIndex(_inventory_EquipBag_Cube, index_X, index_Y);
    }
    public void Update_UI_CubeBag()
    {
        if (_inventory_UI_Manager == null)
        {
            _inventory_UI_Manager = Inventory_UI_Manager.Instance;
        }
        _inventory_UI_Manager.Update_UI_CubeBag(_inventory_EquipBag_Cube);
    }
    public void Update_UI_EquipmentBag_ByIndex(int index_X, int index_Y)
    {
        if (_inventory_UI_Manager == null)
        {
            _inventory_UI_Manager = Inventory_UI_Manager.Instance;
        }
        _inventory_UI_Manager.Update_UI_EquipmentBag_ByIndex(_inventory_EquipBag_Equipment, index_X, index_Y);
    }
    public void Update_UI_EquipmentBag()
    {
        if (_inventory_UI_Manager == null)
        {
            _inventory_UI_Manager = Inventory_UI_Manager.Instance;
        }
        _inventory_UI_Manager.Update_UI_EquipmentBag(_inventory_EquipBag_Equipment);
    }

    //提示
    public void ShowPlaceTips_EquipToEquip(Item item)
    {
        if (_inventory_UI_Manager == null)
        {
            _inventory_UI_Manager = Inventory_UI_Manager.Instance;
        }
        if (_inventory_UI_Manager != null)
        {
            _inventory_UI_Manager.ShowPlaceTips_EquipToEquip(_inventory_EquipBag_Cube,item);
        }
    }
    public void ShowPlaceTips_BagToEquip(Item item)
    {
        if (_inventory_UI_Manager == null)
        {
            _inventory_UI_Manager = Inventory_UI_Manager.Instance;
        }
        if (_inventory_UI_Manager != null)
        {
            _inventory_UI_Manager.ShowPlaceTips_BagToEquip(_inventory_EquipBag_Cube, _inventory_EquipBag_Equipment, item);
        }
    }
    public void HidePlaceTips_EquipBag()
    {
        if (_inventory_UI_Manager == null)
        {
            _inventory_UI_Manager = Inventory_UI_Manager.Instance;
        }
        if (_inventory_UI_Manager != null)
        {
            _inventory_UI_Manager.HidePlaceTips_EquipBag();
        }
    }
    public void ShowReplaceTips(Item item)
    {
        if (_inventory_UI_Manager == null)
        {
            _inventory_UI_Manager = Inventory_UI_Manager.Instance;
        }
        if (_inventory_UI_Manager != null)
        {
            _inventory_UI_Manager.ShowReplaceTips(_inventory_EquipBag_Cube, _inventory_EquipBag_Equipment, item);
        }
    }
    public void HideReplaceTips_EquipBag()
    {
        if (_inventory_UI_Manager == null)
        {
            _inventory_UI_Manager = Inventory_UI_Manager.Instance;
        }
        if (_inventory_UI_Manager != null)
        {
            _inventory_UI_Manager.HideReplaceTips_EquipBag();
        }
    }



    //检测装备栏合理性
    public bool Check_EquipBag_Valid()
    {
        if (_BFS_Mark == null)
        {
            Debug.Log("装备栏的BFS未初始化");
            return false;
        }
        //更新BFS
        _BFS_Mark.Update_BFS_EquipBag(_inventory_EquipBag_Cube, _inventory_EquipBag_Equipment);
        //检测核心是否合规，确保核心存在且唯一
        if(!_BFS_Mark.Check_Core_EquipBag(_inventory_EquipBag_Cube, _inventory_EquipBag_Equipment))
        {
            return false;
        }
        //从核心处开始探索所有路径
        _BFS_Mark.Mark_FromCore();
        //检测所有方块是否有路径抵达核心
        if (!_BFS_Mark.Check_Valid_EquipBag(_inventory_EquipBag_Cube))
        {
            return false;
        }
        
        return true;
    }
    public void Show_IllegalTips_EquipBag()
    {
        if( _BFS_Mark != null)
        {
            _BFS_Mark.Show_IllegalTips_EquipBag(_inventory_EquipBag_Cube);
        }
    }
    //飞船信息
    public void Update_AirShip_Attribute_Information()
    {
        Debug.Log("尝试更新飞船属性信息");
        if (_airShip_Attribute_Information == null) return;
        _airShip_Attribute_Information.Update_Attribute_Information(_inventory_EquipBag_Cube, _inventory_EquipBag_Equipment);
        Debug.Log("更新飞船属性信息成功");
    }

    public void Show_Information_AirShip()
    {
        if (Inventory_Information_Manager.Instance == null) return;
        //更新显示当前背包的飞船信息属性
        Inventory_Information_Manager.Instance.Update_Information_AirShip(_airShip_Attribute_Information);
    }

    //保存和读取信息

    //仓库数据
    public void Save_Inventory_EquipBag_Data()
    {
        if(Data_Manager.Instance == null) return;
        Data_Manager.Instance.Save_Inventory_EquipBag_Data(_inventory_EquipBag_Cube, _inventory_EquipBag_Equipment);
    }
    private void Load_Inventory_EquipBag_Data()
    {
        if(Data_Manager.Instance == null) return;
        if (Data_Manager.Instance.Check_Load_Data())
        {
            Data_Manager.Instance.Load_Inventory_EquipBag_Data(_inventory_EquipBag_Cube, _inventory_EquipBag_Equipment);
        }
    }
    //飞船数据
    public void Save_AirShip_EquipBag_Data(Data_AirShip data_AirShip)
    {
        if (data_AirShip == null) return;
        data_AirShip.Save_AirShip_Data(_inventory_EquipBag_Cube, _inventory_EquipBag_Equipment);
    }
    public void Load_AirShip_EquipBag_Data(Data_AirShip data_AirShip)
    {
        if (data_AirShip == null) return;
        int[,] data_AirShip_Cube = data_AirShip._data_AirShip_Cube;
        int[,] data_AirShip_Equipment = data_AirShip._data_AirShip_Equipment;
        for (int i = 0; i < _inventory_EquipBag_MaxNum_X; i++)
        {
            for (int j = 0; j < _inventory_EquipBag_MaxNum_Y; j++)
            {
                if (data_AirShip_Cube[i, j] != -1)
                {
                    Item item = new Item(data_AirShip_Cube[i, j], _data_Item);
                    AddItem_Cube(i, j, item);
                }
                if (data_AirShip_Equipment[i, j] != -1)
                {
                    Item item = new Item(data_AirShip_Equipment[i, j], _data_Item);
                    AddItem_Equipment(i, j, item);
                }
            }
        }
    }

    //更新飞船组件（战役）
    public void Update_Airship_Component_GameObjects(AirShip_Controller airShip_Controller)
    {
        if(airShip_Controller == null)
        {
            Debug.Log("飞船控制器未初始化");
            return;
        }
        airShip_Controller.Update_AirShip_Component_GameObjects(_inventory_EquipBag_Cube, _inventory_EquipBag_Equipment);
    }
    public void Initialize_AirShip_Component_Scripts(AirShip_Controller airShip_Controller)
    {
        if (airShip_Controller == null)
        {
            Debug.Log("飞船控制器未初始化");
            return;
        }
        airShip_Controller.Initialize_AirShip_Component_Scripts(_inventory_EquipBag_Cube, _inventory_EquipBag_Equipment);
    }
}
