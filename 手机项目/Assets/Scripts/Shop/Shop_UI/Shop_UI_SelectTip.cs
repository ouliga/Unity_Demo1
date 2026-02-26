using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_UI_SelectTip : MonoBehaviour
{
    public string _shop_Slot_SelectTip_Name { get; private set; }
    private GameObject _shop_UI_Slot_SelectTip_Prefab;
    private int _shop_Slot_MaxNum;
    private GameObject[] _shop_UI_Slot_SelectTips;
    private int _shop_Bag_Slot_MaxNum;
    private GameObject[] _shop_Bag_UI_Slot_SelectTips;

    private void Initialize_Name()
    {
        _shop_Slot_SelectTip_Name = "格子_选择提示";
    }
    public void Initialize_Shop_UI_SelectTip(GameObject shop_UI_Slot_SelectTip_Prefab)
    {
        _shop_UI_Slot_SelectTip_Prefab = shop_UI_Slot_SelectTip_Prefab;
        Initialize_Name();
    }
    public void Initialize_Shop_UI_Slot_SelectTips(GameObject[] shop_UI_Slots)
    {
        if (_shop_UI_Slot_SelectTip_Prefab == null) return;
        if(shop_UI_Slots != null)
        {
            _shop_Slot_MaxNum = shop_UI_Slots.Length;
            if(_shop_UI_Slot_SelectTips == null)
            {
                _shop_UI_Slot_SelectTips = new GameObject[_shop_Slot_MaxNum];
            }
            for(int i = 0;i< _shop_Slot_MaxNum; i++)
            {
                _shop_UI_Slot_SelectTips[i] = Instantiate(_shop_UI_Slot_SelectTip_Prefab, shop_UI_Slots[i].transform);
                _shop_UI_Slot_SelectTips[i].transform.SetAsLastSibling();
                _shop_UI_Slot_SelectTips[i].name = _shop_Slot_SelectTip_Name;
            }
        }
    }
    public void Initialize_Shop_Bag_UI_Slot_SelectTips(GameObject[] shop_Bag_UI_Slots)
    {
        if (_shop_UI_Slot_SelectTip_Prefab == null) return;
        if (shop_Bag_UI_Slots != null)
        {
            _shop_Bag_Slot_MaxNum = shop_Bag_UI_Slots.Length;
            if(_shop_Bag_UI_Slot_SelectTips == null)
            {
                _shop_Bag_UI_Slot_SelectTips = new GameObject[_shop_Bag_Slot_MaxNum];
            }
            for (int i = 0; i < _shop_Bag_Slot_MaxNum; i++)
            {
                _shop_Bag_UI_Slot_SelectTips[i] = Instantiate(_shop_UI_Slot_SelectTip_Prefab, shop_Bag_UI_Slots[i].transform);
                _shop_Bag_UI_Slot_SelectTips[i].transform.SetAsLastSibling();
                _shop_Bag_UI_Slot_SelectTips[i].name = _shop_Slot_SelectTip_Name;
            }
        }
    }
    public void Update_Shop_SelectTip(int index)
    {  
        if (index < 0 || index >= _shop_Slot_MaxNum)
        {
            Debug.Log("超过了商店格子的界限");
            return;
        }
        Reset_SelectTip();
        _shop_UI_Slot_SelectTips[index].SetActive(true);
    }
    public void Update_Shop_Bag_SelectTip(int index)
    {
        if(index < 0|| index >= _shop_Bag_Slot_MaxNum)
        {
            Debug.Log("超出了商店背包格子的界限");
            return;
        }
        Reset_SelectTip();
        _shop_Bag_UI_Slot_SelectTips[index].SetActive(true);
    }
    public void Reset_SelectTip()
    {
        for(int i = 0;i< _shop_Slot_MaxNum; i++)
        {
            _shop_UI_Slot_SelectTips[i].SetActive(false);
        }
        for(int i = 0; i < _shop_Bag_Slot_MaxNum; i++)
        {
            _shop_Bag_UI_Slot_SelectTips[i].SetActive(false);
        }
    }

}
