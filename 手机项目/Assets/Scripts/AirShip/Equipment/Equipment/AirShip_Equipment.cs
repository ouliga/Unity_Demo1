using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Pool;

public abstract class AirShip_Equipment : MonoBehaviour
{
    public GroupType _groupType {  get; private set; }
    private bool _isActive; 
    [SerializeField] private Transform _bullet_Position;
    private AirShip_Bullet_Attribute _airShip_Bullet_Attribute;
    private AirShip_Equipment_Attribute _airShip_Equipment_Attribute;
    private AirShip_Equipment_Shoot _airShip_Equipment_Shoot;
    protected AirShip_Equipment_Buff_Bullet _airhip_Equipment_Buff_Bullet;

    public void Initialize_AirShip_Equipment(Item item_Equipment,GroupType groupType,int direction)
    {
        if(item_Equipment == null)
        {
            Debug.Log("物品信息为空");
            return;
        }
        _groupType = groupType;
        _isActive = true;
        //子弹数据
        _airShip_Bullet_Attribute = new AirShip_Bullet_Attribute(item_Equipment, _bullet_Position,direction,groupType);
        //飞船装备数据
        _airShip_Equipment_Attribute = new AirShip_Equipment_Attribute(item_Equipment);
        //装备射击功能
        _airShip_Equipment_Shoot = gameObject.AddComponent<AirShip_Equipment_Shoot>();
        _airShip_Equipment_Shoot.Initialize_AirShip_Equipment_Shoot(_airShip_Equipment_Attribute,_airShip_Bullet_Attribute);
        //装备Buff子弹功能
        _airhip_Equipment_Buff_Bullet = new AirShip_Equipment_Buff_Bullet();
        Initialize_AirShip_Equipment_Buff_Bullet();
    }
    //检测碰撞
    public bool Check_Hit()
    {
        return _isActive;
    }
    //停止装备
    public void Stop_Equipment()
    {
        _isActive = false;
        if (_airShip_Equipment_Shoot != null) _airShip_Equipment_Shoot.Stop_Shoot();
    }
    //增幅子弹
    public void Buff_Bullet(AirShip_Bullet_Attribute airShip_Bullet_Attribute_Current)
    {
        if (!_isActive) return;
        if (_airhip_Equipment_Buff_Bullet == null)
        {
            Debug.Log("装备BUFF子弹功能未初始化");
            return;
        }
        Debug.Log("装备：为子弹添加buff");
        _airhip_Equipment_Buff_Bullet.Buff_Bullet(airShip_Bullet_Attribute_Current);
    }

    public abstract void Initialize_AirShip_Equipment_Buff_Bullet();

}
