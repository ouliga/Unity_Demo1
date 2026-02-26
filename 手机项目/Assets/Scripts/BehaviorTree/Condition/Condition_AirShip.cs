using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition_AirShip : Behavior
{
    protected AirShip_Controller _airShip_Controller_Player;
    protected AirShip_Controller _airShip_Controller_Enemy;

    protected Condition_AirShip(AirShip_Controller airShip_Controller_Player, AirShip_Controller airShip_Controller_Enemy)
    {
        _airShip_Controller_Player = airShip_Controller_Player;
        _airShip_Controller_Enemy = airShip_Controller_Enemy;
    }

}
