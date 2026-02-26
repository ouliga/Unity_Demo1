using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_GamePrepare : IState
{
    public State_GamePrepare(FSM_BattleManager fsm)
    {
        _fsm = fsm;
    }
    public override void Enter()
    {
        if(_fsm != null)
        {
            _fsm.Battle_Prepare();
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
        Battle_Manager battle_Manager = Battle_Manager.Instance;
        if (battle_Manager == null) return;
        switch (buttonType)
        {
            case ButtonType.GameStart:
                battle_Manager.FSM_Enter_State(StateType.gameStart);
                break;
        }

    }
}
