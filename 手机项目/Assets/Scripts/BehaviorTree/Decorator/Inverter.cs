using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : Decorator
{
    protected override EStatus OnUpdate()
    {
        _child.Tick();
        if (_child._isFailure)
            return EStatus.Success;
        if (_child._isSuccess)
            return EStatus.Failure;
        return EStatus.Running;
    }
}
