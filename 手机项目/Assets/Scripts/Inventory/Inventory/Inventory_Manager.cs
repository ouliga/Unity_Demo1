using UnityEngine;

public class Inventory_Manager : Singleton<Inventory_Manager>
{
    private Inventory_UI_Manager _inventory_UI_Manager;

    //背包
    private Inventory_Bag _inventory_Bag;
    //背包栏大小
    public int _inventory_Bag_MaxNum { get; private set; }
    //背包整理
    private Inventory_Bag_Sort _inventory_Bag_Sort;

    //装备栏大小
    public int _inventory_EquipBag_MaxNum_X { get; private set; }
    public int _inventory_EquipBag_MaxNum_Y { get; private set; }
    //当前装备栏
    private Inventory_EquipBag _inventory_EquipBag_Current;
    //玩家装备栏
    private Inventory_EquipBag _inventory_EquipBag_Player;
    //敌方装备栏
    private Inventory_EquipBag _inventory_EquipBag_Enemy;

    //仓库数据
    private Data _data_Inventory;

    private void OnEnable()
    {
        //仓库
        Scene_Manager.Instance._subscribe += Set_Inventory_Manager;
        //仓库UI
        Scene_Manager.Instance._subscribe += Set_Inventory_UI_Manager;
        Scene_Manager.Instance._subscribe += Initialize_UI;
        //Scene_Manager.Instance._subscribe += Update_UI_Inventory;
        //信息管理
        Scene_Manager.Instance._subscribe += Set_Inventory_Information_Manager;
        //Scene_Manager.Instance._subscribe += Show_Information_AirShip;
        //界面控制
        Scene_Manager.Instance._subscribe += Open_Inventory_Panel;
    }
    private void OnDisable()
    {
        Scene_Manager.Instance._subscribe -= Set_Inventory_Manager;
        Scene_Manager.Instance._subscribe -= Set_Inventory_UI_Manager;
        Scene_Manager.Instance._subscribe -= Initialize_UI;
        //Scene_Manager.Instance._subscribe -= Update_UI_Inventory;
        Scene_Manager.Instance._subscribe -= Set_Inventory_Information_Manager;
        //Scene_Manager.Instance._subscribe -= Show_Information_AirShip;
        Scene_Manager.Instance._subscribe -= Open_Inventory_Panel;
    }
    private void Open_Inventory_Panel(Scene_Type scene_Type)
    {
        if (scene_Type == Scene_Type.TestScene)
        {
            if (UI_Panel_Manager.Instance != null)
            {
                UI_Panel_Manager.Instance.Open_Inventory_Panel();
            }
        }
    }
    private void Set_Inventory_Manager(Scene_Type scene_Type)
    {
        if (scene_Type == Scene_Type.WorldScene)
        {
            if (!_has_Initialize)
            {
                Debug.Log("初始化仓库");
                Set_Inventory_Bag();
                Set_EquipBag();
                _has_Initialize = true;
                Debug.Log("初始化仓库成功");
            }
            else
            {
                Debug.Log("仓库已经初始化完成");
            }
        }
        else if (scene_Type == Scene_Type.TestScene)
        {
            if (!_has_Initialize)
            {
                Debug.Log("初始化测试仓库");
                Set_Bag_Test();
                Initialize_EquipBag_Test();
                _has_Initialize = true;
                Debug.Log("测试仓库已完成");
            }   
        }
        else if(scene_Type == Scene_Type.StartScene)
        {
            Destroy(gameObject);
        }
    }
    /// 仓库UI
    private void Set_Inventory_UI_Manager(Scene_Type scene_Type)
    {
        if (scene_Type == Scene_Type.WorldScene || scene_Type == Scene_Type.TestScene)
        {
            if (_inventory_UI_Manager == null)
            {
                _inventory_UI_Manager = Inventory_UI_Manager.Instance;
            }
            _inventory_UI_Manager.Set_Inventory_UI_Manager();
        }
    }
    private void Initialize_UI(Scene_Type scene_Type)
    {
        if (scene_Type == Scene_Type.WorldScene || scene_Type == Scene_Type.TestScene)
        {
            Debug.Log("初始化仓库UI");
            Initialize_UI_Bag();
            Initialize_UI_EquipBag();
            Debug.Log("初始化仓库UI完成");
        }
    }
    public void Update_UI_Inventory()
    {
        Debug.Log("更新仓库UI");
        Update_UI_Bag();
        Update_UI_CubeBag();
        Update_UI_EquipmentBag();
        Debug.Log("更新仓库UI成功");
    }
    //仓库背包//
    private void Set_Inventory_Bag()
    {
        if (_inventory_Bag == null)
        {
            _inventory_Bag = new Inventory_Bag();
        }
        _inventory_Bag_MaxNum = 50;
        _inventory_Bag.Set_Bag(_inventory_Bag_MaxNum);
    }
    private void Set_Bag_Test()
    {
        if (_inventory_Bag == null)
        {
            _inventory_Bag = new Inventory_Bag();
        }
        _inventory_Bag.Set_Bag_Test();
    }
    //获得背包物品
    public Item Get_Item_Bag(int index)
    {
        if (_inventory_Bag == null) return null;
        return _inventory_Bag.Get_Item_Bag(index);
    }
    //背包添加或移除物品
    public bool Check_AddItem_Bag(Item item, int num)
    {
        if (_inventory_Bag == null) return false;
        return _inventory_Bag.Check_AddItem_Bag(item, num);
    }
    public void AddItem_Bag(Item item, int num)
    {
        if (_inventory_Bag == null) return;
        _inventory_Bag.AddItem_Bag(item, num);
    }
    public bool Check_RemoveItem_Bag(int index, int num)
    {
        if (_inventory_Bag == null) return false;
        return _inventory_Bag.Check_RemoveItem_Bag(index, num);
    }
    public void RemoveItem_Bag(int index, int num)
    {
        if (_inventory_Bag == null) return;
        _inventory_Bag.RemoveItem_Bag(index, num);
    }
    //背包整理
    public void Sort_Bag_Fill_Empty()
    {
        if (_inventory_Bag == null) return;
        _inventory_Bag.Sort_Bag_Fill_Empty();

    }
    public void Sort_Bag_By_ID()
    {
        if (_inventory_Bag == null) return;
        _inventory_Bag.Sort_Bag_By_ID();
    }
    public void Sort_Bag_By_Rare(int rare)
    {
        if (_inventory_Bag == null) return;
        _inventory_Bag.Sort_Bag_By_Rare(rare);
    }
    //背包UI
    private void Initialize_UI_Bag()
    {
        if (_inventory_Bag != null)
        {
            Debug.Log("初始化背包UI");
            _inventory_Bag.Initialize_Inventory_UI_Bag();
        }
    }
    private void Update_UI_Bag()
    {
        if (_inventory_Bag == null) return;
        _inventory_Bag.Update_UI_Bag();
    }
    private void Update_UI_Bag_By_Index(int index)
    {
        if (_inventory_Bag == null) return;
        _inventory_Bag.Update_UI_Bag_By_Index(index);
    }
    public void Update_UI_Shop_Bag_SelectTip(int index)
    {
        if (_inventory_Bag != null)
        {
            _inventory_Bag.Update_UI_Shop_Bag_Slot_SelectTip(index);
        }
    }
    public void Reset_UI_Shop_Bag_SelectTip()
    {
        if (_inventory_Bag != null)
        {
            _inventory_Bag.Reset_UI_Shop_Bag_SelectTip();
        }
    }

