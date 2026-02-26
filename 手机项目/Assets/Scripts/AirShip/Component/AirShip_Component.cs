using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class AirShip_Component : MonoBehaviour
{
    public GroupType _groupType { get; private set; }
    private Item _item_Cube;
    private Item _item_Equipment;

    public bool _isAlive { get; private set; }
    public bool _isCore { get; private set; }
    public int _weight { get; private set; }
    public int _liftForce { get; private set; }
    public int _healthy { get; private set; }

    //装备
    private AirShip_Equipment _airShip_Equipment;
    //事件
    private Action _action_Update_AirShip_Attributes;
    private Action _action_Check_AirShip_Components_Valid;

    public void Initialize_AirShip_Component(Item item_Cube, Item item_Equipment, GroupType groupType)
    {
        _groupType = groupType;
        _isAlive = true;
        _item_Cube = item_Cube;
        _item_Equipment = item_Equipment;
        _weight = item_Cube._weight;
        _liftForce = item_Cube._liftForce;
        _healthy = item_Cube._healthy;

        if (item_Equipment != null)
        {
            _isCore = item_Equipment._isCore;
            _weight += item_Equipment._weight;
            _healthy += item_Equipment._healthy;
            _liftForce += item_Equipment._liftForce;
        }
    }
    //初始化事件
    public void Initialize_Action_Update_AirShip_Attributes(Action action)
    {
        if (action == null) return;
        _action_Update_AirShip_Attributes = action;
    }
    public void Initialize_Action_Check_AirShip_Components_Valid(Action action)
    {
        if (action == null) return;
        _action_Check_AirShip_Components_Valid = action;
    }
    //添加装备
    public void Set_AirShip_Equipment(AirShip_Equipment airShip_Equipment,Item item_Equipment, GroupType groupType, int direction)
    {
        if (airShip_Equipment == null) return;
        _airShip_Equipment = airShip_Equipment;
        airShip_Equipment.Initialize_AirShip_Equipment(item_Equipment,groupType,direction);
    }
    //检测是否撞击
    public bool Check_Hit()
    {
        return _isAlive;
    }
    //收到伤害
    public void TakeDamage(int damage_Num)
    {
        Debug.Log($"{_groupType.ToString()}:{gameObject.name}尝试受到{damage_Num}点伤害");
        if (!_isAlive) return;

        Change_Healthy(-damage_Num);

        Debug.Log($"{_groupType.ToString()}:{gameObject.name}受到{damage_Num}点伤害");

        if (_healthy > 0) return;
        Destory_Component();
    }
    //摧毁
    public void Destory_Component()
    {
        if (!_isAlive) return;
        _isAlive = false;
        
        if (_airShip_Equipment != null)
        {
            _airShip_Equipment.Stop_Equipment();
        }

        if (_action_Update_AirShip_Attributes != null)
        {
            _action_Update_AirShip_Attributes.Invoke();
        }

        if(_action_Check_AirShip_Components_Valid != null)
        {
            _action_Check_AirShip_Components_Valid.Invoke();
        }

        gameObject.SetActive(false);
    }
    //改变属性
    public void Change_Healthy(int change_Num)
    {
        _healthy += change_Num;
        if(_healthy <= 0) _healthy = 0;

        if (_action_Update_AirShip_Attributes != null)
        {
            _action_Update_AirShip_Attributes.Invoke();
        }
    }

}
