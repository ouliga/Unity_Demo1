using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Bag
{
    private Data_SO _data_Item;
    //背包栏大小
    public int _inventory_Bag_MaxNum { get; private set; }
    //背包
    private Inventory_Slot[] _inventory_Bag;
    //背包整理
    private Inventory_Bag_Sort _inventory_Bag_Sort;
    //背包UI
    private Inventory_UI_Manager _inventory_UI_Manager;
    public Inventory_Bag()
    {
        if(Game_Manager.Instance != null)
        {
            _data_Item = Game_Manager.Instance.Get_Data_Item();
        }
        else
        {
            Debug.Log("游戏管理器未初始化");
        }
        
        _inventory_Bag_Sort = new Inventory_Bag_Sort();
        _inventory_UI_Manager = Inventory_UI_Manager.Instance;
    }
    //初始化背包
    public void Set_Bag(int inventory_Bag_MaxNum)
    {
        Debug.Log($"初始化背包({inventory_Bag_MaxNum})");
        _inventory_Bag_MaxNum = inventory_Bag_MaxNum;
        _inventory_Bag = new Inventory_Slot[_inventory_Bag_MaxNum];
        for (int i = 0; i < _inventory_Bag_MaxNum; i++)
        {
            _inventory_Bag[i] = new Inventory_Slot();
        }
        Load_Inventory_Bag_Data();
        Debug.Log("背包初始化完成");
    }
    public void Set_Bag_Test()
    {
        int rare_Default = -1;
        _inventory_Bag = Initialize_Bag_Test_By_Rare(rare_Default);
        _inventory_Bag_MaxNum = _inventory_Bag.Length;
    }
    private Inventory_Slot[] Initialize_Bag_Test_By_Rare(int rare)
    {
        List<Item> items = new();
        if (Game_Manager.Instance != null)
        {
            _data_Item = Game_Manager.Instance.Get_Data_Item();
        }
        if(_data_Item == null)
        {
            Debug.Log("物品数据未初始化");
            return null;
        }
        foreach (ExcelData excelData in _data_Item._data)
        {
            if (rare == -1)
            {
                Item item = new Item(excelData._id, _data_Item);
                items.Add(item);
            }
            else if (excelData._rare == rare)
            {
                Item item = new Item(excelData._id, _data_Item);
                items.Add(item);
            }
        }
        Inventory_Slot[] inventory_Bag = new Inventory_Slot[items.Count];
        int item_Num_Default = 999;
        for (int i = 0; i < items.Count; i++)
        {
            inventory_Bag[i] = new Inventory_Slot();
            inventory_Bag[i]._item = items[i];
            inventory_Bag[i]._item_Num = item_Num_Default;
        }
        return inventory_Bag;
    }
    //获得物品
    public Item Get_Item_Bag(int index)
    {
        if (index < 0 || index >= _inventory_Bag_MaxNum)
        {
            Debug.Log("获得的物品超过背包界限");
            return null;
        }
        return _inventory_Bag[index]._item;
    }
    //添加物品
    public bool Check_AddItem_Bag(Item item, int num)
    {
        for (int i = 0; i < _inventory_Bag_MaxNum; i++)
        {
            if (_inventory_Bag[i]._item == null)
            {
                continue;
            }
            if (_inventory_Bag[i]._item._id == item._id)
            {
                return true;
            }
        }
        for (int i = 0; i < _inventory_Bag_MaxNum; i++)
        {
            if (_inventory_Bag[i]._item == null)
            {
                return true;
            }
        }
        return false;
    }
    public void AddItem_Bag(Item item, int num)
    {
        for (int i = 0; i < _inventory_Bag_MaxNum; i++)
        {
            if (_inventory_Bag[i]._item == null)
            {
                continue;
            }

            if (_inventory_Bag[i]._item._id == item._id)
            {
                _inventory_Bag[i]._item_Num += num;
                Update_UI_Bag_By_Index(i);
                Debug.Log("物品成功重叠添加到第" + i + "格" + ";当前数量为：" + _inventory_Bag[i]._item_Num);
                return;
            }
        }
        for (int i = 0; i < _inventory_Bag_MaxNum; i++)
        {
            if (_inventory_Bag[i]._item == null)
            {
                _inventory_Bag[i]._item = item;
                _inventory_Bag[i]._item_Num += num;
                Update_UI_Bag_By_Index(i);
                Debug.Log("物品成功添加到第" + i + "格");
                return;
            }
        }
    }
    public bool Check_RemoveItem_Bag(int index, int num)
    {
        if (_inventory_Bag[index]._item == null)
        {
            Debug.Log("当前位置(" + index + ")没有物品无法移除");
            return false;
        }
        if (num > _inventory_Bag[index]._item_Num)
        {
            Debug.Log("当前位置(" + index + ")无法移除过多的物品");
            return false;
        }
        return true;
    }
    public void RemoveItem_Bag(int index, int num)
    {
        if (_inventory_Bag[index]._item == null)
        {
            return;
        }
        if (num > _inventory_Bag[index]._item_Num)
        {
            return;
        }
        if (num == _inventory_Bag[index]._item_Num)
        {
            _inventory_Bag[index].ResetSlot();
        }
        if (num < _inventory_Bag[index]._item_Num)
        {
            _inventory_Bag[index]._item_Num -= num;
        }
        Update_UI_Bag_By_Index(index);
        Debug.Log("移除了第" + index + "个物品(" + num + "件)");
    }
    //整理背包
    public void Sort_Bag_Fill_Empty()
    {
        if (_inventory_Bag_Sort != null)
        {
            _inventory_Bag = _inventory_Bag_Sort.Sort_Bag_Fill_Empty(_inventory_Bag);
            Update_UI_Bag();
        }
    }
    public void Sort_Bag_By_ID()
    {
        if (_inventory_Bag_Sort != null)
        {
            _inventory_Bag = _inventory_Bag_Sort.Sort_Bag_By_ID(_inventory_Bag);
            Update_UI_Bag();
        }
    }
    public void Sort_Bag_By_Rare(int rare)
    {
        if (_inventory_Bag_Sort != null)
        {
            _inventory_Bag = _inventory_Bag_Sort.Sort_Bag_By_Rare(rare, _inventory_Bag);
            Update_UI_Bag();
        }
    }
    //背包UI
    public void Initialize_Inventory_UI_Bag()
    {
        if (_inventory_UI_Manager == null)
        {
            _inventory_UI_Manager = Inventory_UI_Manager.Instance;
        }
        _inventory_UI_Manager.Initialize_Inventory_UI_Bag(_inventory_Bag_MaxNum);
        Intialize_UI_Shop_Bag();
    }
    private void Intialize_UI_Shop_Bag()
    {
        if (_inventory_UI_Manager == null)
        {
            _inventory_UI_Manager = Inventory_UI_Manager.Instance;
        }
        _inventory_UI_Manager.Initialize_Inventory_UI_Shop_Bag(_inventory_Bag_MaxNum);
    }
    public void Update_UI_Bag()
    {
        if (_inventory_UI_Manager == null)
        {
            _inventory_UI_Manager = Inventory_UI_Manager.Instance;
        }
        _inventory_UI_Manager.Update_UI_Bag(_inventory_Bag);
        Update_UI_Shop_Bag();
    }
    public void Update_UI_Bag_By_Index(int index)
    {
        if (_inventory_UI_Manager == null)
        {
            _inventory_UI_Manager = Inventory_UI_Manager.Instance;
        }
        _inventory_UI_Manager.Update_UI_Bag_ByIndex(_inventory_Bag, index);
        Update_UI_Shop_Bag_By_Index(index);
    }
    public void Update_UI_Shop_Bag()
    {
        if (_inventory_UI_Manager == null)
        {
            _inventory_UI_Manager = Inventory_UI_Manager.Instance;
        }
        _inventory_UI_Manager.Update_UI_Shop_Bag(_inventory_Bag);
    }
    public void Update_UI_Shop_Bag_By_Index(int index)
    {
        if (_inventory_UI_Manager == null)
        {
            _inventory_UI_Manager = Inventory_UI_Manager.Instance;
        }
        _inventory_UI_Manager.Update_UI_Shop_Bag_By_Index(_inventory_Bag, index);
    }
    //商店背包
    public void Update_UI_Shop_Bag_Slot_SelectTip(int index)
    {
        if (_inventory_UI_Manager == null)
        {
            _inventory_UI_Manager = Inventory_UI_Manager.Instance;
        }
        _inventory_UI_Manager.Update_UI_Shop_Bag_Slot_SelectTip(index);
    }
    public void Reset_UI_Shop_Bag_SelectTip()
    {
        if (_inventory_UI_Manager == null)
        {
            _inventory_UI_Manager = Inventory_UI_Manager.Instance;
        }
        _inventory_UI_Manager.Reset_UI_Shop_Bag_SelectTip();
    }
    //保存和读取数据
    public void Save_Inventory_Bag_Data()
    {
        if (Data_Manager.Instance == null) return;
        Data_Manager.Instance.Save_Inventory_Bag_Data(_inventory_Bag);
    }
    private void Load_Inventory_Bag_Data()
    {
        if(Data_Manager.Instance == null)
        {
            Config_Inventory_Bag_Data_Default();
            Debug.Log("数据管理器未初始化");
            return;
        }

        if (Data_Manager.Instance.Check_Load_Data())
        {
            Data_Manager.Instance.Load_Inventory_Bag_Data(_inventory_Bag);
        }
        else
        {
            Config_Inventory_Bag_Data_Default();
        }
    }
    private void Config_Inventory_Bag_Data_Default()
    {
        Item item0 = new Item(0, _data_Item);
        Item item1 = new Item(1, _data_Item);
        Item item2 = new Item(2, _data_Item);
        Item item3 = new Item(3, _data_Item);

        AddItem_Bag(item0, 1);
        AddItem_Bag(item1, 2);
        AddItem_Bag(item2, 2);
        AddItem_Bag(item3, 2);
    }
}
