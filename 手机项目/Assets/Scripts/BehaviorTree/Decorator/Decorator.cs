using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decorator : Behavior
{
    protected Behavior _child;
    public override void AddChild(Behavior child)
    {
        _child = child;
    }
}
