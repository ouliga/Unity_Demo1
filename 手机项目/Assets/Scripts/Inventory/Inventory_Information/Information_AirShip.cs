using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Information_AirShip : MonoBehaviour
{
    private GameObject _airShip_Information_Position;
    private GameObject _airShip_Information_Prefab;
    private Hashtable _airShip_Information_Prefab_Hash;

    public void Initialize_Information_AirShip(GameObject airShip_Information_Position,GameObject airShip_Information_Prefab)
    {
        Debug.Log("初始化飞船信息");
        if (airShip_Information_Position == null) return;
        if (airShip_Information_Prefab == null) return;
        _airShip_Information_Position = airShip_Information_Position;
        _airShip_Information_Prefab = airShip_Information_Prefab;
        _airShip_Information_Prefab_Hash = new();
        Initialize_Information_AriShip_Prefab();
        Debug.Log("初始化飞船信息成功");
    }
    private void Initialize_Information_AriShip_Prefab()
    {
        Create_AirShip_Information_Prefab(Attribute_Type.队伍类型);
        Create_AirShip_Information_Prefab(Attribute_Type.生命值);
        Create_AirShip_Information_Prefab(Attribute_Type.重量);
        Create_AirShip_Information_Prefab(Attribute_Type.升力);
    }
    private void Create_AirShip_Information_Prefab(Attribute_Type attribute_Type)
    {
        GameObject attribute = Instantiate(_airShip_Information_Prefab, _airShip_Information_Position.transform);
        string name = "";
        switch (attribute_Type)
        {
            case Attribute_Type.队伍类型:
                name = "队伍类型";
                break;
            case Attribute_Type.生命值:
                name = "总生命值";
                break;
            case Attribute_Type.重量:
                name = "总重量";
                break;
            case Attribute_Type.升力:
                name = "总升力";
                break;
            default:
                attribute.SetActive(false);
                return;
        }
        attribute.name = $"飞船_属性_{name}";
        TextMeshProUGUI attribute_Name = attribute.transform.Find("名称").GetComponent<TextMeshProUGUI>();
        if (attribute_Name != null) attribute_Name.text = name;

        _airShip_Information_Prefab_Hash.Add(attribute_Type.ToString(), attribute);
    }
    private void Update_AirShip_Information_Prefab(Attribute_Type attribute_Type, string value)
    {
        if (_airShip_Information_Prefab_Hash == null) return;
        GameObject attribute = (GameObject)_airShip_Information_Prefab_Hash[attribute_Type.ToString()];
        if (attribute == null) return;

        TextMeshProUGUI attribute_Name = attribute.transform.Find("属性值").GetComponent<TextMeshProUGUI>();
        attribute_Name.text = value.ToString();
    }
    public void Update_Information_AirShip(Inventory_AirShip_Attribute_Information airShip_Attribute)
    {
        Update_AirShip_Information_Prefab(Attribute_Type.队伍类型, airShip_Attribute._groupType.ToString());
        Update_AirShip_Information_Prefab(Attribute_Type.生命值, airShip_Attribute._airShip_Healthy.ToString());
        Update_AirShip_Information_Prefab(Attribute_Type.重量, airShip_Attribute._airShip_Weight.ToString());
        Update_AirShip_Information_Prefab(Attribute_Type.升力, airShip_Attribute._airShip_LiftForce.ToString());
    }
}
