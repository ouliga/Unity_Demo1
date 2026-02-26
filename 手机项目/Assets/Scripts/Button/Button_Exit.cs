using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Exit : Button_Base
{
    public override void OnClick()
    {
        Application.Quit();
    }

}
