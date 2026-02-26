using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI_Shop_Bag : MonoBehaviour
{
    private Inventory_UI_Name _inventory_UI_Name;
    //public string _shop_Bag_Slot_Name { get; private set; }
    //public string _shop_Bag_Slot_Item_Name { get; private set; }
    //public string _shop_Bag_Slot_Num_Name { get; private set; }

    private int _inventory_Bag_MaxNum;
    private GameObject _shop_UI_Bag_Position;
    private GameObject _shop_UI_Bag_Slot_Prefab;
    private GameObject[] _shop_UI_Bag_Slots;

    private Shop_UI_Manager _shop_UI_Manager;
    public void Initialize_Inventory_UI_Shop_Bag(int inventory_Bag_MaxNum,GameObject shop_UI_Bag_Position, GameObject shop_UI_Bag_Slot_Prefab)
    {
        _inventory_Bag_MaxNum = inventory_Bag_MaxNum;

        _shop_UI_Bag_Slots = new GameObject[_inventory_Bag_MaxNum];
        _shop_UI_Bag_Position = shop_UI_Bag_Position;
        _shop_UI_Bag_Slot_Prefab = shop_UI_Bag_Slot_Prefab;
        _shop_UI_Manager = Shop_UI_Manager.Instance;
        //Initialize_UI_Name();
    }
    public void Initialize_UI_Name(Inventory_UI_Name inventory_UI_Name)
    {
        if (inventory_UI_Name == null)
        {
            Debug.LogError("UI名称未初始化");
            return;
        }
        _inventory_UI_Name = inventory_UI_Name;

        //_shop_Bag_Slot_Name = "商店_背包_格子";
        //_shop_Bag_Slot_Item_Name = "格子_图标";
        //_shop_Bag_Slot_Num_Name = "格子_数量";
    }
    public void Initialize_UI_Shop_Bag()
    {
        if (_shop_UI_Bag_Position == null)
        {
            Debug.Log("商店仓库UI位置未初始化");
            return;
        }
        if (_shop_UI_Bag_Slot_Prefab == null)
        {
            Debug.Log("商店背包格子预制体未初始化");
            return;
        }
        for (int i = 0; i < _inventory_Bag_MaxNum; i++)
        {
            _shop_UI_Bag_Slots[i] = Instantiate(_shop_UI_Bag_Slot_Prefab, _shop_UI_Bag_Position.transform);
            _shop_UI_Bag_Slots[i].name = _inventory_UI_Name._shop_Bag_Slot_Name;
            Item_Sell item_Sell = _shop_UI_Bag_Slots[i].transform.Find(_inventory_UI_Name._shop_Bag_Slot_Item_Name).gameObject.AddComponent<Item_Sell>();
            item_Sell.Initialize_Item_Sell(SelectType.SellItem,i);
        }
        Initialize_UI_Shop_Bag_SelectTips();
    }
    public void Update_UI_Shop_Bag(Inventory_Slot[] inventory_Bag)
    {
        Debug.Log("尝试更新商店仓库UI");
        for (int i = 0; i < _inventory_Bag_MaxNum; i++)
        {
            Update_UI_Shop_Bag_By_Index(inventory_Bag, i);
        }
        Debug.Log("更新商店仓库UI成功");
    }
    public void Update_UI_Shop_Bag_By_Index(Inventory_Slot[] inventory_Bag, int index)
    {
        if (_shop_UI_Bag_Slots[index] == null)
        {
            Debug.Log("背包格子未初始化");
            return;
        }
        if (_shop_UI_Bag_Slots[index].transform.Find(_inventory_UI_Name._shop_Bag_Slot_Item_Name) == null || _shop_UI_Bag_Slots[index].transform.Find(_inventory_UI_Name._shop_Bag_Slot_Num_Name) == null)
        {
            Debug.LogWarning("未找到对应UI");
            return;
        }

        GameObject slot_UI = _shop_UI_Bag_Slots[index].transform.Find(_inventory_UI_Name._shop_Bag_Slot_Item_Name).gameObject;
        GameObject slot_Num = _shop_UI_Bag_Slots[index].transform.Find(_inventory_UI_Name._shop_Bag_Slot_Num_Name).gameObject;

        //激活图标
        if (inventory_Bag[index]._item != null && !inventory_Bag[index]._is_Hidden)
        {
            slot_UI.SetActive(true);
            slot_Num.SetActive(true);
            //更新格子的图标
            slot_UI.GetComponent<Image>().sprite = inventory_Bag[index]._item._display_UI;
            //更新格子的数量
            slot_Num.GetComponent<TextMeshProUGUI>().text = inventory_Bag[index]._item_Num + "";
        }
        else
        {
            slot_UI.SetActive(false);
            slot_Num.SetActive(false);
        }

    }

    //商店背包选择提示//

    public void Initialize_UI_Shop_Bag_SelectTips()
    {
        if (_shop_UI_Manager != null)
        {
            _shop_UI_Manager.Initialize_Shop_Bag_UI_Slot_SelectTips(_shop_UI_Bag_Slots);
        }
    }
    public void Update_UI_Shop_Bag_SelectTip(int index)
    {
        if (_shop_UI_Manager != null)
        {
            _shop_UI_Manager.Update_Shop_Bag_SelectTip(index);
        }
    }
    public void Reset_UI_Shop_Bag_SelectTip()
    {
        if (_shop_UI_Manager != null)
        {
            _shop_UI_Manager.Reset_SelectTip();
        }
    }
}
