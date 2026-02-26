using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


public enum Bag_Type
{
    bag,cubeBag,equipmentBag
}
public class Item_Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Bag_Type _bag_Type { get; private set; }
    public int _index_Bag { get; private set; }
    public int _index_X_EquipBag { get;private set; }
    public int _index_Y_EquipBag { get;private set; }

    // 拖拽的物体
    private GameObject _item_GameObject;
    private Canvas _canvas;
    private Item _item;

    public Item_Drag()
    {
        _index_Bag = -1;
        _index_X_EquipBag = -1;
        _index_Y_EquipBag = -1;
    }
    public void Initialize_Item_Drag(Bag_Type bag_Type,int index_Bag,int index_X_EquipBag,int index_Y_EquipBag, Canvas canvas)
    {
        _bag_Type = bag_Type;
        _index_Bag = index_Bag;
        _index_X_EquipBag = index_X_EquipBag;
        _index_Y_EquipBag = index_Y_EquipBag;
        _canvas = canvas;
    }
    // 开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Inventory_Manager.Instance == null)
        {
            Debug.Log("仓库管理器未初始化");
            return;
        }
        if(Inventory_UI_Manager.Instance == null)
        {
            Debug.Log("仓库UI管理器未初始化");
            return;
        }
        _item_GameObject = Instantiate(gameObject);
        _item_GameObject.transform.SetParent(_canvas.transform);
        _item = GetItem(_bag_Type,_index_Bag,_index_X_EquipBag,_index_Y_EquipBag);
        //改变被选中物体的透明度
        gameObject.GetComponent<CanvasGroup>().alpha = 0.6f;
        //拖拽的时候不能阻挡射线，不然一会在卡槽中放置的时候，射线射不到卡槽上
        _item_GameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        //展示提示
        if(_bag_Type == Bag_Type.bag)
        {
            Inventory_Manager.Instance.ShowPlaceTips_BagToEquip(_item);
            Inventory_Manager.Instance.ShowReplaceTips(_item);
        }
        else
        {
            Inventory_Manager.Instance.ShowPlaceTips_EquipToEquip(_item);
        }
        //隐藏违规提示
        Inventory_UI_Manager.Instance.HideIllegalTips_EquipBag();

        //显示物品信息
        if (Inventory_Information_Manager.Instance == null) return;
        Inventory_Information_Manager.Instance.Update_Information_Item(_item);
        Debug.Log($"开始拖拽物品{_item._type}，位于{_bag_Type}");
    }

    // 拖拽中
    public void OnDrag(PointerEventData eventData)
    {
        _item_GameObject.transform.position = Input.mousePosition;
    }

    // 结束拖拽
    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject pointGameObject = eventData.pointerCurrentRaycast.gameObject;
        if (pointGameObject != null)
        {
            Inventory_UI_Name inventory_UI_Name = Inventory_UI_Manager.Instance._inventory_UI_Name;
            //放置
            if (pointGameObject.name == inventory_UI_Name._inventory_EquipBag_PlaceTip_Name)
            {
                Item_Drag _item_Drag_PointItem = pointGameObject.transform.parent.Find(inventory_UI_Name._inventory_CubeBag_Item_Name).GetComponent<Item_Drag>();
                int index_X_PointItem = _item_Drag_PointItem._index_X_EquipBag;
                int index_Y_PointItem = _item_Drag_PointItem._index_Y_EquipBag;
                //物品来自背包
                if (_bag_Type == Bag_Type.bag)
                {
                    if(_item._type == Item_Type.Cube)
                    {
                        if (Inventory_Manager.Instance.Check_AddItem_Cube(index_X_PointItem, index_Y_PointItem, _item) && Inventory_Manager.Instance.Check_RemoveItem_Bag(_index_Bag, 1))
                        {
                            Inventory_Manager.Instance.RemoveItem_Bag(_index_Bag, 1);
                            Inventory_Manager.Instance.AddItem_Cube(index_X_PointItem, index_Y_PointItem, _item);
                        }
                    }
                    else if (_item._type == Item_Type.Equipment)
                    {
                        if (Inventory_Manager.Instance.Check_AddItem_Equipment(index_X_PointItem, index_Y_PointItem, _item) && Inventory_Manager.Instance.Check_RemoveItem_Bag(_index_Bag, 1))
                        {
                            Inventory_Manager.Instance.RemoveItem_Bag(_index_Bag, 1);
                            Inventory_Manager.Instance.AddItem_Equipment(index_X_PointItem, index_Y_PointItem, _item);
                        }
                    }
                }
                //物品来自装备栏
                else if(_bag_Type == Bag_Type.cubeBag || _bag_Type == Bag_Type.equipmentBag)
                {
                    if (_item._type == Item_Type.Cube)
                    {
                        if (Inventory_Manager.Instance.Check_AddItem_Cube(index_X_PointItem, index_Y_PointItem, _item) && Inventory_Manager.Instance.Check_RemoveItem_Cube(_index_X_EquipBag, _index_Y_EquipBag))
                        {
                            Inventory_Manager.Instance.RemoveItem_Cube(_index_X_EquipBag, _index_Y_EquipBag);
                            Inventory_Manager.Instance.AddItem_Cube(index_X_PointItem, index_Y_PointItem, _item);
                        }
                        else
                        {
                            Inventory_Manager.Instance.SwapItem_CubeToCube(_index_X_EquipBag, _index_Y_EquipBag, index_X_PointItem, index_Y_PointItem);
                        }
                    }
                    else if(_item._type == Item_Type.Equipment)
                    {
                        if (Inventory_Manager.Instance.Check_AddItem_Equipment(index_X_PointItem, index_Y_PointItem, _item) && Inventory_Manager.Instance.Check_RemoveItem_Equipment(_index_X_EquipBag, _index_Y_EquipBag))
                        {
                            Inventory_Manager.Instance.RemoveItem_Equipment(_index_X_EquipBag, _index_Y_EquipBag);
                            Inventory_Manager.Instance.AddItem_Equipment(index_X_PointItem, index_Y_PointItem, _item);
                        }
                        else
                        {
                            Inventory_Manager.Instance.SwapItem_EquipToEquip(_index_X_EquipBag, _index_Y_EquipBag, index_X_PointItem, index_Y_PointItem);
                        }
                    }
                    else
                    {
                        Debug.Log(_item._type + "物品标签有误");
                    }

                }
            }
            //替代
            else if(pointGameObject.name == inventory_UI_Name._inventory_EquipBag_ReplaceTip_Name)
            {
                Item_Drag _item_Drag_PointItem = pointGameObject.transform.parent.Find(inventory_UI_Name._inventory_CubeBag_Item_Name).GetComponent<Item_Drag>();
                int index_X_PointItem = _item_Drag_PointItem._index_X_EquipBag;
                int index_Y_PointItem = _item_Drag_PointItem._index_Y_EquipBag;
                Debug.Log("物品尝试替代");
                if (_bag_Type == Bag_Type.bag)
                {
                    Item pointItem;
                    if(_item._type == Item_Type.Cube)
                    {
                        pointItem = GetItem(Bag_Type.cubeBag, -1, index_X_PointItem, index_Y_PointItem);
                        if (Inventory_Manager.Instance.Check_RemoveItem_Cube(index_X_PointItem, index_Y_PointItem))
                        {
                            if (Inventory_Manager.Instance.Check_AddItem_Bag(pointItem, 1))
                            {
                                Inventory_Manager.Instance.RemoveItem_Cube(index_X_PointItem, index_Y_PointItem);
                                Inventory_Manager.Instance.AddItem_Cube(index_X_PointItem, index_Y_PointItem, _item);
                                Inventory_Manager.Instance.RemoveItem_Bag(_index_Bag, 1);
                                Inventory_Manager.Instance.AddItem_Bag(pointItem, 1);
                            }
                        }
                    }
                    else if(_item._type == Item_Type.Equipment)
                    {
                        pointItem = GetItem(Bag_Type.equipmentBag, -1, index_X_PointItem, index_Y_PointItem);
                        if (Inventory_Manager.Instance.Check_RemoveItem_Equipment(index_X_PointItem, index_Y_PointItem))
                        {
                            if (Inventory_Manager.Instance.Check_AddItem_Bag(pointItem, 1))
                            {
                                Inventory_Manager.Instance.RemoveItem_Equipment(index_X_PointItem, index_Y_PointItem);
                                Inventory_Manager.Instance.AddItem_Equipment(index_X_PointItem, index_Y_PointItem, _item);
                                Inventory_Manager.Instance.RemoveItem_Bag(_index_Bag, 1);
                                Inventory_Manager.Instance.AddItem_Bag(pointItem, 1);
                            }
                        }
                    }
                }
            }
            //回归
            else
            {
                Debug.Log("物品尝试回归背包");
                if (_bag_Type == Bag_Type.cubeBag)
                {
                    if (Inventory_Manager.Instance.Check_AddItem_Bag(_item,1))
                    {
                        Inventory_Manager.Instance.RemoveItem_Cube(_index_X_EquipBag, _index_Y_EquipBag);
                        Inventory_Manager.Instance.AddItem_Bag(_item, 1);
                        Debug.Log("方块从装备栏回归背包");
                    }
                }
                else if(_bag_Type == Bag_Type.equipmentBag)
                {
                    if (Inventory_Manager.Instance.Check_AddItem_Bag(_item, 1))
                    {
                        Inventory_Manager.Instance.RemoveItem_Equipment(_index_X_EquipBag, _index_Y_EquipBag);
                        Inventory_Manager.Instance.AddItem_Bag(_item, 1);
                        Debug.Log("装备从装备栏回归背包");
                    }
                }   
            }
        }

        Destroy(_item_GameObject);
        _item_GameObject = null;
        gameObject.GetComponent<CanvasGroup>().alpha = 1f;

        Inventory_Manager.Instance.HidePlaceTips_EquipBag();
        Inventory_Manager.Instance.HideReplaceTips_EquipBag();
        Inventory_Manager.Instance.Check_EquipBag_Current_Valid();

        Inventory_Manager.Instance.Sort_Bag_Fill_Empty();

        Inventory_Manager.Instance.Update_Information_AirShip_Current();
        //Inventory_Manager.Instance.Show_Information_AirShip_Current();
        
        Debug.Log("拖拽结束");
    }
    private Item GetItem(Bag_Type bag_Type,int index_Bag,int index_X_EquipBag,int index_Y_EquipBag)
    {
        if (Inventory_Manager.Instance == null)
        {
            Debug.Log("背包管理器未初始化");
            return null;
        }
        if (bag_Type == Bag_Type.bag)
        {
            return Inventory_Manager.Instance.Get_Item_Bag(index_Bag);
        }
        else if(bag_Type == Bag_Type.cubeBag)
        {
            return Inventory_Manager.Instance.Get_Item_CubeBag(index_X_EquipBag, index_Y_EquipBag);
        }
        else if(bag_Type == Bag_Type.equipmentBag)
        {
            return Inventory_Manager.Instance.Get_Item_Equipment(index_X_EquipBag, index_Y_EquipBag);
        }
        return null;
    }
}
