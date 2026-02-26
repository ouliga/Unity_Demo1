using UnityEngine;

public class Game_Manager : Singleton<Game_Manager>
{
    [SerializeField]private Data_SO _data_Item;

    private Game_Manager_Buff _game_Manager_Buff;

    private Game_Information_Manager _game_Information_Manager;
    private void Start()
    {
        Set_Game_Manager(Scene_Type.StartScene);
        Set_Game_Information_Manager(Scene_Type.StartScene);
    }
    private void OnEnable()
    {
        if(Scene_Manager.Instance != null)
        {
            Scene_Manager.Instance._subscribe += Set_Game_Manager;
            Scene_Manager.Instance._subscribe += Set_Game_Information_Manager;
        }
    }
    private void OnDisable()
    {
        if (Scene_Manager.Instance != null)
        {
            Scene_Manager.Instance._subscribe -= Set_Game_Manager;
            Scene_Manager.Instance._subscribe -= Set_Game_Information_Manager;
        }
    }
    public void Set_Game_Manager(Scene_Type scene_Type)
    {
        if(scene_Type == Scene_Type.StartScene)
        {
            if (_game_Manager_Buff == null)
            {
                _game_Manager_Buff = new Game_Manager_Buff();
            }
        }
    }
    //获得物品信息
    public Data_SO Get_Data_Item()
    {
        if(_data_Item == null)
        {
            Debug.LogError("物品数据未初始化");
        }
        return _data_Item;
    }

    //全局Buff
    public void Config_Buff_By_Game_Difficulty_Degree(int game_Difficulty_Degree)
    {
        if (_game_Manager_Buff != null)
        {
            _game_Manager_Buff.Config_Buff_By_Game_Difficulty_Degree(game_Difficulty_Degree);
            Debug.Log($"游戏难度为：{game_Difficulty_Degree}级");
        }
    }
    public void Reset_Buff()
    {
        if (_game_Manager_Buff != null)
        {
            _game_Manager_Buff.Reset_Buff();
        }
    }
    public void Buff_AirShip_Controller(GroupType groupType, AirShip_Component[,] airShip_Components, AirShip_Controller_Fly airShip_Controller_Fly)
    {
        if(_game_Manager_Buff != null)
        {
            _game_Manager_Buff.Buff_AirShip_Controller(groupType, airShip_Components, airShip_Controller_Fly);
        }
    }
    //Buff信息
    public void Update_Buff_Description()
    {
        if (_game_Manager_Buff != null)
        {
            _game_Manager_Buff.Update_Buff_Description();
        }
    }
    public void Update_Buff_Description_Title()
    {
        if (_game_Manager_Buff != null)
        {
            _game_Manager_Buff.Update_Buff_Description_Title();
        }
    }
    //读取或存储游戏数据
    public void Save_Game_Difficulty_Degree_Data()
    {
        if (_game_Manager_Buff != null)
        {
            _game_Manager_Buff.Save_Game_Difficulty_Degree_Data();
        }
    }
    public void Load_Game_Difficulty_Degree_Data()
    {
        if(Data_Manager.Instance != null)
        {
            Data_Manager.Instance.Load_Game_Difficulty_Degree_Data(_game_Manager_Buff);
        }
    }

    //信息管理器
    private void Set_Game_Information_Manager(Scene_Type scene_Type)
    {
        if(scene_Type == Scene_Type.StartScene)
        {
            if (_game_Information_Manager == null)
            {
                _game_Information_Manager = Game_Information_Manager.Instance;
            }
            _game_Information_Manager.Set_Game_Information_Manager();
        }
    }

    //游戏进程
    public bool Check_Game_Start()
    {
        if(_game_Manager_Buff == null)
        {
            return false;
        }
        else
        {
            if (!_game_Manager_Buff.Check_Has_Config_Buff())
            {
                return false;
            }
        }
        return true;
    }
}
