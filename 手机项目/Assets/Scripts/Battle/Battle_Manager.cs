using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
public enum GroupType
{
    None, Player, Enemy
}

public class Battle_Manager : Singleton<Battle_Manager>
{
    [SerializeField] private GameObject _airShip_Center_Player;
    [SerializeField] private GameObject _airShip_Center_Enemy;
    [SerializeField] private GameObject _airShip_Component_GameObject;
    private AirShip_Controller _airShip_Controller_Player;
    private AirShip_Controller _airShip_Controller_Enemy;
    //控制战役的有限状态机
    private FSM_BattleManager _fsm_BattleManager;
    //控制飞船的行为树AI管理器
    //private BehaviorManager _behaviorManager;
    //游戏结束检测锁
    private bool _gameOver_Check_Lock;
    private void Awake() { }
    private void OnEnable()
    {
        if(Scene_Manager.Instance != null)
        {
            Scene_Manager.Instance._subscribe += Set_Battle_Manager;
        }
    }
    private void OnDisable()
    {
        if (Scene_Manager.Instance != null)
        {
            Scene_Manager.Instance._subscribe -= Set_Battle_Manager;
        }
    }
    private void FixedUpdate()
    {
        if (_fsm_BattleManager == null) return;
        _fsm_BattleManager.Update_FSM();
    }

    private void Set_Battle_Manager(Scene_Type scene)
    {
        if (scene == Scene_Type.BattleScene)
        {
            //_fsm_BattleManager = new FSM_BattleManager();
            _gameOver_Check_Lock = false;
            _fsm_BattleManager = gameObject.AddComponent<FSM_BattleManager>();
            _fsm_BattleManager.Set_FSM_BattleManager(_airShip_Center_Player, _airShip_Center_Enemy, _airShip_Component_GameObject);
            _fsm_BattleManager.AddState(StateType.gamePrepare, new State_GamePrepare(_fsm_BattleManager));
            _fsm_BattleManager.AddState(StateType.gameStart,new State_GameStart(_fsm_BattleManager));
            _fsm_BattleManager.AddState(StateType.gameEnd,new State_GameEnd(_fsm_BattleManager));
            _fsm_BattleManager.AddState(StateType.gamePaused,new State_GamePause(_fsm_BattleManager));
            FSM_Enter_State(StateType.gamePrepare);
        }
    }
    //战役结束判定
    public void Battle_Over_Check(GroupType groupType)
    {
        Debug.Log("尝试游戏结束检测1V1");
        if (_gameOver_Check_Lock) return;
        _gameOver_Check_Lock = true;

        if(groupType == GroupType.Player)
        {
            Debug.Log("敌方胜利");
        }
        else if(groupType == GroupType.Enemy)
        {
            Battle_Win();
            Debug.Log("玩家胜利");
        }
        FSM_Enter_State(StateType.gameEnd);
    }
    private void Battle_Win()
    {
        if(Map_Manager.Instance == null) return;
        if(Shop_Manager.Instance == null) return;
        Map_Node map_Node_Active_Current = Map_Manager.Instance.Get_Map_Node_Active_Current();
        Shop_Manager.Instance.Get_Shop_Reward(map_Node_Active_Current);
    }
    //FSM
    public void FSM_Button_Event(ButtonType buttonType)
    {
        if (_fsm_BattleManager == null) return;
        _fsm_BattleManager.Button_Event(buttonType);
    }
    public void FSM_Enter_State(StateType stateType)
    {
        Debug.Log($"FSM尝试进入({stateType.ToString()})状态");
        if (_fsm_BattleManager == null) return;
        _fsm_BattleManager.Enter_State(stateType);
    }
}
