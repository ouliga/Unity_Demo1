using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Manager : Singleton<Event_Manager>
{
    public Shop_UI_Reset_Event _shop_UI_Reset_Event{ get; private set; }

    private void Start()
    {
        _shop_UI_Reset_Event = new Shop_UI_Reset_Event();
    }
}
public class Shop_UI_Reset_Event
{
    public event Action _reset;
    public void Invoke()
    {
        _reset?.Invoke();
    }
}



