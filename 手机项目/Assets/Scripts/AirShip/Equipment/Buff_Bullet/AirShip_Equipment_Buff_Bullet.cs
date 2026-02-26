using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip_Equipment_Buff_Bullet
{
    private List<AirShip_Bullet_Buff_Base> _buff_List;
    public void Buff_Bullet(AirShip_Bullet_Attribute _bullet_Attribute_Current)
    {
        if(_buff_List != null)
        {
            foreach(AirShip_Bullet_Buff_Base airShip_Bullet_Buff in _buff_List)
            {
                airShip_Bullet_Buff.Buff_Bullet(_bullet_Attribute_Current);
            }
        }
    }
    public void Add_Buff(AirShip_Bullet_Buff_Base airShip_Bullet_Buff)
    {
        if (_buff_List == null) _buff_List = new();
        _buff_List.Add(airShip_Bullet_Buff);
    }
}
