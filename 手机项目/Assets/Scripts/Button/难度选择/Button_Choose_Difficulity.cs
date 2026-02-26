using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Choose_Difficulity : Button_Base
{
    [SerializeField] int _difficulity_Degree;
    public override void OnClick()
    {
        if(Game_Manager.Instance != null)
        {
            Game_Manager.Instance.Config_Buff_By_Game_Difficulty_Degree(_difficulity_Degree);
            Game_Manager.Instance.Update_Buff_Description();
            Game_Manager.Instance.Update_Buff_Description_Title();
            Game_Manager.Instance.Save_Game_Difficulty_Degree_Data();
        }
    }
}
