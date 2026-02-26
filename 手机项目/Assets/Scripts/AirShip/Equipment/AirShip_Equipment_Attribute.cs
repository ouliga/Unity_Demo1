using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShip_Equipment_Attribute
{
    public int _attack_Times {  get; private set; }
    public float _attack_CD { get; private set; }
    public float _attack_Delay {  get; private set; }
    public AirShip_Equipment_Attribute(Item item)
    {
        if (item == null) return;
        _attack_Times = item._attack_Times;
        _attack_CD = item._attack_CD;
        _attack_Delay = item._attack_Delay;
    }

}
