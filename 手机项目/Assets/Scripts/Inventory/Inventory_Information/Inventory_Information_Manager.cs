using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public enum Attribute_Type
{
    队伍类型,名称,类型,生命值,攻击力,攻击次数,子弹类型,重量,升力
}
public class Inventory_Information_Manager : Singleton<Inventory_Information_Manager>
{
    private Information_Item _information_Item;
    [SerializeField] private GameObject _item_Information_Display_UI;
    [SerializeField] private GameObject _item_Attribute_Information_Position;
    [SerializeField] private GameObject _item_Information_Prefab;
    [SerializeField] private TextMeshProUGUI _item_Description;

    private Information_AirShip _information_AirShip;
    [SerializeField] private GameObject _airShip_Information_Position;
    [SerializeField] private GameObject _airShip_Information_Prefab;
    private Hashtable _attribute_GameObjects_AirShip;
 

    private void Awake() { }
    public void Set_Inventory_Information_Manager()
    {
        Initialize_Information_Item();
        Initialize_Information_AriShip();
    }
    //飞船信息
    private void Initialize_Information_AriShip()
    {
        Debug.Log("初始化飞船信息");
        if(_airShip_Information_Position == null)
        {
            Debug.Log("飞船信息放置位置未初始化");
        }
        _information_AirShip = gameObject.AddComponent<Information_AirShip>();
        _information_AirShip.Initialize_Information_AirShip(_airShip_Information_Position, _airShip_Information_Prefab);
    }
    public void Update_Information_AirShip(Inventory_AirShip_Attribute_Information airShip_Attribute)
    {
        if (_information_AirShip != null)
        {
            _information_AirShip.Update_Information_AirShip(airShip_Attribute);
        }

    }
    //物品信息
    public void Initialize_Information_Item()
    {
        _information_Item = gameObject.AddComponent<Information_Item>();
        _information_Item.Set_Information_Item(_item_Information_Display_UI, _item_Attribute_Information_Position, _item_Information_Prefab, _item_Description);
    }
    public void Update_Information_Item(Item item)
    {
        if (_information_Item == null) return;
        _information_Item.Update_Information_Item(item);
    }
    public void Reset_Information_Item()
    {
        if(_information_Item != null)
        {
            _information_Item.Reset_Item_Information();
        }
    }
}
