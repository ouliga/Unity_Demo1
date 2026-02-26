using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class State_GameStart : IState
{
    public State_GameStart(FSM_BattleManager fsm)
    {
        _fsm = fsm;
    }
    public override void Enter()
    {
        //if (Battle_Manager.Instance == null) return;
        //Battle_Manager.Instance.Battle_Start();
        //throw new System.NotImplementedException();
        if (_fsm != null)
        {
            _fsm.Battle_Start();
        }
    }
    public override void Exit()
    {
    }
    public override void Update()
    {
        if( _fsm != null)
        {
            _fsm.Battle_Start_Update();
        }
    }
    public override void Button_Event(ButtonType buttonType)
    {
        if (_fsm != null)
        {
            switch (buttonType)
            {
                case ButtonType.AirShip_Fly:
                    _fsm.AirShip_Player_Fly_Up();
                    break;
            }
        }
    }
}
