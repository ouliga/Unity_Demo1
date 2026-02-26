using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Return_PreScene : Button_Base
{
    public override void OnClick()
    {
        if(Scene_Manager.Instance != null)
        {
            Scene_Manager.Instance.Return_PreScene();
        }
    }
}
