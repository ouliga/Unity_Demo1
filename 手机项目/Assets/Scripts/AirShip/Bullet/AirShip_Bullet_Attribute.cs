using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip_Bullet_Attribute
{
    public float _bullet_Size {  get; private set; }
    public int _bullet_Damage {  get;private set; }
    public float _bullet_Angle { get; private set; }
    public float _bullet_Speed { get; private set; }
    public float _bullet_GravityScale {  get; private set; }
    public float _bullet_ExistTime { get; private set; }
    public Bullet_Type _bullet_Type { get; private set; }
    public Transform _bullet_Position { get; private set; }
    public int _bullet_Direction { get; private set; }
    public GroupType _bullet_GroupType { get; private set; }

    public AirShip_Bullet_Attribute() { }
    public AirShip_Bullet_Attribute(Item item, Transform bullet_Position,int bullet_Direction, GroupType bullet_GroupType)
    {
        if (item == null) return;
        _bullet_Size = 1;
        _bullet_Damage = item._bullet_Damage;
        _bullet_Angle = item._bullet_Angle;
        _bullet_Speed = item._bullet_Speed;
        _bullet_GravityScale = 0;
        _bullet_ExistTime = item._bullet_ExistTime;
        _bullet_Type = item._bullet_Type;
        _bullet_Position = bullet_Position;
        _bullet_Direction = bullet_Direction;
        _bullet_GroupType = bullet_GroupType;
    }
    public void Copy(AirShip_Bullet_Attribute airShip_Bullet_Attribute)
    {
        if(airShip_Bullet_Attribute == null) return;
        _bullet_Size = airShip_Bullet_Attribute._bullet_Size;
        _bullet_Damage = airShip_Bullet_Attribute._bullet_Damage;
        _bullet_Angle = airShip_Bullet_Attribute._bullet_Angle;
        _bullet_Speed = airShip_Bullet_Attribute._bullet_Speed;
        _bullet_GravityScale = airShip_Bullet_Attribute._bullet_GravityScale;
        _bullet_ExistTime = airShip_Bullet_Attribute._bullet_ExistTime;
        _bullet_Type = airShip_Bullet_Attribute._bullet_Type;
        _bullet_Position = airShip_Bullet_Attribute._bullet_Position;
        _bullet_Direction = airShip_Bullet_Attribute._bullet_Direction;
        _bullet_GroupType = airShip_Bullet_Attribute._bullet_GroupType;
    }
    public void Change_Damage(int damage_Change)
    {
        _bullet_Damage += damage_Change;
    }
    public void Change_Speed(float speed_Change)
    {
        _bullet_Speed += speed_Change;
    }
    public void Change_GravityScale(float gravityScale_Change)
    {
        _bullet_GravityScale += gravityScale_Change;
    }
    public void Change_Size(float size_Change)
    {
        _bullet_Size += size_Change;
    }

}
