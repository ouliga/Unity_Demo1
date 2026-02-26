using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip_Bullet_Fly
{
    private Rigidbody2D _rigidbody2D;
    private float _random_Angle;
    public AirShip_Bullet_Fly(Rigidbody2D rigidbody2D)
    {
        _rigidbody2D = rigidbody2D;
    }

    public void Update_AirShip_Bullet_Rigidbody(AirShip_Bullet_Attribute bullet_Attribute_Current)
    {
        if (bullet_Attribute_Current == null) return;
        if (_rigidbody2D == null) return;
        float current_Speed = bullet_Attribute_Current._bullet_Speed;
        float velocity_X = current_Speed * Mathf.Cos(_random_Angle * Mathf.PI / 180) * bullet_Attribute_Current._bullet_Direction;
        float velocity_Y = current_Speed * Mathf.Sin(_random_Angle * Mathf.PI / 180);
        _rigidbody2D.velocity = new Vector3(velocity_X, velocity_Y, 0);
        _rigidbody2D.gravityScale = bullet_Attribute_Current._bullet_GravityScale;
    }
    //重置子弹的运动状态
    public void Reset_AirShip_Bullet_Rigidbody(AirShip_Bullet_Attribute bullet_Attribute_Current)
    {
        if (bullet_Attribute_Current == null) return;
        if (_rigidbody2D == null) return;
        _random_Angle = Get_Random_Angle(bullet_Attribute_Current);
        float velocity_X = bullet_Attribute_Current._bullet_Speed * Mathf.Cos(_random_Angle * Mathf.PI / 180) * bullet_Attribute_Current._bullet_Direction;
        float velocity_Y = bullet_Attribute_Current._bullet_Speed * Mathf.Sin(_random_Angle * Mathf.PI / 180);
        _rigidbody2D.velocity = new Vector3(velocity_X, velocity_Y, 0);
        _rigidbody2D.gravityScale = bullet_Attribute_Current._bullet_GravityScale;
    }
    private float Get_Random_Angle(AirShip_Bullet_Attribute bullet_Attribute_Current)
    {
        if (bullet_Attribute_Current == null) return 0;
        else
        {
            return Random.Range(-bullet_Attribute_Current._bullet_Angle, bullet_Attribute_Current._bullet_Angle);
        }
    }
}
