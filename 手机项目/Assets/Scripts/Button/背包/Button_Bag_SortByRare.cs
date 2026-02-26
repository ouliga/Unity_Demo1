using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Bag_SortByRare : Button_Base
{
    [SerializeField] private int rare = -1;

    public override void OnClick()
    {
        if (Inventory_Manager.Instance != null)
        {
            Inventory_Manager.Instance.Sort_Bag_By_Rare(rare);

        }
        if (Inventory_Information_Manager.Instance != null)
        {
            Inventory_Information_Manager.Instance.Reset_Information_Item();
        }
        if (Shop_Manager.Instance != null)
        {
            Shop_Manager.Instance.Reset_Select_Item();
            Shop_Manager.Instance.Reset_Item_Information();
        }
    }
}
