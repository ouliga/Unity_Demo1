using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip_Bullet_Pierce : AirShip_Bullet
{
    public override void Hit_Component(AirShip_Component airShip_Component)
    {
        if (airShip_Component == null) return;
        airShip_Component.TakeDamage(_bullet_Attribute_Current._bullet_Damage);
    }
    public override void Hit_Bullet(AirShip_Bullet airShip_Bullet)
    {
        if (airShip_Bullet == null) return;
    }
    public override void BeBlocked()
    {
        Release_AirShip_Bullet();
    }
}