    //装备栏//

    private void Set_EquipBag()
    {
        _inventory_EquipBag_MaxNum_X = 9;
        _inventory_EquipBag_MaxNum_Y = 9;

        Debug.Log("初始化玩家装备仓库");
        _inventory_EquipBag_Player = new Inventory_EquipBag(GroupType.Player);
        _inventory_EquipBag_Player.Initialize_Inventory_EquipBag(_inventory_EquipBag_MaxNum_X, _inventory_EquipBag_MaxNum_Y);
        Debug.Log("玩家装备仓库初始化成功");

        //默认的装备仓库为玩家仓库
        _inventory_EquipBag_Current = _inventory_EquipBag_Player;
    }
    private void Initialize_EquipBag_Test()
    {
        Set_EquipBag();

        Debug.Log("初始化敌方装备仓库");
        _inventory_EquipBag_Enemy = new Inventory_EquipBag(GroupType.Enemy);
        _inventory_EquipBag_Enemy.Initialize_Inventory_EquipBag(_inventory_EquipBag_MaxNum_X, _inventory_EquipBag_MaxNum_Y);
        Debug.Log("敌方装备仓库初始化成功");
    }

    public Item Get_Item_CubeBag(int index_X, int index_Y)
    {
        if (_inventory_EquipBag_Current == null) return null;
        return _inventory_EquipBag_Current.Get_Item_Cube(index_X, index_Y);
    }
    public Item Get_Item_Equipment(int index_X, int index_Y)
    {
        if (_inventory_EquipBag_Current == null) return null;
        return _inventory_EquipBag_Current.Get_Item_Equipment(index_X, index_Y);

    }

