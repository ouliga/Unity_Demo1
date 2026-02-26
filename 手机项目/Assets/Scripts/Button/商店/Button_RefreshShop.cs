using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_RefreshShop : Button_Base
{
    public override void OnClick()
    {
        if(Shop_Manager.Instance != null)
        {
            if (Shop_Manager.Instance.Check_Refresh_Shop())
            {
                Shop_Manager.Instance.Refresh_Shop();
                Shop_Manager.Instance.Refresh_Shop_Cost();
                Shop_Manager.Instance.Update_Shop_UI_Slots();
                Shop_Manager.Instance.Update_Shop_Refresh_Price_Information();

                if(Shop_Information_Manager.Instance != null)
                {
                    Shop_Information_Manager.Instance.Reset_Shop_Item_Price_Information();
                }
            }
        }

    }
}
