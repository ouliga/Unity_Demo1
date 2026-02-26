using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip_Controller_Fly
{
    public float _speed_Max_Fly_Up { get; private set; }
    public float _speed_Min_Fly_Down { get; private set; }
    public float _acceleratedSpeed_Fly_Up { get; private set; }

    public float _decreasedSpeed_Fly_Down { get; private set; }

    public float _decreasedSpeed_Fall_Down { get; private set; }

    public AirShip_Controller_Fly()
    {
        _speed_Max_Fly_Up = 4f;
        _speed_Min_Fly_Down = -5f;
        _acceleratedSpeed_Fly_Up = 2.5f;
        _decreasedSpeed_Fly_Down = -0.5f;
        _decreasedSpeed_Fall_Down = -2.5f;
    }
    public void AirShip_Fly_UP(Rigidbody2D rigidbody_AirShip,AirShip_Controller_Attribute airShip_Controller_Attribute)
    {

        if (rigidbody_AirShip == null)
        {
            Debug.Log("飞船的刚体未初始化");
            return;
        }
        if(airShip_Controller_Attribute == null)
        {
            Debug.Log("未传入飞船数据");
            return;
        }
        float change_Speed_Value = _acceleratedSpeed_Fly_Up / (1 + Mathf.Exp(-(airShip_Controller_Attribute._airShip_LiftForce - airShip_Controller_Attribute._airShip_Weight)));
        Change_AirShip_Speed_Y(rigidbody_AirShip, change_Speed_Value);
    }
    public void AirShip_Fly_Down(Rigidbody2D rigidbody_AirShip)
    {
        if (rigidbody_AirShip == null)
        {
            Debug.Log("飞船的刚体未初始化");
            return;
        }

        Change_AirShip_Speed_Y(rigidbody_AirShip, _decreasedSpeed_Fly_Down);
    }

    public void AirShip_Fall_Down(Rigidbody2D rigidbody_AirShip)
    {
        if (rigidbody_AirShip == null)
        {
            Debug.Log("飞船的刚体未初始化");
            return;
        }

        Change_AirShip_Speed_Y(rigidbody_AirShip, _decreasedSpeed_Fall_Down * Time.deltaTime);
    }

    public void Change_AirShip_Speed_Y(Rigidbody2D rigidbody_AirShip,float change_Value)
    {
        if (rigidbody_AirShip == null) return;

        float speed_Current = rigidbody_AirShip.velocity.y + change_Value;
        if (speed_Current > _speed_Max_Fly_Up) speed_Current = _speed_Max_Fly_Up;
        if (speed_Current < _speed_Min_Fly_Down) speed_Current = _speed_Min_Fly_Down;
        rigidbody_AirShip.velocity = new Vector2(0, speed_Current);
    }

}
