using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class State_GamePause : IState
{
    public State_GamePause(FSM_BattleManager fsm)
    {
        _fsm = fsm;
    }
    public override void Enter()
    {
        //throw new System.NotImplementedException();
    }
    public override void Exit()
    {
        //throw new System.NotImplementedException();
    }
    public override void Update()
    {
        //throw new System.NotImplementedException();
    }
    public override void Button_Event(ButtonType buttonType)
    {
        //throw new System.NotImplementedException();
    }
}
