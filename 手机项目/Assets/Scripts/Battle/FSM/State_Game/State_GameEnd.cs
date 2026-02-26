using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_GameEnd : IState
{
    public State_GameEnd(FSM_BattleManager fsm)
    {
        _fsm = fsm;
    }
    public override void Enter()
    {
        if(_fsm != null)
        {
            _fsm.Battle_End();
        }
    }
    public override void Exit()
    {
    }
    public override void Update()
    {
    }
    public override void Button_Event(ButtonType buttonType)
    {
    }
}