    // 方块与装备的添加或移除 //

    public bool Check_AddItem_Cube(int index_X, int index_Y, Item item)
    {
        if(_inventory_EquipBag_Current == null)
        {
            Debug.Log("当前装备栏未初始化");
            return false;
        }
        return _inventory_EquipBag_Current.Check_AddItem_Cube(index_X, index_Y, item);

    }
    public void AddItem_Cube(int index_X,int index_Y,Item item)
    {
        if (_inventory_EquipBag_Current == null) return;
        _inventory_EquipBag_Current.AddItem_Cube(index_X,index_Y, item);
        _inventory_EquipBag_Current.Update_UI_CubeBag_ByIndex(index_X, index_Y);
    }
    public bool Check_AddItem_Equipment(int index_X, int index_Y, Item item)
    {

        if( _inventory_EquipBag_Current == null)
        {
            Debug.Log("当前装备栏未初始化");
            return false;
        }
        return _inventory_EquipBag_Current.Check_AddItem_Equipment(index_X, index_Y, item);

    }
    public void AddItem_Equipment(int index_X, int index_Y, Item item)
    {
        if (_inventory_EquipBag_Current == null) return;
        _inventory_EquipBag_Current.AddItem_Equipment(index_X, index_Y, item);
        _inventory_EquipBag_Current.Update_UI_EquipmentBag_ByIndex(index_X, index_Y);
    }
    public bool Check_RemoveItem_Cube(int index_X, int index_Y)
    {
        if(_inventory_EquipBag_Current == null) return false;
        return _inventory_EquipBag_Current.Check_RemoveItem_Cube(index_X, index_Y);
    }
    public void RemoveItem_Cube(int index_X,int index_Y)
    {
        if(_inventory_EquipBag_Current == null) return;
        _inventory_EquipBag_Current.RemoveItem_Cube(index_X, index_Y);
        _inventory_EquipBag_Current.Update_UI_CubeBag_ByIndex(index_X, index_Y);
    }
    public bool Check_RemoveItem_Equipment(int index_X, int index_Y)
    {
        if( _inventory_EquipBag_Current == null)return false;
        return _inventory_EquipBag_Current.Check_RemoveItem_Equipment(index_X,index_Y);
    }
    public void RemoveItem_Equipment(int index_X,int index_Y)
    {
        if(_inventory_EquipBag_Current == null)return;
        _inventory_EquipBag_Current.RemoveItem_Equipment(index_X, index_Y);
        _inventory_EquipBag_Current.Update_UI_EquipmentBag_ByIndex(index_X, index_Y);
    }
    /// <summary>
    /// 装备或物品的交换
    /// </summary>
    public void SwapItem_CubeToCube(int indexX_1,int indexY_1,int indexX_2,int indexY_2)
    {
        Item item1 = _inventory_EquipBag_Current.Get_Item_Cube(indexX_1,indexY_1);
        Item item2 = _inventory_EquipBag_Current.Get_Item_Cube(indexX_2,indexY_2);
        _inventory_EquipBag_Current.RemoveItem_Cube(indexX_1,indexY_1);
        _inventory_EquipBag_Current.RemoveItem_Cube(indexX_2,indexY_2);
        _inventory_EquipBag_Current.AddItem_Cube(indexX_1 ,indexY_1,item2);
        _inventory_EquipBag_Current.AddItem_Cube(indexX_2, indexY_2, item1);
        _inventory_EquipBag_Current.Update_UI_CubeBag_ByIndex(indexX_1, indexY_1);
        _inventory_EquipBag_Current.Update_UI_CubeBag_ByIndex(indexX_2, indexY_2);
        Debug.Log("("+indexX_1+","+indexY_1+")" + "位置方块与" + "(" + indexX_2 + "," + indexY_2 + ")" + "位置方块交换");
    }
    public void SwapItem_EquipToEquip(int indexX_1, int indexY_1, int indexX_2, int indexY_2)
    {
        Item item1 = _inventory_EquipBag_Current.Get_Item_Equipment(indexX_1, indexY_1);
        Item item2 = _inventory_EquipBag_Current.Get_Item_Equipment(indexX_2, indexY_2);
        _inventory_EquipBag_Current.RemoveItem_Equipment(indexX_1, indexY_1);
        _inventory_EquipBag_Current.RemoveItem_Equipment(indexX_2, indexY_2);
        _inventory_EquipBag_Current.AddItem_Equipment(indexX_1, indexY_1, item2);
        _inventory_EquipBag_Current.AddItem_Equipment(indexX_2, indexY_2, item1);
        _inventory_EquipBag_Current.Update_UI_EquipmentBag_ByIndex(indexX_1, indexY_1);
        _inventory_EquipBag_Current.Update_UI_EquipmentBag_ByIndex(indexX_2, indexY_2);
        Debug.Log("(" + indexX_1 + "," + indexY_1 + ")" + "位置装备与" + "(" + indexX_2 + "," + indexY_2 + ")" + "位置装备交换");
    }

