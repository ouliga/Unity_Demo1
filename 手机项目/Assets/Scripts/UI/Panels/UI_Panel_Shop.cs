using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Panel_Shop : UI_Panel_Base
{
    [SerializeField] private int _panelLevel_Shop = 1;
    public override int _panelLevel
    {
        get
        {
            return _panelLevel_Shop;
        }
    }
    public override void OnEnter()
    {
        Debug.Log("打开商店");
        if (Inventory_Manager.Instance != null)
        {
            Inventory_Manager.Instance.Update_UI_Inventory();
        }
            if (Shop_Manager.Instance != null)
        {
            Shop_Manager.Instance.Reset_Select_Item();
            Shop_Manager.Instance.Update_Shop_UI_Slots();
            Shop_Manager.Instance.Update_Shop_Refresh_Price_Information();
            Shop_Manager.Instance.Update_Shop_LevelUp_Price_Information();
            Shop_Manager.Instance.Reset_Item_Information();
            Shop_Manager.Instance.Reset_Shop_Item_Price_Information();
        }
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
    }

    public override void OnExit()
    {
        Debug.Log("关闭商店");
        gameObject.SetActive(false);
    }

    public override void OnPause()
    {
        Debug.Log("暂停商店界面");
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public override void OnResume()
    {
        Debug.Log("恢复商店界面");
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public override void RequestOpen()
    {
    }
}
