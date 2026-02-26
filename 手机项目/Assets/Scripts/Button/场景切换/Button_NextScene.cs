using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_NextScene : Button_Base
{

    [SerializeField]private Scene_Type _sceneType;

    public override void OnClick()
    {
        Scene_Manager.Instance.Enter_Scene(_sceneType);
    }

}
