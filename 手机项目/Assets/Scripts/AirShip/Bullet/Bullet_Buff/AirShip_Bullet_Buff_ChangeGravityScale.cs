using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip_Bullet_Buff_ChangeGravityScale : AirShip_Bullet_Buff_Base
{
    private float _change_GravityScale;

    public AirShip_Bullet_Buff_ChangeGravityScale(float change_GravityScale)
    {
        _change_GravityScale = change_GravityScale;
    }

    public override void Buff_Bullet(AirShip_Bullet_Attribute airShip_Bullet_Attribute_Current)
    {
        airShip_Bullet_Attribute_Current.Change_GravityScale(_change_GravityScale);
    }
}
