using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Shop_UI_Manager : Singleton<Shop_UI_Manager>
{
    private void Awake() { }

    private int _shop_Items_MaxNum;

    public string _shop_Slot_Name { get; private set; }
    public string _shop_Slot_Item_Name { get; private set; }
    public string _shop_Slot_Num_Name { get ; private set; }
    //商店UI位置
    [SerializeField] private GameObject _shop_UI_Position;
    [SerializeField] private GameObject _shop_UI_Slot_Prefab;

    private GameObject[] _shop_UI_Slots;

    //商店选择提示
    public string _shop_Slot_SelectTip_Name {  get; private set; }
    [SerializeField] private GameObject _shop_UI_Slot_SelectTip_Prefab;

    private Shop_UI_SelectTip _shop_UI_SelectTip;
    private int _select_Buy_Item_Index = -1;

    private void Initialize_Name()
    {
        _shop_Slot_Name = "商店_格子";
        _shop_Slot_Item_Name = "格子_图标";
        _shop_Slot_Num_Name = "格子_数量";
        _shop_Slot_SelectTip_Name = "格子_选择提示";
    }

    public void Set_Shop_UI_Manager(int shop_Items_MaxNum)
    {
        Debug.Log("初始化商店UI管理器");

        Initialize_Name();
        _shop_Items_MaxNum = shop_Items_MaxNum;
        _shop_UI_Slots = new GameObject[shop_Items_MaxNum];
        _shop_UI_SelectTip = gameObject.AddComponent<Shop_UI_SelectTip>();
        _shop_UI_SelectTip.Initialize_Shop_UI_SelectTip(_shop_UI_Slot_SelectTip_Prefab);
    }
    public void Initialize_Shop_UI_Slots()
    {
        if(_shop_UI_Position == null)
        {
            Debug.Log("商店位置未初始化");
            return;
        }
        if(_shop_UI_Slot_Prefab == null)
        {
            Debug.Log("商店格子预制体未初始化");
            return;
        }
        for(int i = 0; i < _shop_Items_MaxNum; i++)
        {
            _shop_UI_Slots[i] = Instantiate(_shop_UI_Slot_Prefab, _shop_UI_Position.transform);
            _shop_UI_Slots[i].name = _shop_Slot_Name;
            Item_Buy item_Buy = _shop_UI_Slots[i].transform.Find(_shop_Slot_Item_Name).AddComponent<Item_Buy>();
            item_Buy.Initialize_Item_Bag(SelectType.BuyItem,i);

        }
        _shop_UI_SelectTip.Initialize_Shop_UI_Slot_SelectTips(_shop_UI_Slots);
    }
    public void Initialize_Shop_Bag_UI_Slot_SelectTips(GameObject[] shop_Bag_UI_Slots)
    {
        if(_shop_UI_SelectTip != null)
        {
            _shop_UI_SelectTip.Initialize_Shop_Bag_UI_Slot_SelectTips(shop_Bag_UI_Slots);
        }
    }
    public void Update_Shop_UI_Slots(Inventory_Slot[] shop_Items)
    {
        int length = shop_Items.Length;
        for(int i = 0;i < length; i++)
        {
            Update_Shop_UI_Slot(shop_Items, i);
        }
    }
    public void Update_Shop_UI_Slot(Inventory_Slot[] shop_Items,int index)
    {
        if (shop_Items == null)
        {
            Debug.Log("商品列表未初始化");
            return;
        }
        if(index >= _shop_Items_MaxNum)
        {
            Debug.Log($"索引{index}超出UI列表上界");
            return;
        }
        if (_shop_UI_Slots[index].transform.Find(_shop_Slot_Item_Name) == null || _shop_UI_Slots[index].transform.Find(_shop_Slot_Num_Name) == null)
        {
            Debug.Log("未找到对应的UI");
            return;
        }
        GameObject slot_Item_UI = _shop_UI_Slots[index].transform.Find(_shop_Slot_Item_Name).gameObject;
        GameObject slot_Num_UI = _shop_UI_Slots[index].transform.Find(_shop_Slot_Num_Name).gameObject;

        if (shop_Items[index]._item == null)
        {
            slot_Item_UI.SetActive(false);
            slot_Num_UI.SetActive(false);
        }
        else
        {
            slot_Item_UI.SetActive(true);
            slot_Num_UI.SetActive(true);
            //更新格子的图标
            Debug.Log($"更新商店图标(Index:{index},Id:{shop_Items[index]._item._id})");
            slot_Item_UI.GetComponent<Image>().sprite = shop_Items[index]._item._display_UI;
            //更新格子的数量
            slot_Num_UI.GetComponent<TextMeshProUGUI>().text = shop_Items[index]._item_Num + "";
        }
        
    }

    public void Update_Shop_SelectTip(int index)
    {
        if(_shop_UI_SelectTip != null)
        {
            _shop_UI_SelectTip.Update_Shop_SelectTip(index);
        }
    }
    public void Update_Shop_Bag_SelectTip(int index)
    {
        if(_shop_UI_SelectTip != null)
        {
            _shop_UI_SelectTip.Update_Shop_Bag_SelectTip(index);
        }
    }
    public void Reset_SelectTip()
    {
        if(_shop_UI_SelectTip != null)
        {
            _shop_UI_SelectTip.Reset_SelectTip();
        }
    }
}
