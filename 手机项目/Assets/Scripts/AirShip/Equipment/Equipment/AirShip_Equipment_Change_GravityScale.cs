using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip_Equipment_Change_GravityScale : AirShip_Equipment
{
    [SerializeField] private float _gravityScale_Change;
    public override void Initialize_AirShip_Equipment_Buff_Bullet()
    {
        AirShip_Bullet_Buff_ChangeGravityScale airShip_Bullet_Buff_ChangeGravityScale = new AirShip_Bullet_Buff_ChangeGravityScale(_gravityScale_Change);
        if (_airhip_Equipment_Buff_Bullet != null)
        {
            _airhip_Equipment_Buff_Bullet.Add_Buff(airShip_Bullet_Buff_ChangeGravityScale);
        }
    }
}
