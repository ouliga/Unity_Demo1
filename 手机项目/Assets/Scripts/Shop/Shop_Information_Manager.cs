using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class Shop_Information_Manager : Singleton<Shop_Information_Manager>
{
    [SerializeField] private GameObject _item_Information_Display_UI;
    [SerializeField] private GameObject _item_Information_Attribute_Position;
    [SerializeField] private GameObject _item_Information_Attribute_Prefab;
    [SerializeField] private TextMeshProUGUI _item_Information_Description;

    private Information_Item _information_Item;

    //private ObjectPool<GameObject> _pool_Information_Attribute_Prefab;
    //private List<GameObject> _information_Attribute_Prefabs;

    [SerializeField] private TextMeshProUGUI _shop_Moneny_Num_Text;
    [SerializeField] private TextMeshProUGUI _shop_Component_Num_Text;

    [SerializeField] private TextMeshProUGUI _shop_Item_Price_Money_Num_Text;
    [SerializeField] private TextMeshProUGUI _shop_Item_Price_Component_Num_Text;
    [SerializeField] private TextMeshProUGUI _shop_Refresh_Price_Money_Num_Text;
    [SerializeField] private TextMeshProUGUI _shop_LevelUp_Price_Money_Num_Text;


    private void Awake() { }
    private void OnEnable()
    {
        if(Shop_Manager.Instance != null)
        {
            Shop_Manager.Instance._subscribe_Shop_Resource_Update += Update_Shop_Resource_Information;
        }
    }
    private void OnDisable()
    {
        if (Shop_Manager.Instance != null)
        {
            Shop_Manager.Instance._subscribe_Shop_Resource_Update -= Update_Shop_Resource_Information;
        }
    }

    public void Set_Shop_Information_Manager()
    {
        Set_Information_Item();
    }

    //物品信息//
    private void Set_Information_Item()
    {
        _information_Item = gameObject.AddComponent<Information_Item>();
        _information_Item.Set_Information_Item(_item_Information_Display_UI, _item_Information_Attribute_Position, _item_Information_Attribute_Prefab, _item_Information_Description);
    }

    public void Update_Information_Item(Item item)
    {
        if (_information_Item == null) return;
        _information_Item.Update_Information_Item(item);
    }

    public void Reset_Information_Item()
    {
        if (_information_Item != null)
        {
            _information_Item.Reset_Item_Information();
        }
    }

    //商店信息
    public void Update_Shop_Resource_Information(Shop_Resource shop_Resource)
    {
        if(_shop_Moneny_Num_Text == null || _shop_Component_Num_Text == null)
        {
            Debug.LogError("商店资源信息未初始化");
            return;
        }
        if(shop_Resource == null)
        {
            Debug.LogError("资源物资未初始化");
            return;
        }
        _shop_Moneny_Num_Text.text = ""+shop_Resource._money_Num;
        _shop_Component_Num_Text.text = ""+shop_Resource._component_Num;
    }
    public void Update_Shop_Item_Price_Information(Shop_Resource shop_Resource,Item item)
    {
        if (shop_Resource == null) return;
        if(item == null) return;
        if(_shop_Item_Price_Money_Num_Text == null || _shop_Item_Price_Component_Num_Text == null)
        {
            Debug.LogError("商品信息未初始化");
            return;
        }
        if(item._type == Item_Type.Cube)
        {
            _shop_Item_Price_Money_Num_Text.text = shop_Resource._item_Buy_Price_Money + "";
            _shop_Item_Price_Component_Num_Text.text = "0";
        }
        else if(item._type == Item_Type.Equipment)
        {
            _shop_Item_Price_Money_Num_Text.text = shop_Resource._item_Buy_Price_Money + "";
            _shop_Item_Price_Component_Num_Text.text = shop_Resource._item_Buy_Price_Component + "";
        }
    }
    public void Reset_Shop_Item_Price_Information()
    {
        _shop_Item_Price_Money_Num_Text.text = "0";
        _shop_Item_Price_Component_Num_Text.text = "0";
    }
    public void Update_Shop_Refresh_Price_Information(Shop_Resource shop_Resource)
    {
        if (shop_Resource == null) return;
        if (_shop_Refresh_Price_Money_Num_Text == null)
        {
            Debug.LogError("商店信息未初始化");
            return;
        }
        _shop_Refresh_Price_Money_Num_Text.text = shop_Resource._shop_Refresh_Cost+"";
    }
    public void Update_Shop_LevelUp_Price_Information(Shop_Resource shop_Resource)
    {
        if (shop_Resource == null) return;
        if(_shop_LevelUp_Price_Money_Num_Text == null)
        {
            Debug.LogError("商店信息未初始化");
            return;
        }
        _shop_LevelUp_Price_Money_Num_Text.text = shop_Resource._shop_LevelUp_Cost + "";
    }
}
