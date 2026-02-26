using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI_EquipBag : MonoBehaviour
{
    private Canvas _canvas;

    private Inventory_UI_Name _inventory_UI_Name;
    //public string _inventory_EquipBag_Slot_Name { get; private set; }
    //public string _inventory_CubeBag_Item_Name { get; private set; }
    //public string _inventory_EquipBag_Item_Name { get; private set; }

    //public string _inventory_EquipBag_PlaceTip_Name { get; private set; }
    //public string _inventory_EquipBag_ReplaceTip_Name { get; private set; }
    //public string _inventory_EquipBag_IllegalTip_Name { get; private set; }

    private GameObject _inventory_UI_EquipBag_Position;
    private GameObject _inventory_UI_EquipBag_Slot_Prefab;
    private GameObject _inventory_UI_EquipBag_PlaceTip_Prefab;
    private GameObject _inventory_UI_EquipBag_ReplaceTip_Prefab;
    private GameObject _inventory_UI_EquipBag_IllegalTip_Prefab;

    private int _inventory_EquipBag_MaxNum_X;
    private int _inventory_EquipBag_MaxNum_Y;
    private GameObject[,] _inventory_UI_EquipBag_Slots;
    private GameObject[,] _inventory_UI_EquipBag_PlaceTips;
    private GameObject[,] _inventory_UI_EquipBag_ReplaceTips;
    private GameObject[,] _inventory_UI_EquipBag_IllegalTips;

    public void Initialize_Inventory_UI_EquipBag(int inventory_EquipBag_MaxNum_X, int inventory_EquipBag_MaxNum_Y,GameObject inventory_UI_EquipBag_Position, GameObject inventory_UI_EquipBag_Slot_Prefab, GameObject inventory_UI_EquipBag_PlaceTip_Prefab, GameObject inventory_UI_EquipBag_ReplaceTip_Prefab, GameObject inventory_UI_EquipBag_IllegalTip_Prefab,Canvas canvas)
    {
        _inventory_EquipBag_MaxNum_X = inventory_EquipBag_MaxNum_X;
        _inventory_EquipBag_MaxNum_Y = inventory_EquipBag_MaxNum_Y;
        _inventory_UI_EquipBag_Slots = new GameObject[_inventory_EquipBag_MaxNum_X, _inventory_EquipBag_MaxNum_Y];
        _inventory_UI_EquipBag_PlaceTips = new GameObject[_inventory_EquipBag_MaxNum_X, _inventory_EquipBag_MaxNum_Y];
        _inventory_UI_EquipBag_ReplaceTips = new GameObject[_inventory_EquipBag_MaxNum_X, _inventory_EquipBag_MaxNum_Y];
        _inventory_UI_EquipBag_IllegalTips = new GameObject[_inventory_EquipBag_MaxNum_X, _inventory_EquipBag_MaxNum_Y];

        _inventory_UI_EquipBag_Position = inventory_UI_EquipBag_Position;
        _inventory_UI_EquipBag_Slot_Prefab = inventory_UI_EquipBag_Slot_Prefab;
        _inventory_UI_EquipBag_PlaceTip_Prefab = inventory_UI_EquipBag_PlaceTip_Prefab;
        _inventory_UI_EquipBag_ReplaceTip_Prefab = inventory_UI_EquipBag_ReplaceTip_Prefab;
        _inventory_UI_EquipBag_IllegalTip_Prefab = inventory_UI_EquipBag_IllegalTip_Prefab;

        _canvas = canvas;

        //Initialize_UI_Name();
    }
    public void Initialize_UI_Name(Inventory_UI_Name inventory_UI_Name)
    {
        if(inventory_UI_Name == null)
        {
            Debug.LogError("UI名称未初始化");
            return;
        }
        _inventory_UI_Name = inventory_UI_Name;
        //_inventory_EquipBag_Slot_Name = "装备栏_格子";
        //_inventory_CubeBag_Item_Name = "格子_方块图标";
        //_inventory_EquipBag_Item_Name = "格子_装备图标";
        //_inventory_EquipBag_PlaceTip_Name = "格子_放置提示";
        //_inventory_EquipBag_ReplaceTip_Name = "格子_替代提示";
        //_inventory_EquipBag_IllegalTip_Name = "格子_违规提示";
    }
    public void Initialize_UI_EquipBag()
    {
        if (_inventory_UI_EquipBag_Slots == null || _inventory_UI_EquipBag_PlaceTips == null || _inventory_UI_EquipBag_ReplaceTips == null || _inventory_UI_EquipBag_IllegalTips == null)
        {
            Debug.Log("未初始化装备栏数组");
            return;
        }
        if (_inventory_UI_EquipBag_Position == null)
        {
            Debug.Log("未初始化装备栏UI");
            return;
        }
        if (_inventory_UI_EquipBag_PlaceTip_Prefab == null || _inventory_UI_EquipBag_ReplaceTip_Prefab == null || _inventory_UI_EquipBag_IllegalTip_Prefab == null)
        {
            Debug.Log("未初始化提示放置");
            return;
        }
        Debug.Log("装备栏UI初始化");
        for (int i = 0; i < _inventory_EquipBag_MaxNum_X; i++)
        {
            for (int j = 0; j < _inventory_EquipBag_MaxNum_Y; j++)
            {
                //初始化装备栏格子
                _inventory_UI_EquipBag_Slots[i, j] = Instantiate(_inventory_UI_EquipBag_Slot_Prefab, _inventory_UI_EquipBag_Position.transform);
                _inventory_UI_EquipBag_Slots[i, j].name = _inventory_UI_Name._inventory_EquipBag_Slot_Name;
                //初始化违规提示
                _inventory_UI_EquipBag_IllegalTips[i, j] = Instantiate(_inventory_UI_EquipBag_IllegalTip_Prefab, _inventory_UI_EquipBag_Slots[i, j].transform);
                _inventory_UI_EquipBag_IllegalTips[i, j].transform.SetAsLastSibling();
                _inventory_UI_EquipBag_IllegalTips[i, j].name = _inventory_UI_Name._inventory_EquipBag_IllegalTip_Name;
                //初始化放置提示
                _inventory_UI_EquipBag_PlaceTips[i, j] = Instantiate(_inventory_UI_EquipBag_PlaceTip_Prefab, _inventory_UI_EquipBag_Slots[i, j].transform);
                _inventory_UI_EquipBag_PlaceTips[i, j].transform.SetAsLastSibling();
                _inventory_UI_EquipBag_PlaceTips[i, j].name = _inventory_UI_Name._inventory_EquipBag_PlaceTip_Name;
                //初始化替代提示
                _inventory_UI_EquipBag_ReplaceTips[i, j] = Instantiate(_inventory_UI_EquipBag_ReplaceTip_Prefab, _inventory_UI_EquipBag_Slots[i, j].transform);
                _inventory_UI_EquipBag_ReplaceTips[i, j].transform.SetAsLastSibling();
                _inventory_UI_EquipBag_ReplaceTips[i, j].name = _inventory_UI_Name._inventory_EquipBag_ReplaceTip_Name;
                //初始化方块背包信息
                Item_Drag item_Drag_Cube = _inventory_UI_EquipBag_Slots[i, j].transform.Find(_inventory_UI_Name._inventory_CubeBag_Item_Name).AddComponent<Item_Drag>();
                item_Drag_Cube.Initialize_Item_Drag(Bag_Type.cubeBag, -1, i, j, _canvas);
                //初始化装备背包信息
                Item_Drag item_Drag_Equipment = _inventory_UI_EquipBag_Slots[i, j].transform.Find(_inventory_UI_Name._inventory_EquipBag_Item_Name).AddComponent<Item_Drag>();
                item_Drag_Equipment.Initialize_Item_Drag(Bag_Type.equipmentBag, -1, i, j, _canvas);
            }
        }
        Debug.Log("装备栏UI初始化完成");
    }
    public void Update_UI_CubeBag_ByIndex(Inventory_Slot[,] inventory_CubeBag, int index_X, int index_Y)
    {
        GameObject slot_UI = _inventory_UI_EquipBag_Slots[index_X, index_Y].transform.Find(_inventory_UI_Name._inventory_CubeBag_Item_Name).gameObject;
        if (slot_UI == null)
        {
            Debug.Log("未找到对应UI");
        }
        if (inventory_CubeBag[index_X, index_Y]._item == null)
        {
            slot_UI.SetActive(false);
            return;
        }
        else
        {
            slot_UI.SetActive(true);
        }
        slot_UI.GetComponent<Image>().sprite = inventory_CubeBag[index_X, index_Y]._item._display_UI;
    }
    public void Update_UI_CubeBag(Inventory_Slot[,] inventory_CubeBag)
    {
        for (int i = 0; i < _inventory_EquipBag_MaxNum_X; i++)
        {
            for (int j = 0; j < _inventory_EquipBag_MaxNum_Y; j++)
            {
                Update_UI_CubeBag_ByIndex(inventory_CubeBag, i, j);
            }
        }
    }
    public void Update_UI_EquipmentBag_ByIndex(Inventory_Slot[,] inventory_EquipmentBag, int index_X, int index_Y)
    {
        GameObject slot_UI = _inventory_UI_EquipBag_Slots[index_X, index_Y].transform.Find(_inventory_UI_Name._inventory_EquipBag_Item_Name).gameObject;
        if (slot_UI == null)
        {
            Debug.Log("未找到对应UI");
            return;
        }
        if (inventory_EquipmentBag[index_X, index_Y]._item == null)
        {
            slot_UI.SetActive(false);
            return;
        }
        else
        {
            slot_UI.SetActive(true);
        }
        slot_UI.GetComponent<Image>().sprite = inventory_EquipmentBag[index_X, index_Y]._item._display_UI;
    }
    public void Update_UI_EquipmentBag(Inventory_Slot[,] inventory_EquipmentBag)
    {
        for (int i = 0; i < _inventory_EquipBag_MaxNum_X; i++)
        {
            for (int j = 0; j < _inventory_EquipBag_MaxNum_Y; j++)
            {
                Update_UI_EquipmentBag_ByIndex(inventory_EquipmentBag, i, j);
            }
        }
    }
    //放置提示
    public void ShowPlaceTips_EquipToEquip(Inventory_Slot[,] inventory_CubeBag, Item item)
    {
        Debug.Log("显示提示放置");
        if (_inventory_UI_EquipBag_PlaceTips == null)
        {
            Debug.Log("提示词数组未初始化");
            return;
        }
        if (item == null)
        {
            Debug.Log("无法显示提示放置");
            return;
        }
        if (item._type == Item_Type.Cube)
        {
            for (int i = 0; i < _inventory_EquipBag_MaxNum_X; i++)
            {
                for (int j = 0; j < _inventory_EquipBag_MaxNum_Y; j++)
                {
                    _inventory_UI_EquipBag_PlaceTips[i, j].SetActive(true);
                }
            }
        }
        else if (item._type == Item_Type.Equipment)
        {
            for (int i = 0; i < _inventory_EquipBag_MaxNum_X; i++)
            {
                for (int j = 0; j < _inventory_EquipBag_MaxNum_Y; j++)
                {
                    if (inventory_CubeBag[i, j]._item != null)
                    {
                        _inventory_UI_EquipBag_PlaceTips[i, j].SetActive(true);
                    }
                }
            }
        }
        else
        {
            Debug.Log("该物品种类标签不存在");
        }
    }
    public void ShowPlaceTips_BagToEquip(Inventory_Slot[,] inventory_CubeBag, Inventory_Slot[,] inventory_EquipmentBag, Item item)
    {
        Debug.Log("显示提示放置");
        if (_inventory_UI_EquipBag_PlaceTips == null)
        {
            Debug.Log("提示词数组未初始化");
            return;
        }
        if (item == null)
        {
            Debug.Log("无法显示提示放置");
            return;
        }
        if (item._type == Item_Type.Cube)
        {
            for (int i = 0; i < _inventory_EquipBag_MaxNum_X; i++)
            {
                for (int j = 0; j < _inventory_EquipBag_MaxNum_Y; j++)
                {
                    if (inventory_CubeBag[i, j]._item != null) continue;
                    _inventory_UI_EquipBag_PlaceTips[i, j].SetActive(true);
                }
            }
        }
        else if (item._type == Item_Type.Equipment)
        {
            for (int i = 0; i < _inventory_EquipBag_MaxNum_X; i++)
            {
                for (int j = 0; j < _inventory_EquipBag_MaxNum_Y; j++)
                {
                    if (inventory_CubeBag[i, j]._item != null)
                    {
                        if (inventory_EquipmentBag[i, j]._item != null) continue;
                        _inventory_UI_EquipBag_PlaceTips[i, j].SetActive(true);
                    }
                }
            }
        }
        else
        {
            Debug.Log("该物品种类标签不存在");
        }
    }
    public void HidePlaceTips_EquipBag()
    {
        Debug.Log("隐藏提示放置");
        if (_inventory_UI_EquipBag_PlaceTips == null)
        {
            Debug.Log("提示词数组未初始化");
            return;
        }
        for (int i = 0; i < _inventory_EquipBag_MaxNum_X; i++)
        {
            for (int j = 0; j < _inventory_EquipBag_MaxNum_Y; j++)
            {
                _inventory_UI_EquipBag_PlaceTips[i, j].SetActive(false);
            }
        }
    }
    //替代提示
    public void ShowReplaceTips(Inventory_Slot[,] inventory_CubeBag, Inventory_Slot[,] inventory_EquipmentBag, Item item)
    {
        Debug.Log("显示替代提示");
        if (_inventory_UI_EquipBag_ReplaceTips == null)
        {
            Debug.Log("替代提示数组未初始化");
            return;
        }
        if (item == null)
        {
            Debug.Log("无法显示替代提示");
        }
        if (item._type == Item_Type.Cube)
        {
            for (int i = 0; i < _inventory_EquipBag_MaxNum_X; i++)
            {
                for (int j = 0; j < _inventory_EquipBag_MaxNum_Y; j++)
                {
                    if (inventory_CubeBag[i, j]._item == null) continue;
                    _inventory_UI_EquipBag_ReplaceTips[i, j].SetActive(true);
                }
            }
        }
        else if (item._type == Item_Type.Equipment)
        {
            for (int i = 0; i < _inventory_EquipBag_MaxNum_X; i++)
            {
                for (int j = 0; j < _inventory_EquipBag_MaxNum_Y; j++)
                {
                    if (inventory_EquipmentBag[i, j]._item == null) continue;
                    _inventory_UI_EquipBag_ReplaceTips[i, j].SetActive(true);
                }
            }
        }
    }
    public void HideReplaceTips_EquipBag()
    {
        Debug.Log("隐藏替代提示");
        if (_inventory_UI_EquipBag_ReplaceTips == null)
        {
            Debug.Log("替代提示数组未初始化");
            return;
        }
        for (int i = 0; i < _inventory_EquipBag_MaxNum_X; i++)
        {
            for (int j = 0; j < _inventory_EquipBag_MaxNum_Y; j++)
            {
                _inventory_UI_EquipBag_ReplaceTips[i, j].SetActive(false);
            }
        }
    }
    //违规提示
    public void ShowIllegalTips(int index_X, int index_Y)
    {
        if (_inventory_UI_EquipBag_IllegalTips == null)
        {
            Debug.Log("违规提示数组未初始化");
            return;
        }
        _inventory_UI_EquipBag_IllegalTips[index_X, index_Y].SetActive(true);
    }
    public void HideIllegalTips_EquipBag()
    {
        Debug.Log("隐藏所有违规提示");
        if (_inventory_UI_EquipBag_IllegalTips == null)
        {
            Debug.Log("违规提示数组未初始化");
            return;
        }
        for (int i = 0; i < _inventory_EquipBag_MaxNum_X; i++)
        {
            for (int j = 0; j < _inventory_EquipBag_MaxNum_Y; j++)
            {
                _inventory_UI_EquipBag_IllegalTips[i, j].SetActive(false);
            }
        }
    }

}


