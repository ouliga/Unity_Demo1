using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorManager
{
    public float _oprate_Time { get; private set; }
    private BehaviorTree _behaviorTree_AirShip_Fly;
    private Time_Counter _counter_AirShip_Fly;
    private AirShip_Controller _airShip_Controller_Player;
    private AirShip_Controller _airShip_Controller_Enemy;

    public BehaviorManager(AirShip_Controller airShip_Controller_Player,AirShip_Controller airShip_Controller_Enemy)
    {
        _airShip_Controller_Player = airShip_Controller_Player;
        _airShip_Controller_Enemy = airShip_Controller_Enemy;
        _oprate_Time = 0.1f;
    }

    public void Bulid_BehaviorTree_AirShip_Enemy_Fly(Action action_AirShip_Enemy_Fly_Up,Action action_AirShip_Enemy_Fly_Down)
    {

        if (_airShip_Controller_Player == null || _airShip_Controller_Enemy == null)
        {
            Debug.Log("飞船控制器未初始化");
            return;
        }
        //根节点为选择器
        Behavior root = new Selector();
        //选择器的0号节点
        Filter root_0 = new Filter();
        root.AddChild(root_0);
        //条件：玩家飞船位置在上
        Condition_AirShip_Position root_0_Condition_AirShip_Position = new Condition_AirShip_Position(_airShip_Controller_Player, _airShip_Controller_Enemy);
        root_0.AddCondition(root_0_Condition_AirShip_Position);
        ActionNode root_0_ActionNode = new ActionNode("敌方飞船试图起飞",action_AirShip_Enemy_Fly_Up);
        root_0.AddAction(root_0_ActionNode);

        //选择器的1号节点
        Filter root_1 = new Filter();
        root.AddChild(root_1);
        //条件：玩家飞船位置在下
        Condition_AirShip_Position root_1_Condition_AirShip_Position = new Condition_AirShip_Position(_airShip_Controller_Player, _airShip_Controller_Enemy);
        Inverter root_1_Inverter = new Inverter();
        root_1_Inverter.AddChild(root_1_Condition_AirShip_Position);
        root_1.AddCondition(root_1_Inverter);
        ActionNode root_1_ActionNode = new ActionNode("敌方飞船试图下降", action_AirShip_Enemy_Fly_Down);
        root_1.AddChild(root_1_ActionNode);



        _behaviorTree_AirShip_Fly = new BehaviorTree(root);
        if (Time_Manager.Instance != null)
        {
            _counter_AirShip_Fly = Time_Manager.Instance.Create_Time_Counter(_oprate_Time, Tick);
        }
    }
    public void Tick()
    {
        if(_behaviorTree_AirShip_Fly != null)
        {
            Debug.Log("尝试Tick");
            _behaviorTree_AirShip_Fly.Tick();
        }
    }
}
