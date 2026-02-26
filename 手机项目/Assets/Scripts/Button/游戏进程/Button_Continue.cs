using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Continue : Button_Base
{
    [SerializeField] private string _stringpath;

    public override void OnClick()
    {
        if(Data_Manager.Instance != null)
        {
            Data_Manager.Instance.LoadData(_stringpath);
        }
        if(Game_Manager.Instance != null)
        {
            Game_Manager.Instance.Load_Game_Difficulty_Degree_Data();
        }
        if(Scene_Manager.Instance != null)
        {
            Scene_Manager.Instance.Enter_Scene(Scene_Type.WorldScene);
        }
    }
}
