using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Panel_Inventory : UI_Panel_Base
{
    [SerializeField] private int _panelLevel_Inventory = 1;
    public override int _panelLevel
    {
        get
        {
            return _panelLevel_Inventory;
        }
    }
    public override void OnEnter()
    {
        Debug.Log("´ò¿ª²Ö¿â");
        if(Inventory_Manager.Instance != null)
        {
            Inventory_Manager.Instance.Update_UI_Inventory();
            Inventory_Manager.Instance.Update_Information_AirShip_Current();
            Inventory_Manager.Instance.Check_EquipBag_Current_Valid();
        }
        if(Inventory_Information_Manager.Instance != null)
        {
            Inventory_Information_Manager.Instance.Reset_Information_Item();
        }
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
    }

    public override void OnExit()
    {
        Debug.Log("¹Ø±Õ²Ö¿â");
        gameObject.SetActive(false);
    }

    public override void OnPause()
    {
        Debug.Log("ÔÝÍ£²Ö¿â½çÃæ");
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public override void OnResume()
    {
        Debug.Log("»Ö¸´²Ö¿â½çÃæ");
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public override void RequestOpen()
    {
        throw new System.NotImplementedException();
    }
}
