using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Button_EquipBag_Switch : Button_Base
{
    [SerializeField] private GroupType groupType;

    public override void OnClick()
    {
        if (Inventory_Manager.Instance != null)
        {
            Inventory_Manager.Instance.Switch_EquipBag_Test(groupType);
        }

    }

}
