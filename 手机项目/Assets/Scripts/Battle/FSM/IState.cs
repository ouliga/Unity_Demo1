using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public abstract class IState
{
    protected FSM_BattleManager _fsm;
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
    public abstract void Button_Event(ButtonType buttonType);
}