    //切换装备栏
    public void Switch_EquipBag_Test(GroupType groupType)
    {
        if (!Check_EquipBag_Current_Valid())
        {
            return;
        }
        Debug.Log($"尝试切换装备栏({groupType.ToString()})");
        switch (groupType)
        {
            case GroupType.Player:
                _inventory_EquipBag_Current = _inventory_EquipBag_Player;
                break;
            case GroupType.Enemy:
                _inventory_EquipBag_Current = _inventory_EquipBag_Enemy;
                break;
        }

        //切换装备栏后更新UI
        Update_UI_EquipmentBag();
        Update_UI_CubeBag();
        //检测飞船架构的合理性
        Check_EquipBag_Current_Valid();
        //信息栏更新
        Update_Information_AirShip_Current();
        //Show_Information_AirShip_Current();

        Debug.Log("切换装备栏成功");
    }
    //检测装备栏合理性
    public bool Check_EquipBag_Current_Valid()
    {
        if (_inventory_EquipBag_Current == null)
        {
            return false;
        }
        if (!_inventory_EquipBag_Current.Check_EquipBag_Valid())
        {
            _inventory_EquipBag_Current.Show_IllegalTips_EquipBag();
            return false;
        }
        return true;
    }
    public bool Check_EquipBag_Both_Valid()
    {
        if(_inventory_EquipBag_Player == null || _inventory_EquipBag_Enemy == null)
        {
            Debug.Log("玩家或敌人装备栏未初始化");
            return false;
        }
        if((!_inventory_EquipBag_Player.Check_EquipBag_Valid()) || (!_inventory_EquipBag_Enemy.Check_EquipBag_Valid()))
        {
            _inventory_EquipBag_Current.Show_IllegalTips_EquipBag();
            return false;
        }
        return true;
    }
    //装备栏UI
    private void Initialize_UI_EquipBag()
    {
        if(_inventory_EquipBag_Current != null)
        {
            _inventory_EquipBag_Current.Initialize_UI_EquipBag();
        }
    }
    private void Update_UI_CubeBag()
    {
        if(_inventory_EquipBag_Current != null)
        {
            _inventory_EquipBag_Current.Update_UI_CubeBag();
        }
    }
    private void Update_UI_EquipmentBag()
    {
        if(_inventory_EquipBag_Current != null)
        {
            _inventory_EquipBag_Current.Update_UI_EquipmentBag();
        }
    }
    //显示或隐藏放置提示
    public void ShowPlaceTips_EquipToEquip(Item item)
    {
        if(_inventory_EquipBag_Current != null)
        {
            _inventory_EquipBag_Current.ShowPlaceTips_EquipToEquip(item);
        }
    }
    public void ShowPlaceTips_BagToEquip(Item item)
    {
        if(_inventory_EquipBag_Current != null)
        {
            _inventory_EquipBag_Current.ShowPlaceTips_BagToEquip(item);
        }
    }
    public void HidePlaceTips_EquipBag()
    {
        if (_inventory_EquipBag_Current != null)
        {
            _inventory_EquipBag_Current.HidePlaceTips_EquipBag();
        }
    }
    //显示或隐藏替代提示
    public void ShowReplaceTips(Item item)
    {
        if(_inventory_EquipBag_Current != null)
        {
            _inventory_EquipBag_Current.ShowReplaceTips(item);
        }
    }
    public void HideReplaceTips_EquipBag()
    {
        if (_inventory_EquipBag_Current != null)
        {
            _inventory_EquipBag_Current .HideReplaceTips_EquipBag();
        }
    }
    ///仓库信息管理///

