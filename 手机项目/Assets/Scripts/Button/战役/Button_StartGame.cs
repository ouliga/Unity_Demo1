using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;
public class Button_StartGame : Button_Base
{
    public override void OnClick()
    {
        if (Battle_Manager.Instance == null) return;
        Battle_Manager.Instance.FSM_Button_Event(ButtonType.GameStart);
        gameObject.SetActive(false);
    }
}
