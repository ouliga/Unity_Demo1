using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AirShip_Controller_Buff_Base
{
    public string _buff_Description{ get; protected set;}
    public abstract void Buff_AirShip_Components(AirShip_Component[,] airShip_Components);
    public abstract void Buff_AirShip_Controller_Fly(AirShip_Controller_Fly airShip_Controller_Fly);

}
