using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_NextScene_Battle : Button_Base
{
    [SerializeField] private Scene_Type _sceneType;

    public override void OnClick()
    {
        if(Inventory_Manager.Instance == null)
        {
            Debug.Log("仓库管理器未初始化");
            return;
        }
        if (Inventory_Manager.Instance.Check_EquipBag_Both_Valid())
        {
            Scene_Manager.Instance.Enter_Scene(_sceneType);
        }
        else
        {
            Debug.Log("玩家或敌人的装备配置不合规");
        }
    }
}
