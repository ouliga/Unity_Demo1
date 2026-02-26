using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Time_Counter
{
    public int _id { get; private set; }
    private static int _uuid;
    protected bool _is_Active;
    public float _cooling_Time { get; private set; }
    public float _cooling_Time_Max { get; private set; }
    protected UnityAction _action;

    public Time_Counter(float cooling_Time_Max,UnityAction action)
    {
        _id = _uuid++;
        _is_Active = true;
        _cooling_Time = cooling_Time_Max;
        _cooling_Time_Max = cooling_Time_Max;
        _action = action;
    }
    public void Reset_Counter()
    {
        _cooling_Time = _cooling_Time_Max;
    }
    public void Active_Counter()
    {
        _is_Active = true;
    }
    public void Stop_Counter()
    {
        _is_Active = false;
    }
    public virtual void Trigger()
    {
        if(_action != null && _is_Active)
        {
            _action.Invoke();
        }
    }
    public void Count()
    {
        if (!_is_Active) return;
        _cooling_Time -= Time.deltaTime;
    }

}
