using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Button_AirShip_Fly : Button_Base
{
    public override void OnClick()
    {
        if (Battle_Manager.Instance != null)
        {
            Battle_Manager.Instance.FSM_Button_Event(ButtonType.AirShip_Fly);
        }
        else
        {
            Debug.Log("战役管理器还未初始化");
        }
    }
}
