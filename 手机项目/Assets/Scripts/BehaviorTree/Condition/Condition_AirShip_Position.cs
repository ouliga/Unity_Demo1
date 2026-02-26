using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition_AirShip_Position : Condition_AirShip
{
    public Condition_AirShip_Position(AirShip_Controller airShip_Controller_Player, AirShip_Controller airShip_Controller_Enemy) : base(airShip_Controller_Player, airShip_Controller_Enemy) { }

    protected override EStatus OnUpdate()
    {
        //Debug.Log("Conditon_AirShip_Position OnUpdate");
        //当玩家的垂直速度大于敌方的垂直速度，执行
        if (_airShip_Controller_Player == null || _airShip_Controller_Enemy == null)
        {
            Debug.Log("玩家飞船控制器或敌方飞船控制器未初始化");
            return EStatus.Failure;
        }

        Vector3 position_Player = _airShip_Controller_Player.Get_AirShip_CenterPosition();
        Vector3 position_Enemy = _airShip_Controller_Enemy.Get_AirShip_CenterPosition();

        if(position_Player.y >= position_Enemy.y)
        {
            //Debug.Log("玩家飞船的位置在敌方飞船上方");
            return EStatus.Success;
        }
        else
        {
            //Debug.Log("玩家飞船的位置在敌方飞船下方");
            return EStatus.Failure;
        }


    }
}
