using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Information_Item : MonoBehaviour
{
    private GameObject _item_Information_Display_UI;
    private Sprite _item_Information_Display_UI_Default;
    private GameObject _item_Attribute_Information_Position;
    private GameObject _item_Information_Prefab;
    private TextMeshProUGUI _item_Description;
    private ObjectPool<GameObject> _pool_Attribute_Item;
    private List<GameObject> _attribute_GameObjects_Item;

    public void Set_Information_Item(GameObject item_Information_Display_UI, GameObject item_Attribute_Information_Position, GameObject item_Information_Prefab, TextMeshProUGUI item_Description)
    {
        _item_Information_Display_UI = item_Information_Display_UI;
        _item_Information_Display_UI_Default = item_Information_Display_UI.GetComponent<Image>().sprite;
        _item_Attribute_Information_Position = item_Attribute_Information_Position;
        _item_Information_Prefab = item_Information_Prefab;
        _item_Description = item_Description;
        _pool_Attribute_Item = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy, true, 10, 1000);
        _attribute_GameObjects_Item = new();
        if(!(_item_Information_Display_UI && _item_Attribute_Information_Position && _item_Information_Prefab && _item_Description))
        {
            Debug.Log("Information_Item未初始化完全");
        }
    }

    public void Update_Information_Item(Item item)
    {
        if (item == null) return;

        //更新物品图像
        if (_item_Information_Display_UI == null) return;
        _item_Information_Display_UI.GetComponent<Image>().sprite = item._display_UI;

        if (_pool_Attribute_Item == null) return;
        if (_item_Attribute_Information_Position == null)
        {
            Debug.LogError("物品属性信息位置未初始化");
            return;
        }
        if (_item_Information_Prefab == null)
        {
            Debug.LogError("物品属性预制体未初始化");
            return;
        }

        foreach (GameObject attribute in _attribute_GameObjects_Item)
        {
            if (!attribute.activeSelf) continue;
            _pool_Attribute_Item.Release(attribute);
        }

        Debug.Log($"开始显示{item._id}物品的信息");
        Update_Information_Item_ByType(item, Attribute_Type.名称);
        Update_Information_Item_ByType(item, Attribute_Type.类型);
        Update_Information_Item_ByType(item, Attribute_Type.生命值);
        Update_Information_Item_ByType(item, Attribute_Type.攻击力);
        Update_Information_Item_ByType(item, Attribute_Type.攻击次数);
        Update_Information_Item_ByType(item, Attribute_Type.子弹类型);
        Update_Information_Item_ByType(item, Attribute_Type.重量);
        Update_Information_Item_ByType(item, Attribute_Type.升力);

        Update_Item_Description(item);
    }
    private void Update_Information_Item_ByType(Item item, Attribute_Type attribute_Type)
    {
        string text = "";
        string content = "";
        switch (attribute_Type)
        {
            case Attribute_Type.名称:
                text = "名称";
                content = item._name;
                break;
            case Attribute_Type.类型:
                text = "类别";
                content = Get_Type_Name(item);
                break;
            case Attribute_Type.生命值:
                if (item._healthy == 0) return;
                text = "生命值";
                content = item._healthy.ToString();
                break;
            case Attribute_Type.攻击力:
                if (item._bullet_Damage == 0) return;
                text = "攻击力";
                content = item._bullet_Damage.ToString();
                break;
            case Attribute_Type.子弹类型:
                if (item._bullet_Type == Bullet_Type.None) return;
                text = "子弹类型";
                content = Get_Bullet_Type_Name(item);
                break;
            case Attribute_Type.攻击次数:
                if (item._attack_Times == 0) return;
                text = "攻击频率";
                content = $"{item._attack_Times.ToString()}T/{item._attack_CD.ToString()}s";
                break;
            case Attribute_Type.重量:
                text = "重量";
                content = item._weight.ToString();
                break;
            case Attribute_Type.升力:
                if (item._liftForce == 0) return;
                text = "升力";
                content = item._liftForce.ToString();
                break;
            default:
                Debug.Log("没有该属性值");
                break;
        }
        Update_Information_Item(text, content);
    }
    private void Update_Information_Item(string name, string content)
    {
        if (name.Equals("") || content.Equals("")) return;

        GameObject attribute = _pool_Attribute_Item.Get();
        attribute.name = $"属性_{name}";
        attribute.transform.SetAsLastSibling();

        TextMeshProUGUI attribute_Name = attribute.transform.Find("名称").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI attribute_Text = attribute.transform.Find("属性值").GetComponent<TextMeshProUGUI>();

        if (attribute_Name == null || attribute_Text == null)
        {
            _pool_Attribute_Item.Release(attribute);
            return;
        }

        attribute_Name.text = name;
        attribute_Text.text = content;
    }
    private void Update_Item_Description(Item item)
    {
        if (_item_Description == null) return;
        _item_Description.text = item._description;
    }
    //重置物品信息
    public void Reset_Item_Information()
    {
        if (_item_Information_Display_UI != null)
        {
            _item_Information_Display_UI.GetComponent<Image>().sprite = _item_Information_Display_UI_Default;
        }
        if(_pool_Attribute_Item != null)
        {
            foreach (GameObject attribute in _attribute_GameObjects_Item)
            {
                if (!attribute.activeSelf) continue;
                _pool_Attribute_Item.Release(attribute);
            }
        }
        if(_item_Description != null)
        {
            _item_Description.text = "";
        }
    }
    
    private string Get_Type_Name(Item item)
    {
        if (item == null) return "";
        string content = "";
        if (item._isCore)
        {
            content = "核心";
        }
        else if(item._type == Item_Type.Cube)
        {
            content = "方块";
        }
        else if(item._type == Item_Type.Equipment)
        {
            if(item._equipment_Type == Equipment_Type.Weapon)
            {
                content = "武器";
            }
            else if(item._equipment_Type == Equipment_Type.Tool)
            {
                content = "工具";
            }
        }
        return content;
    }
    private string Get_Bullet_Type_Name(Item item)
    {
        if (item == null) return "";
        string content = "";
        if(item._bullet_Type == Bullet_Type.Common)
        {
            content = "普通";
        }
        else if(item._bullet_Type == Bullet_Type.Pierce)
        {
            content = "贯穿";
        }
        else if(item._bullet_Type == Bullet_Type.Block)
        {
            content = "拦截";
        }
        return content;
    }

    //对象池
    private GameObject CreateFunc()
    {
        if (_item_Information_Prefab == null)
        {
            Debug.Log("属性UI预制体未初始化");
            return null;
        }
        GameObject attribute = Instantiate(_item_Information_Prefab, _item_Attribute_Information_Position.transform);
        _attribute_GameObjects_Item.Add(attribute);
        return attribute;
    }
    private void ActionOnGet(GameObject obj)
    {
        obj.SetActive(true);
    }
    private void ActionOnRelease(GameObject obj)
    {
        obj.SetActive(false);
    }
    private void ActionOnDestroy(GameObject obj)
    {
        obj.SetActive(false);
    }

}