    //初始化仓库信息管理器
    private void Set_Inventory_Information_Manager(Scene_Type scene_Type)
    {
        if (scene_Type == Scene_Type.WorldScene || scene_Type == Scene_Type.TestScene)
        {
            Debug.Log("初始化仓库信息管理器");
            if (Inventory_Information_Manager.Instance == null) return;
            Inventory_Information_Manager.Instance.Set_Inventory_Information_Manager();
            Debug.Log("初始化仓库信息管理器成功");
        }
    }
    //更新并显示当前飞船属性信息
    public void Update_Information_AirShip_Current()
    {
        if (_inventory_EquipBag_Current == null) return;
        _inventory_EquipBag_Current.Update_AirShip_Attribute_Information();
        _inventory_EquipBag_Current.Show_Information_AirShip();
    }
    ///仓库数据的保存与加载///
    public void Save_Inventory_Data()
    {
        if (_inventory_Bag != null)
        {
            _inventory_Bag.Save_Inventory_Bag_Data();
        }
        if(_inventory_EquipBag_Current != null)
        {
            _inventory_EquipBag_Current.Save_Inventory_EquipBag_Data();
        }
    }

    //保存飞船数据
    public Data_AirShip Save_AirShip_Data()
    {
        Data_AirShip data_AirShip = new Data_AirShip();

        if (Check_EquipBag_Current_Valid())
        {
            _inventory_EquipBag_Current.Save_AirShip_EquipBag_Data(data_AirShip);
        }
        else
        {
            Debug.Log("当前飞船架构不合理");
            data_AirShip = null;
        }
        return data_AirShip;
    }
    //读取敌方飞船数据
    public void Load_AirShip_Data_Enemy(Data_AirShip data_AirShip)
    {
        if(_inventory_EquipBag_Enemy == null)
        {
            _inventory_EquipBag_Enemy = new Inventory_EquipBag(GroupType.Enemy);
            _inventory_EquipBag_Enemy.Initialize_Inventory_EquipBag(_inventory_EquipBag_MaxNum_X, _inventory_EquipBag_MaxNum_Y);
        }
        _inventory_EquipBag_Enemy.Load_AirShip_EquipBag_Data(data_AirShip);
    }

    /// 战役管理器 ///
    public void Update_Airship_Component_GameObjects(AirShip_Controller airShip_Controller, GroupType groupType)
    {
        if(groupType == GroupType.Player)
        {
            if (_inventory_EquipBag_Player == null)
            {
                Debug.Log("玩家装备栏未初始化");
                return;
            }
            _inventory_EquipBag_Player.Update_Airship_Component_GameObjects(airShip_Controller);
        }
        else if(groupType == GroupType.Enemy)
        {
            if(_inventory_EquipBag_Enemy == null)
            {
                Debug.Log("敌方装备栏未初始化");
                return;
            }
            _inventory_EquipBag_Enemy.Update_Airship_Component_GameObjects(airShip_Controller);
        }

    }
    //初始化飞船组件的脚本
    public void Initialize_AirShip_Component_Scripts(AirShip_Controller airShip_Controller, GroupType groupType)
    {
        if (groupType == GroupType.Player)
        {
            if (_inventory_EquipBag_Player == null)
            {
                Debug.Log("玩家装备栏未初始化");
                return;
            }
            _inventory_EquipBag_Player.Initialize_AirShip_Component_Scripts(airShip_Controller);
        }
        else if (groupType == GroupType.Enemy)
        {
            if (_inventory_EquipBag_Enemy == null)
            {
                Debug.Log("敌方装备栏未初始化");
                return;
            }
            _inventory_EquipBag_Enemy.Initialize_AirShip_Component_Scripts(airShip_Controller);
        }
    }

}
