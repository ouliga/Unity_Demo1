using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip_Bullet_Block : AirShip_Bullet
{
    public override void Hit_Component(AirShip_Component airShip_Component)
    {
        //if (airShip_Component == null) return;
        //airShip_Component.TakeDamage(_bullet_Attribute_Current._bullet_Damage);
        //Release_AirShip_Bullet();
    }
    public override void Hit_Bullet(AirShip_Bullet airShip_Bullet)
    {
        if(airShip_Bullet == null) return;
        airShip_Bullet.BeBlocked();
        Release_AirShip_Bullet();
    }
    public override void BeBlocked() { }

}
