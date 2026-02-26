using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree
{
    public bool _haveRoot => _root != null;
    private Behavior _root;//¸ù½Úµã
    public BehaviorTree(Behavior root)
    {
        _root = root;
    }
    public void Tick()
    {
        _root.Tick();
    }
    public void SetRoot(Behavior root)
    {
        _root = root;
    }
}
