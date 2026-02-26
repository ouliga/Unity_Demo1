using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI_Manager : Singleton<Inventory_UI_Manager>
{
    //画布
    [SerializeField] private Canvas _canvas;

    //名称
    public Inventory_UI_Name _inventory_UI_Name { get; private set; }
    //public string _inventory_Bag_Slot_Name { get; private set; }
    //public string _inventory_EquipBag_Slot_Name { get; private set; }

    //public string _inventory_Bag_Slot_Item_Name { get; private set; }
    //public string _inventory_Bag_Slot_Num_Name { get; private set; }

    //public string _inventory_CubeBag_Item_Name { get; private set; }
    //public string _inventory_EquipBag_Item_Name { get; private set; }

    //public string _inventory_EquipBag_PlaceTip_Name { get; private set; }
    //public string _inventory_EquipBag_ReplaceTip_Name { get; private set; }
    //public string _inventory_EquipBag_IllegalTip_Name { get; private set; }

    //背包
    [SerializeField]
    private GameObject _inventory_UI_Bag_Position;
    [SerializeField]
    private GameObject _inventory_UI_Bag_Slot_Prefab;

    //背包UI功能
    private Inventory_UI_Bag _inventory_UI_Bag;

    [SerializeField]
    private GameObject _inventory_UI_EquipBag_Position;
    [SerializeField]
    private GameObject _inventory_UI_EquipBag_Slot_Prefab;
    [SerializeField]
    private GameObject _inventory_UI_EquipBag_PlaceTip_Prefab;
    [SerializeField]
    private GameObject _inventory_UI_EquipBag_ReplaceTip_Prefab;
    [SerializeField]
    private GameObject _inventory_UI_EquipBag_IllegalTip_Prefab;

    //装备栏UI功能
    private Inventory_UI_EquipBag _inventory_UI_EquipBag;
    //商店背包
    [SerializeField] private GameObject _shop_UI_Bag_Position;
    [SerializeField] private GameObject _shop_UI_Bag_Slot_Prefab;

    //public string _shop_Bag_Slot_Name { get;private set; }
    //public string _shop_Bag_Slot_Item_Name { get; private set; }
    //public string _shop_Bag_Slot_Num_Name { get; private set; }
    //[SerializeField] private GameObject _shop_UI_Bag_SelectTip_Prefab;
    //商店UI功能
    private Inventory_UI_Shop_Bag _inventory_UI_Shop_Bag;

    private void Awake() { }

    private void Initialize_UI_Name()
    {
        //_inventory_Bag_Slot_Name = "背包栏_格子";
        //_inventory_EquipBag_Slot_Name = "装备栏_格子";
        //_inventory_Bag_Slot_Item_Name = "格子_图标";
        //_inventory_Bag_Slot_Num_Name = "格子_数量";
        //_inventory_CubeBag_Item_Name = "格子_方块图标";
        //_inventory_EquipBag_Item_Name = "格子_装备图标";
        //_inventory_EquipBag_PlaceTip_Name = "格子_放置提示";
        //_inventory_EquipBag_ReplaceTip_Name = "格子_替代提示";
        //_inventory_EquipBag_IllegalTip_Name = "格子_违规提示";

        ////商店
        //_shop_Bag_Slot_Name = "商店_背包_格子";
        //_shop_Bag_Slot_Item_Name = "格子_图标";
        //_shop_Bag_Slot_Num_Name = "格子_数量";

        _inventory_UI_Name = new Inventory_UI_Name();
        _inventory_UI_Name._inventory_Bag_Slot_Name = "背包栏_格子";
        _inventory_UI_Name._inventory_Bag_Slot_Item_Name = "格子_图标";
        _inventory_UI_Name._inventory_Bag_Slot_Num_Name = "格子_数量";

        _inventory_UI_Name._inventory_EquipBag_Slot_Name = "装备栏_格子";
        _inventory_UI_Name._inventory_CubeBag_Item_Name = "格子_方块图标";
        _inventory_UI_Name._inventory_EquipBag_Item_Name = "格子_装备图标";

        _inventory_UI_Name._inventory_EquipBag_PlaceTip_Name = "格子_放置提示";
        _inventory_UI_Name._inventory_EquipBag_ReplaceTip_Name = "格子_替代提示";
        _inventory_UI_Name._inventory_EquipBag_IllegalTip_Name = "格子_违规提示";

        _inventory_UI_Name._shop_Bag_Slot_Name = "商店_背包_格子";
        _inventory_UI_Name._shop_Bag_Slot_Item_Name = "格子_图标";
        _inventory_UI_Name._shop_Bag_Slot_Num_Name = "格子_数量";

    }
    public void Set_Inventory_UI_Manager()
    {
        Debug.Log("初始化仓库UI管理器");
        Initialize_UI_Name();   
        Debug.Log("初始化仓库UI管理器成功");
    }
    //背包UI
    public void Initialize_Inventory_UI_Bag(int inventory_Bag_MaxNum)
    {
        Debug.Log($"初始化背包UI({inventory_Bag_MaxNum})");
        if (_inventory_UI_Bag == null)
        {
            _inventory_UI_Bag = gameObject.AddComponent<Inventory_UI_Bag>();

        }
        _inventory_UI_Bag.Initialize_Inventory_UI_Bag(inventory_Bag_MaxNum, _inventory_UI_Bag_Position, _inventory_UI_Bag_Slot_Prefab, _canvas);
        _inventory_UI_Bag.Initialize_UI_Name(_inventory_UI_Name);
        _inventory_UI_Bag.Initialize_UI_Bag();
    }

    public void Update_UI_Bag_ByIndex(Inventory_Slot[] inventory_Bag, int index)
    {
        if ( _inventory_UI_Bag != null)
        {
            _inventory_UI_Bag.Update_UI_Bag_ByIndex(inventory_Bag, index);
        }
    }
    public void Update_UI_Bag(Inventory_Slot[] inventory_Bag)
    {
        if(_inventory_UI_Bag != null)
        {
            _inventory_UI_Bag.Update_UI_Bag(inventory_Bag);
        }
    }
    //装备栏UI
    public void Initialize_UI_EquipBag(int inventory_EquipBag_MaxNum_X,int inventory_EquipBag_MaxNum_Y)
    {
        if(_inventory_UI_EquipBag == null)
        {
            _inventory_UI_EquipBag = gameObject.AddComponent<Inventory_UI_EquipBag>();
        }
        _inventory_UI_EquipBag.Initialize_Inventory_UI_EquipBag(inventory_EquipBag_MaxNum_X, inventory_EquipBag_MaxNum_Y, _inventory_UI_EquipBag_Position, _inventory_UI_EquipBag_Slot_Prefab, _inventory_UI_EquipBag_PlaceTip_Prefab, _inventory_UI_EquipBag_ReplaceTip_Prefab, _inventory_UI_EquipBag_IllegalTip_Prefab,_canvas);
        _inventory_UI_EquipBag.Initialize_UI_Name(_inventory_UI_Name);
        _inventory_UI_EquipBag.Initialize_UI_EquipBag();
    }


    public void Update_UI_CubeBag_ByIndex(Inventory_Slot[,] inventory_CubeBag, int index_X, int index_Y)
    {
        if(_inventory_UI_EquipBag != null)
        {
            _inventory_UI_EquipBag.Update_UI_CubeBag_ByIndex(inventory_CubeBag, index_X, index_Y);
        }
    }
    public void Update_UI_CubeBag(Inventory_Slot[,] inventory_CubeBag)
    {
        if(_inventory_UI_EquipBag != null)
        {
            _inventory_UI_EquipBag.Update_UI_CubeBag(inventory_CubeBag);
        }
    }
    public void Update_UI_EquipmentBag_ByIndex(Inventory_Slot[,] inventory_EquipmentBag,int index_X, int index_Y)
    {
        if(_inventory_UI_EquipBag != null)
        {
            _inventory_UI_EquipBag.Update_UI_EquipmentBag_ByIndex(inventory_EquipmentBag, index_X, index_Y);
        }
    }
    public void Update_UI_EquipmentBag(Inventory_Slot[,] inventory_EquipmentBag)
    {
        if(_inventory_UI_EquipBag != null)
        {
            _inventory_UI_EquipBag.Update_UI_EquipmentBag(inventory_EquipmentBag);
        }
    }
    //放置提示
    public void ShowPlaceTips_EquipToEquip(Inventory_Slot[,] inventory_CubeBag,Item item)
    {
        if(_inventory_UI_EquipBag != null)
        {
            _inventory_UI_EquipBag.ShowPlaceTips_EquipToEquip(inventory_CubeBag, item);
        }
    }
    public void ShowPlaceTips_BagToEquip(Inventory_Slot[,] inventory_CubeBag, Inventory_Slot[,] inventory_EquipmentBag, Item item)
    {
        if(_inventory_UI_EquipBag != null)
        {
            _inventory_UI_EquipBag.ShowPlaceTips_BagToEquip(inventory_CubeBag, inventory_EquipmentBag, item);
        }
    }
    public void HidePlaceTips_EquipBag()
    {
        if(_inventory_UI_EquipBag != null)
        {
            _inventory_UI_EquipBag.HidePlaceTips_EquipBag();
        }
    }
    //替代提示
    public void ShowReplaceTips(Inventory_Slot[,] inventory_CubeBag, Inventory_Slot[,] inventory_EquipmentBag, Item item)
    {
        if(_inventory_UI_EquipBag != null)
        {
            _inventory_UI_EquipBag.ShowReplaceTips(inventory_CubeBag,inventory_EquipmentBag,item);
        }
    }
    public void HideReplaceTips_EquipBag()
    {
        if(_inventory_UI_EquipBag != null)
        {
            _inventory_UI_EquipBag.HideReplaceTips_EquipBag();
        }
    }
    //违规提示
    public void ShowIllegalTips(int index_X,int index_Y)
    {
        if(_inventory_UI_EquipBag != null)
        {
            _inventory_UI_EquipBag.ShowIllegalTips(index_X,index_Y);
        }
    }
    public void HideIllegalTips_EquipBag()
    {
        if(_inventory_UI_EquipBag != null)
        {
            _inventory_UI_EquipBag.HideIllegalTips_EquipBag();
        }
    }
    //商店//
    public void Initialize_Inventory_UI_Shop_Bag(int inventory_Bag_MaxNum)
    {
        if(_inventory_UI_Shop_Bag == null)
        {
            _inventory_UI_Shop_Bag = gameObject.AddComponent<Inventory_UI_Shop_Bag>();
            
        }
        _inventory_UI_Shop_Bag.Initialize_Inventory_UI_Shop_Bag(inventory_Bag_MaxNum, _shop_UI_Bag_Position, _shop_UI_Bag_Slot_Prefab);
        _inventory_UI_Shop_Bag.Initialize_UI_Name(_inventory_UI_Name);
        _inventory_UI_Shop_Bag.Initialize_UI_Shop_Bag();
    }

    //更新商店背包格子UI
    public void Update_UI_Shop_Bag(Inventory_Slot[] inventory_Bag)
    {
        if (_inventory_UI_Shop_Bag != null)
        {
            _inventory_UI_Shop_Bag.Update_UI_Shop_Bag(inventory_Bag);
        }
    }

    public void Update_UI_Shop_Bag_By_Index(Inventory_Slot[] inventory_Bag,int index)
    {
        if(_inventory_UI_Shop_Bag != null)
        {
            _inventory_UI_Shop_Bag.Update_UI_Shop_Bag_By_Index(inventory_Bag, index);
        }
    }

    //商店背包选择提示//

    private void Initialize_UI_Shop_Bag_SelectTips()
    {
        if(_inventory_UI_Shop_Bag != null)
        {
            _inventory_UI_Shop_Bag.Initialize_UI_Shop_Bag_SelectTips();
        }
    }
    public void Update_UI_Shop_Bag_Slot_SelectTip(int index)
    {
        if(_inventory_UI_Shop_Bag != null)
        {
            _inventory_UI_Shop_Bag.Update_UI_Shop_Bag_SelectTip(index);
        }
    }
    public void Reset_UI_Shop_Bag_SelectTip()
    {
        if(_inventory_UI_Shop_Bag != null)
        {
            _inventory_UI_Shop_Bag.Reset_UI_Shop_Bag_SelectTip();
        }

    }



}
