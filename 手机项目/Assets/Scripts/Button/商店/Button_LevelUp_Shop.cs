using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_LevelUp_Shop : Button_Base
{
    public override void OnClick()
    {
        if(Shop_Manager.Instance != null)
        {
            if (Shop_Manager.Instance.Check_LevelUp_Shop())
            {
                Shop_Manager.Instance.LevelUp_Shop();
                Shop_Manager.Instance.Update_Shop_LevelUp_Price_Information();
            }
        }
    }

}
