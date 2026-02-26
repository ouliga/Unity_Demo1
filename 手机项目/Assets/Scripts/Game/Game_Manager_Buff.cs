using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager_Buff
{
    private bool _has_Config_Buff;
    private int _game_Difficulty_Degree;
    private List<AirShip_Controller_Buff_Base> airShip_Controller_Player_Buffs;
    private List<AirShip_Controller_Buff_Base> airShip_Controller_Enemy_Buffs;

    public Game_Manager_Buff()
    {
        _has_Config_Buff = false;
        _game_Difficulty_Degree = -1;
        airShip_Controller_Player_Buffs = new();
        airShip_Controller_Enemy_Buffs = new();
    }
    public void Buff_AirShip_Controller(GroupType groupType,AirShip_Component[,] airShip_Components,AirShip_Controller_Fly airShip_Controller_Fly)
    {
        List<AirShip_Controller_Buff_Base> airShip_Controller_Buffs = null;
        if (groupType == GroupType.Player) airShip_Controller_Buffs = airShip_Controller_Player_Buffs;
        else if (groupType == GroupType.Enemy) airShip_Controller_Buffs = airShip_Controller_Enemy_Buffs;

        if (airShip_Controller_Player_Buffs == null) return;
        foreach (AirShip_Controller_Buff_Base airShip_Controller_Buff in airShip_Controller_Buffs)
        {
            airShip_Controller_Buff.Buff_AirShip_Components(airShip_Components);
            airShip_Controller_Buff.Buff_AirShip_Controller_Fly(airShip_Controller_Fly);
        }
    }
    public void Config_Buff_By_Game_Difficulty_Degree(int game_Difficulty_Degree)
    {
        if (airShip_Controller_Player_Buffs == null) airShip_Controller_Player_Buffs = new();
        if(airShip_Controller_Enemy_Buffs == null) airShip_Controller_Enemy_Buffs = new();

        Reset_Buff();
        _game_Difficulty_Degree = game_Difficulty_Degree;
        if (_game_Difficulty_Degree == 0)
        {
            AirShip_Controller_Buff_Component_Core_Add_Healthy airShip_Controller_Buff_Core_Add_Healthy_Player = new AirShip_Controller_Buff_Component_Core_Add_Healthy(10);
            airShip_Controller_Player_Buffs.Add(airShip_Controller_Buff_Core_Add_Healthy_Player);

            AirShip_Controller_Buff_Components_Add_Healthy airShip_Controller_Buff_Components_Add_Healthy_Player = new AirShip_Controller_Buff_Components_Add_Healthy(1);
            airShip_Controller_Player_Buffs.Add(airShip_Controller_Buff_Components_Add_Healthy_Player);

            AirShip_Controller_Buff_Component_Core_Add_Healthy airShip_Controller_Buff_Core_Add_Healthy_Enemy = new AirShip_Controller_Buff_Component_Core_Add_Healthy(5);
            airShip_Controller_Enemy_Buffs.Add(airShip_Controller_Buff_Core_Add_Healthy_Enemy);
        }
        else if(_game_Difficulty_Degree == 1)
        {
            AirShip_Controller_Buff_Components_Add_Healthy airShip_Controller_Buff_Components_Add_Healthy_Enemy = new AirShip_Controller_Buff_Components_Add_Healthy(1);
            airShip_Controller_Enemy_Buffs.Add(airShip_Controller_Buff_Components_Add_Healthy_Enemy);
            AirShip_Controller_Buff_Component_Core_Add_Healthy airShip_Controller_Buff_Core_Add_Healthy_Enemy = new AirShip_Controller_Buff_Component_Core_Add_Healthy(5);
            airShip_Controller_Enemy_Buffs.Add(airShip_Controller_Buff_Core_Add_Healthy_Enemy);
        }
        else if (_game_Difficulty_Degree == 2)
        {

        }
        else
        {

        }
        _has_Config_Buff = true;
    }
    public void Reset_Buff()
    {
        _has_Config_Buff = false;
        _game_Difficulty_Degree = -1;
        if (airShip_Controller_Player_Buffs != null)
        {
            airShip_Controller_Player_Buffs.Clear();
        }
        if(airShip_Controller_Enemy_Buffs != null)
        {
            airShip_Controller_Enemy_Buffs.Clear();
        }
    }
    public bool Check_Has_Config_Buff()
    {
        return _has_Config_Buff;
    }

    //UI
    public void Update_Buff_Description()
    {
        if(Game_Information_Manager.Instance != null)
        {
            Game_Information_Manager.Instance.Update_Buff_Description(airShip_Controller_Player_Buffs, airShip_Controller_Enemy_Buffs);
        }
    }
    public void Update_Buff_Description_Title()
    {
        if (Game_Information_Manager.Instance != null)
        {
            Game_Information_Manager.Instance.Update_Buff_Description_Title(_game_Difficulty_Degree);
        }
    }
    //保存或读取数据
    public void Save_Game_Difficulty_Degree_Data()
    {
        if(Data_Manager.Instance != null)
        {
            Data_Manager.Instance.Save_Game_Difficulty_Degree_Data(_game_Difficulty_Degree);
        }
    }
    
}
