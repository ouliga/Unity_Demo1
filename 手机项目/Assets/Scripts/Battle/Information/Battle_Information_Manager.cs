using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Battle_Information_Manager : Singleton<Battle_Information_Manager>
{
    [SerializeField] private TextMeshProUGUI _text_Player_Healthy;
    [SerializeField] private TextMeshProUGUI _text_Enemy_Healthy;
    private void Awake() { }
    public void Show_AirShip_Information(AirShip_Controller_Attribute airShip_Controller_Attribute, GroupType groupType)
    {
        Show_AirShip_Information_Healthy(airShip_Controller_Attribute, groupType);
    }
    private void Show_AirShip_Information_Healthy(AirShip_Controller_Attribute airShip_Controller_Attribute,GroupType groupType)
    {
        if (_text_Player_Healthy == null) return;
        if(groupType == GroupType.Player)
        {
            _text_Player_Healthy.text = airShip_Controller_Attribute._airShip_Healthy + "";
        }

        if(_text_Enemy_Healthy == null) return;
        if(groupType == GroupType.Enemy)
        {
            _text_Enemy_Healthy.text = airShip_Controller_Attribute._airShip_Healthy + "";
        }
        Debug.Log($"成功更新({groupType})飞船血量信息");

    }


}
