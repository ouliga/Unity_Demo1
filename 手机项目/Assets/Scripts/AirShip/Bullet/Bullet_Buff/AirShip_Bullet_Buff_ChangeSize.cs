using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip_Bullet_Buff_ChangeSize : AirShip_Bullet_Buff_Base
{
    private float _change_Size;

    public AirShip_Bullet_Buff_ChangeSize(float change_Size)
    {
        _change_Size = change_Size;
    }

    public override void Buff_Bullet(AirShip_Bullet_Attribute airShip_Bullet_Attribute_Current)
    {
        airShip_Bullet_Attribute_Current.Change_Size(_change_Size);
    }
}
