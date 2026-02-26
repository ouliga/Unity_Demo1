
using UnityEngine;
using UnityEngine.UI;
public class Item_Buy : MonoBehaviour
{
    private Inventory_Manager _inventory_Manager;
    private Shop_Manager _shop_Manager;
    private SelectType _selectType;
    public int _index_Shop {  get; private set; }

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
        if(_shop_Manager == null) _shop_Manager = Shop_Manager.Instance;
        Item item = _shop_Manager.Get_Shop_Item(_index_Shop);
        if (_shop_Manager.Check_HaveSelect_Item(_selectType, _index_Shop))
        {
            if(item != null)
            {
                if(_shop_Manager.Check_Buy_Item(item) && _shop_Manager.Check_Remove_Shop_Item(_index_Shop,1) &&_inventory_Manager.Check_AddItem_Bag(item, 1))
                {
                    _shop_Manager.Remove_Shop_Item(_index_Shop, 1);
                    _inventory_Manager.AddItem_Bag(item, 1);
                    _shop_Manager.Buy_Item(item);
                }
            }
            _shop_Manager.Reset_Select_Item();
        }
        else
        {
            _shop_Manager.Update_Shop_SelectTip(_index_Shop);
            _shop_Manager.Update_Item_Information(item);
            _shop_Manager.Update_Shop_Item_Price_Information(item);
        }
    }
    public void Initialize_Item_Bag(SelectType selectType, int index)
    {
        _selectType = selectType;
        _index_Shop = index;
        _inventory_Manager = Inventory_Manager.Instance;
        _shop_Manager = Shop_Manager.Instance;
    }
}
