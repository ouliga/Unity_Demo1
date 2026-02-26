using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;
public enum StateType
{
    gamePrepare,gameStart,gameEnd,gamePaused
}
public enum ButtonType
{
    AirShip_Fly, AirShip_Shoot,
    GameStart
}
public class FSM_BattleManager : MonoBehaviour
{
    private IState _istate_Current;
    protected Dictionary<StateType, IState> _states = new Dictionary<StateType, IState>();

    private GameObject _airShip_Center_Player;
    private GameObject _airShip_Center_Enemy;
    private GameObject _airShip_Component_GameObject;
    private AirShip_Controller _airShip_Controller_Player;
    private AirShip_Controller _airShip_Controller_Enemy;

    //控制飞船的行为树AI管理器
    private BehaviorManager _behaviorManager;

    public void Set_FSM_BattleManager(GameObject airShip_Center_Player,GameObject airShip_Center_Enemy,GameObject airShip_Component_GameObject)
    {
        _airShip_Center_Player = airShip_Center_Player;
        _airShip_Center_Enemy = airShip_Center_Enemy;
        _airShip_Component_GameObject = airShip_Component_GameObject;
    }

    //添加状态
    public virtual void AddState(StateType state, IState istate)
    {
        if (_states.ContainsKey(state))
        {
            Debug.Log("请勿重复添加状态");
            return;
        }
        _states.Add(state, istate);
    }
    //转换状态
    public virtual void Enter_State(StateType state)
    {
        if (!_states.ContainsKey(state))
        {
            Debug.Log("没有找到此状态");
            return;
        }
        if (_istate_Current != null)
        {
            _istate_Current.Exit();
        }
        _istate_Current = _states[state];
        _istate_Current.Enter();
    }
    //更新
    public void Update_FSM()
    {
        if (_istate_Current == null) return;
        _istate_Current.Update();
    }
    //按键事件
    public void Button_Event(ButtonType buttonType)
    {
        if (_istate_Current == null) return;
        _istate_Current.Button_Event(buttonType);
    }

    public void Initialize_AirShip_Controller()
    {
        if (Inventory_Manager.Instance != null)
        {
            _airShip_Controller_Player = _airShip_Center_Player.AddComponent<AirShip_Controller>();
            _airShip_Controller_Player.Initialize_AirShip_Controller(Inventory_Manager.Instance._inventory_EquipBag_MaxNum_X, Inventory_Manager.Instance._inventory_EquipBag_MaxNum_Y, GroupType.Player);
            _airShip_Controller_Enemy = _airShip_Center_Enemy.AddComponent<AirShip_Controller>();
            _airShip_Controller_Enemy.Initialize_AirShip_Controller(Inventory_Manager.Instance._inventory_EquipBag_MaxNum_X, Inventory_Manager.Instance._inventory_EquipBag_MaxNum_Y, GroupType.Enemy);
        }
        else
        {
            Debug.LogError("仓库管理器未初始化");
        }
    }
    public void Initialize_AirShip_Component_GameObjects()
    {
        Debug.Log($"初始化飞船GameObject");
        if (Inventory_Manager.Instance != null)
        {
            //生成玩家飞船组件
            _airShip_Controller_Player.Initialize_AirShip_Component_GameObjects(_airShip_Component_GameObject, _airShip_Center_Player);
            Inventory_Manager.Instance.Update_Airship_Component_GameObjects(_airShip_Controller_Player, GroupType.Player);
            //生成敌方飞船组件
            _airShip_Controller_Enemy.Initialize_AirShip_Component_GameObjects(_airShip_Component_GameObject, _airShip_Center_Enemy);
            Inventory_Manager.Instance.Update_Airship_Component_GameObjects(_airShip_Controller_Enemy, GroupType.Enemy);
        }
        else
        {
            Debug.LogError("仓库管理器未初始化");
            return;
        }
    }
    public void Initialize_AirShip_RigidBody_GamePrepare()
    {
        Debug.Log("初始化飞船运动状态");
        _airShip_Controller_Player.Initialize_AirShip_RigidBody_GamePrepare();
        _airShip_Controller_Enemy.Initialize_AirShip_RigidBody_GamePrepare();
        Debug.Log("初始化飞船运动状态成功");
    }
    public void Reset_AirShip_RigidBody()
    {
        Debug.Log("重置飞船运动状态");
        _airShip_Controller_Player.Reset_AirShip_RigidBody();
        _airShip_Controller_Enemy.Reset_AirShip_RigidBody();
        Debug.Log("重置飞船运动状态成功");
    }

