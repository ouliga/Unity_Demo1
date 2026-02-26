using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Item_Sell : MonoBehaviour
{
    private Inventory_Manager _inventory_Manager;
    private Shop_Manager _shop_Manager;
    private SelectType _selectType;
    public int _index_Shop_Bag { get; private set; }

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }
    private void OnClick()
    {
        if (_inventory_Manager == null) _inventory_Manager = Inventory_Manager.Instance;
        if (_shop_Manager == null) _shop_Manager = Shop_Manager.Instance;
        Item item = _inventory_Manager.Get_Item_Bag(_index_Shop_Bag);
        if (_shop_Manager.Check_HaveSelect_Item(_selectType, _index_Shop_Bag))
        {
            if (item != null && !item._isCore)
            {
                if (_inventory_Manager.Check_RemoveItem_Bag(_index_Shop_Bag,1))
                {
                    _inventory_Manager.RemoveItem_Bag(_index_Shop_Bag, 1);
                    _shop_Manager.Sell_Item(item);
                }
            }
            _shop_Manager.Reset_Select_Item();
        }
        else
        {
            _inventory_Manager.Update_UI_Shop_Bag_SelectTip(_index_Shop_Bag);
            _shop_Manager.Update_Item_Information(item);
        }
        _shop_Manager.Reset_Shop_Item_Price_Information();
    }
    public void Initialize_Item_Sell(SelectType selectType,int index)
    {
        _selectType = selectType;
        _index_Shop_Bag = index;
        _inventory_Manager = Inventory_Manager.Instance;
        _shop_Manager = Shop_Manager.Instance;
    }
}
