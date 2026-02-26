using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip_Equipment_ChangeSpeed : AirShip_Equipment
{
    [SerializeField] private float _change_Speed;
    public override void Initialize_AirShip_Equipment_Buff_Bullet()
    {
        AirShip_Bullet_Buff_ChangeSpeed airShip_Bullet_Buff_ChangeSpeed = new AirShip_Bullet_Buff_ChangeSpeed(_change_Speed);
        if (_airhip_Equipment_Buff_Bullet != null)
        {
            _airhip_Equipment_Buff_Bullet.Add_Buff(airShip_Bullet_Buff_ChangeSpeed);
        }
    }
}
