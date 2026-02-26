using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public enum Item_Type
{
    None,Cube,Equipment
}
public enum Equipment_Type
{
    None,Weapon,Tool
}
public enum Bullet_Type
{
    None,Common,Pierce,Block
}
public class Item
{
    public Sprite _display_UI { get; }
    public int _id { get; }
    public string _name { get; }
    public Item_Type _type { get;}
    public Equipment_Type _equipment_Type { get; }
    public bool _isCore { get; }
    public int _rare {  get; }
    public int _weight { get; }
    public int _liftForce { get; }
    public int _healthy { get; }
    public int _attack_Times {  get; }
    public float _attack_CD {  get; }
    public float _attack_Delay {  get; }
    public int _bullet_Damage {  get; }
    public float _bullet_Angle { get; }
    public float _bullet_Speed {  get; }
    public float _bullet_ExistTime {  get; }
    public Bullet_Type _bullet_Type { get; }

    public string _description { get; }

    public Item()
    {
        _id = -1;
        _name = "";
        _type = Item_Type.None;
        _equipment_Type = Equipment_Type.None;
        _isCore = false;
        _rare = 0;
        _weight = 0;
        _liftForce = 0;
        _healthy = 0;
        _attack_Times = -1;
        _attack_CD = -1;
        _attack_Delay = 0;
        _bullet_Damage = 0;
        _bullet_Angle = 0;
        _bullet_Speed = 0;
        _bullet_ExistTime = 0;
        _bullet_Type = Bullet_Type.None;
        _description = "";
    }
    /// <summary>
    /// 通过id在数据库中检索数据
    /// </summary>
    public Item(int id,Data_SO data_SO)
    {
        _id = id;
        foreach (ExcelData excelData in data_SO._data)
        {
            if(id == excelData._id)
            {
                _display_UI = LoadSprite(id);
                _name = excelData._name;
                _type = GetType(excelData._type);
                _equipment_Type = Get_EquipmentType(excelData._equipment_Type);
                _isCore = excelData._isCore;
                _rare = excelData._rare;
                _weight = excelData._weight;
                _liftForce = excelData._liftForce;
                _healthy = excelData._healthy;
                _attack_Times = excelData._attack_Times;
                _attack_CD = excelData._attack_CD;
                _attack_Delay = excelData._attack_Delay;
                _bullet_Damage = excelData._bullet_Damage;
                _bullet_Angle= excelData._bullet_Angle;
                _bullet_Speed= excelData._bullet_Speed;
                _bullet_ExistTime = excelData._bullet_ExistTime;
                _bullet_Type = Get_BulletType(excelData._bullet_Type);
                _description = excelData._description;

                Debug.Log($"成功创建物品(id:{id},bullet_Type:{_bullet_Type})");
                return;
            }
        }
        Debug.Log("未找到物品数据id：" + id);
    }
    private Sprite LoadSprite(int id)
    {
        string path = "UI/物品贴图/Item_" + id;
        Sprite sprite = Resources.Load<Sprite>(path);
        if(sprite == null)
        {
            Debug.Log("并未找到对应图像id：" + id);
        }
        return sprite;
    }
    private Item_Type GetType(string type)
    {
        if(type == "cube")
        {
            return Item_Type.Cube;
        }
        else if(type == "equipment")
        {
            return Item_Type.Equipment;
        }
        Debug.Log("未找到对应标签");
        return Item_Type.None;
    }
    private Equipment_Type Get_EquipmentType(string type)
    {
        if(type == "weapon")
        {
            return Equipment_Type.Weapon;
        }
        else if (type == "tool")
        {
            return Equipment_Type.Tool;
        }
        return Equipment_Type.None;
    }
    private Bullet_Type Get_BulletType(string type)
    {
        if(type.Equals("common"))
        {
            return Bullet_Type.Common;
        }
        else if (type.Equals("pierce"))
        {
            return Bullet_Type.Pierce;
        }
        else if (type.Equals("block"))
        {
            return Bullet_Type.Block;
        }
        return Bullet_Type.None;
    }
}
