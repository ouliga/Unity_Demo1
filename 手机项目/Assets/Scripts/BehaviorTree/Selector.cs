using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Sequence
{
    protected override EStatus OnUpdate()
    {
        //Debug.Log("Selector OnUpdate");
        while (true)
        {
            var s = _currentChild.Value.Tick();
            if (s != EStatus.Failure) return s;

            _currentChild = _currentChild.Next;

            if (_currentChild == null) return EStatus.Failure;

        }
    }
}
