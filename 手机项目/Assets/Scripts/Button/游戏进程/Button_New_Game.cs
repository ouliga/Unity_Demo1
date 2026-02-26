using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_New_Game : Button_Base
{
    public override void OnClick()
    {
        if(Game_Manager.Instance == null)
        {
            return;
        }
        else
        {
            if (!Game_Manager.Instance.Check_Game_Start())
            {
                return;
            }
        }
        //if (Data_Manager.Instance != null)
        //{
        //    Data_Manager.Instance.CreateData();
        //}
        //else
        //{
        //    Debug.Log("存档管理器未初始化");
        //    return;
        //}
        if (Scene_Manager.Instance != null)
        {
            Scene_Manager.Instance.Enter_Scene(Scene_Type.WorldScene);
        }
        else
        {
            Debug.Log("场景管理器未初始化");
        }
    }
}
