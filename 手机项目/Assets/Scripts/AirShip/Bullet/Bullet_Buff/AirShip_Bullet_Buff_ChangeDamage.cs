using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip_Bullet_Buff_ChangeDamage : AirShip_Bullet_Buff_Base
{
    private int _damage_Change;
    public AirShip_Bullet_Buff_ChangeDamage(int damage_Change)
    {
        _damage_Change = damage_Change;
    }
    public override void Buff_Bullet(AirShip_Bullet_Attribute airShip_Bullet_Attribute_Current)
    {
        airShip_Bullet_Attribute_Current.Change_Damage(_damage_Change);
    }
}