    private void Initialize_BehaviorManager()
    {
        Debug.Log("初始化行为树AI管理器");
        _behaviorManager = new BehaviorManager(_airShip_Controller_Player, _airShip_Controller_Enemy);
        _behaviorManager.Bulid_BehaviorTree_AirShip_Enemy_Fly(AirShip_Enemy_Fly_Up, AirShip_Enemy_Fly_Down);
        Debug.Log("初始化行为树AI管理器成功");
    }

    //战役进程
    public void Battle_Prepare()
    {
        //_gameOver_Check_Lock = false;
        Initialize_AirShip_Controller();
        Initialize_AirShip_Component_GameObjects();
        Initialize_AirShip_RigidBody_GamePrepare();

        Initialize_BehaviorManager();
    }
    public void Battle_Start()
    {
        Debug.Log("游戏开始阶段初始化");
        if (Inventory_Manager.Instance == null) return;
        if (Battle_Information_Manager.Instance == null) return;

        //订阅飞船属性更新的委托
        _airShip_Controller_Player._subscribe_AirShip_Controller_Update_Attribute += Battle_Information_Manager.Instance.Show_AirShip_Information;
        _airShip_Controller_Enemy._subscribe_AirShip_Controller_Update_Attribute += Battle_Information_Manager.Instance.Show_AirShip_Information;

        //初始化飞船组件脚本
        Inventory_Manager.Instance.Initialize_AirShip_Component_Scripts(_airShip_Controller_Player, GroupType.Player);
        Inventory_Manager.Instance.Initialize_AirShip_Component_Scripts(_airShip_Controller_Enemy, GroupType.Enemy);

        //飞船调用全局Buff
        _airShip_Controller_Player.Get_Global_Buff();
        _airShip_Controller_Enemy.Get_Global_Buff();

        //初始化飞船属性
        _airShip_Controller_Player.Update_AirShip_Attributes();
        _airShip_Controller_Enemy.Update_AirShip_Attributes();

        Debug.Log("游戏开始阶段初始化成功");
    }
    public void Battle_Start_Update()
    {
        //时间管理器的更新
        if (Time_Manager.Instance != null)
        {
            Time_Manager.Instance.Update_Time_Manager();
        }
        //飞船飞行的下降控制
        AirShip_Player_Fall_Down();
        AirShip_Enemy_Fall_Down();
    }
    public void Battle_End()
    {
        _airShip_Controller_Player._subscribe_AirShip_Controller_Update_Attribute -= Battle_Information_Manager.Instance.Show_AirShip_Information;
        _airShip_Controller_Enemy._subscribe_AirShip_Controller_Update_Attribute -= Battle_Information_Manager.Instance.Show_AirShip_Information;
        Reset_AirShip_RigidBody();

        if(UI_Panel_Manager.Instance != null)
        {
            UI_Panel_Manager.Instance.Open_Battle_Over_Panel();
        }
    }

    //控制飞船
    public void AirShip_Player_Fly_Up()
    {
        if (_airShip_Controller_Player != null)
        {
            _airShip_Controller_Player.AirShip_Fly_UP();
        }
    }
    public void AirShip_Enemy_Fly_Up()
    {
        if (_airShip_Controller_Enemy != null)
        {
            _airShip_Controller_Enemy.AirShip_Fly_UP();
        }
    }
    public void AirShip_Enemy_Fly_Down()
    {
        if (_airShip_Controller_Enemy != null)
        {
            _airShip_Controller_Enemy.AirShip_Fly_Down();
        }
    }
    private void AirShip_Player_Fall_Down()
    {
        if (_airShip_Controller_Player != null)
        {
            _airShip_Controller_Player.AirShip_Fall_Down();
        }
    }
    private void AirShip_Enemy_Fall_Down()
    {
        if (_airShip_Controller_Enemy != null)
        {
            _airShip_Controller_Enemy.AirShip_Fall_Down();
        }
    }

}
