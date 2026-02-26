using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Open_Difficulty : Button_Base
{
    public override void OnClick()
    {
        if (Data_Manager.Instance != null)
        {
            Data_Manager.Instance.CreateData();
        }
        else
        {
            Debug.Log("存档管理器未初始化");
            return;
        }
        if (UI_Panel_Manager.Instance != null)
        { 
            UI_Panel_Manager.Instance.Open_Difficulty_Panel();
        }
        if(Game_Manager.Instance != null)
        {
            Game_Manager.Instance.Reset_Buff();
            Game_Manager.Instance.Update_Buff_Description();
            Game_Manager.Instance.Update_Buff_Description_Title();
        }
    }
}
