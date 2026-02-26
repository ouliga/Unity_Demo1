using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Game_Information_Manager : Singleton<Game_Information_Manager>
{
    [SerializeField] private GameObject _buff_Description_Position;
    [SerializeField] private GameObject _buff_Description_Prefab_Player;
    [SerializeField] private GameObject _buff_Description_Prefab_Enemy;
    [SerializeField] private TextMeshProUGUI _buff_Description_Title_Text;
    private void Awake() { }

    private Game_Information_Buff_Description _game_Information_Buff_Description_Player;
    private Game_Information_Buff_Description _game_Information_Buff_Description_Enemy;

    public void Set_Game_Information_Manager()
    {
        _game_Information_Buff_Description_Player = gameObject.AddComponent<Game_Information_Buff_Description>();
        _game_Information_Buff_Description_Player.Set_Game_Information_Buff_Description(_buff_Description_Position, _buff_Description_Prefab_Player);
        _game_Information_Buff_Description_Enemy = gameObject.AddComponent<Game_Information_Buff_Description>();
        _game_Information_Buff_Description_Enemy.Set_Game_Information_Buff_Description(_buff_Description_Position, _buff_Description_Prefab_Enemy);
    }

    public void Update_Buff_Description(List<AirShip_Controller_Buff_Base> airShip_Controller_Buffs_Player, List<AirShip_Controller_Buff_Base> airShip_Controller_Buffs_Enemy)
    {
        if (airShip_Controller_Buffs_Player != null)
        {
            _game_Information_Buff_Description_Player.Update_Buff_Description(GroupType.Player,airShip_Controller_Buffs_Player);
        }
        if(airShip_Controller_Buffs_Enemy != null)
        {
            _game_Information_Buff_Description_Enemy.Update_Buff_Description(GroupType.Enemy,airShip_Controller_Buffs_Enemy);
        }
    }
    public void Update_Buff_Description_Title(int game_Difficulty_Degree)
    {
        if (_buff_Description_Title_Text == null) return;
        if(game_Difficulty_Degree == -1)
        {
            _buff_Description_Title_Text.text = "难度选择";
        }
        else
        {
            _buff_Description_Title_Text.text = $"难度选择({game_Difficulty_Degree}级)";
        }
    }



}
