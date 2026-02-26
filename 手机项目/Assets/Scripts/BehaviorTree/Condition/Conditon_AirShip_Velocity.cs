using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditon_AirShip_Velocity : Condition_AirShip
{
    public Conditon_AirShip_Velocity(AirShip_Controller airShip_Controller_Player, AirShip_Controller airShip_Controller_Enemy): base(airShip_Controller_Player, airShip_Controller_Enemy)
    {
    }

    protected override EStatus OnUpdate()
    {
        //Debug.Log("Conditon_AirShip_Velocity OnUpdate");
        //当玩家的垂直速度大于敌方的垂直速度，执行
        if (_airShip_Controller_Player == null || _airShip_Controller_Enemy == null)
        {
            Debug.Log("玩家飞船控制器或敌方飞船控制器未初始化");
            return EStatus.Failure;
        }
        
        Vector3 velocity_Player = _airShip_Controller_Player.Get_AirShip_Velocity();
        Vector3 velocity_Enemy = _airShip_Controller_Enemy.Get_AirShip_Velocity();

        if(velocity_Player.y >= velocity_Enemy.y)
        {
            //Debug.Log("检测到玩家飞船的速度大于敌方飞船的速度");
            return EStatus.Success;
        }
        else
        {
            //Debug.Log("检测到玩家飞船的速度小于敌方飞船的速度");
            return EStatus.Failure;
        }
    }
        
}
