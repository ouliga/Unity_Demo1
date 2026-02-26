using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI_Bag : MonoBehaviour
{
    private Inventory_UI_Name _inventory_UI_Name;
    //public string _inventory_Bag_Slot_Name { get; private set; }
    //public string _inventory_Bag_Slot_Item_Name { get; private set; }
    //public string _inventory_Bag_Slot_Num_Name { get; private set; }

    
    private int _inventory_Bag_MaxNum;
    private GameObject[] _inventory_UI_Bag_Slots;
    private GameObject _inventory_UI_Bag_Position;
    private GameObject _inventory_UI_Bag_Slot_Prefab;

    private Canvas _canvas;

    public void Initialize_Inventory_UI_Bag(int inventory_Bag_MaxNum,GameObject inventory_UI_Bag_Position,GameObject inventory_UI_Bag_Slot_Prefab, Canvas canvas)
    {
        _inventory_Bag_MaxNum = inventory_Bag_MaxNum;
        _inventory_UI_Bag_Slots = new GameObject[inventory_Bag_MaxNum];
        //Initialize_UI_Name();
        _inventory_UI_Bag_Position = inventory_UI_Bag_Position;
        _inventory_UI_Bag_Slot_Prefab = inventory_UI_Bag_Slot_Prefab;
        _canvas = canvas;
    }
    public void Initialize_UI_Name(Inventory_UI_Name inventory_UI_Name)
    {
        //_inventory_Bag_Slot_Name = "背包栏_格子";
        //_inventory_Bag_Slot_Item_Name = "格子_图标";
        //_inventory_Bag_Slot_Num_Name = "格子_数量";
        if(inventory_UI_Name == null)
        {
            Debug.LogError("UI名称未初始化");
            return;
        }
        _inventory_UI_Name = inventory_UI_Name;
    }
    public void Initialize_UI_Bag()
    {
        if (_inventory_UI_Bag_Slots == null)
        {
            Debug.Log("未初始化背包数组");
            return;
        }
        if (_inventory_UI_Bag_Position == null)
        {
            Debug.Log("未初始化背包栏UI位置");
            return;
        }
        Debug.Log("背包UI初始化");
        for (int i = 0; i < _inventory_Bag_MaxNum; i++)
        {
            _inventory_UI_Bag_Slots[i] = Instantiate(_inventory_UI_Bag_Slot_Prefab, _inventory_UI_Bag_Position.transform);
            _inventory_UI_Bag_Slots[i].name = _inventory_UI_Name._inventory_Bag_Slot_Name;

            Item_Drag item_Drag = _inventory_UI_Bag_Slots[i].transform.Find(_inventory_UI_Name._inventory_Bag_Slot_Item_Name).AddComponent<Item_Drag>();
            item_Drag.Initialize_Item_Drag(Bag_Type.bag, i, -1, -1, _canvas);
        }
        Debug.Log("背包UI初始化完成");
    }
    
    public void Update_UI_Bag_ByIndex(Inventory_Slot[] inventory_Bag, int index)
    {
        if (inventory_Bag == null)
        {
            Debug.Log("仓库背包未初始化，无法更新UI");
            return;
        }
        if(index < 0||index >= _inventory_Bag_MaxNum)
        {
            Debug.Log($"{index}超出了边界{_inventory_Bag_MaxNum}");
            return;
        }
        if (_inventory_UI_Bag_Slots[index].transform.Find(_inventory_UI_Name._inventory_Bag_Slot_Item_Name) == null || _inventory_UI_Bag_Slots[index].transform.Find(_inventory_UI_Name._inventory_Bag_Slot_Num_Name) == null)
        {
            Debug.Log("未找到对应UI");
            return;
        }
        GameObject slot_UI = _inventory_UI_Bag_Slots[index].transform.Find(_inventory_UI_Name._inventory_Bag_Slot_Item_Name).gameObject;
        GameObject slot_Num = _inventory_UI_Bag_Slots[index].transform.Find(_inventory_UI_Name._inventory_Bag_Slot_Num_Name).gameObject;
        Item item = inventory_Bag[index]._item;
        //激活图标
        if(item != null && !inventory_Bag[index]._is_Hidden)
        {
            slot_UI.SetActive(true);
            slot_Num.SetActive(true);
            //测试仓库隐藏数字
            if (inventory_Bag[index]._item_Num >= 100)
            {
                slot_Num.SetActive(false);
            }
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
    public void Update_UI_Bag(Inventory_Slot[] inventory_Bag)
    {
        for (int i = 0; i < _inventory_Bag_MaxNum; i++)
        {
            Update_UI_Bag_ByIndex(inventory_Bag, i);
        }
    }

}
