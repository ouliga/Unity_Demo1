using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip_Bullet_Buff_ChangeSpeed : AirShip_Bullet_Buff_Base {
    private float _change_Speed;

    public AirShip_Bullet_Buff_ChangeSpeed(float change_Speed)
    {
        _change_Speed = change_Speed;
    }

    public override void Buff_Bullet(AirShip_Bullet_Attribute airShip_Bullet_Attribute_Current)
    {
        airShip_Bullet_Attribute_Current.Change_Speed(_change_Speed);
    }

}
