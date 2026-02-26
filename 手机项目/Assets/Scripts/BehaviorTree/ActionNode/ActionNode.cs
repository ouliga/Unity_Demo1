using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : Behavior
{
    private string _description;
    private Action _action;

    public ActionNode(string description,Action action)
    {
        _description = description;
        _action = action;
    }
    protected override EStatus OnUpdate()
    {
        Debug.Log(_description);
        if(_action != null)
        {
            _action.Invoke();
        }
        return EStatus.Success;
    }
}
