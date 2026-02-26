using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Time_Count_Once : Time_Counter
{
    public Time_Count_Once(float cooling_Time_Max, UnityAction action):base(cooling_Time_Max, action) { }
    public override void Trigger()
    {
        if (_action != null && _is_Active)
        {
            _action.Invoke();
            Stop_Counter();
        }
    }
}
