using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip_Equipment_Change_Size : AirShip_Equipment
{
    [SerializeField] private float _size_Change;
    public override void Initialize_AirShip_Equipment_Buff_Bullet()
    {
        AirShip_Bullet_Buff_ChangeSize airShip_Bullet_Buff_ChangeSize = new AirShip_Bullet_Buff_ChangeSize(_size_Change);
        if (_airhip_Equipment_Buff_Bullet != null)
        {
            _airhip_Equipment_Buff_Bullet.Add_Buff(airShip_Bullet_Buff_ChangeSize);
        }
    }
}
