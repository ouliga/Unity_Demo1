using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class AirShip_Controller : MonoBehaviour
{
    //委托
    public delegate void Subscribe_AirShip_Controller(AirShip_Controller_Attribute airShip_Controller_Attribute, GroupType groupType);
    //飞船属性更新委托
    public Subscribe_AirShip_Controller _subscribe_AirShip_Controller_Update_Attribute;

    private GroupType _groupType;
    private int _airShip_Direction;

    public AirShip_Controller_Attribute _airShip_Controller_Attribute { get;private set; }

    private int _airShip_MaxNum_X;
    private int _airShip_MaxNum_Y;

    private AirShip_Component[,] _airShip_Components;
    private GameObject[,] _airShip_Component_GameObjects;
    private GameObject _airShip_Core_Component_GameObject;

    private GameObject[,] _airShip_Equipment_GameObjects;
    private GameObject _airShip_Center;
    private Rigidbody2D _airShip_Rigidbody;

    //调用BFS寻路标记
    private BFS_Mark _BFS_Mark;

    //调用飞船飞行功能
    private AirShip_Controller_Fly _airShip_Controller_Fly;

    public void Initialize_AirShip_Controller(int airship_MaxNum_X, int airship_MaxNum_Y, GroupType groupType)
    {
        Debug.Log($"初始化{groupType}飞船控制器");
        _groupType = groupType;
        _airShip_Controller_Attribute = new AirShip_Controller_Attribute();
        _airShip_MaxNum_X = airship_MaxNum_X;
        _airShip_MaxNum_Y = airship_MaxNum_Y;
        _airShip_Components = new AirShip_Component[airship_MaxNum_X, airship_MaxNum_Y];
        _airShip_Component_GameObjects = new GameObject[airship_MaxNum_X, airship_MaxNum_Y];
        _airShip_Equipment_GameObjects = new GameObject[airship_MaxNum_X, airship_MaxNum_Y];

        switch (_groupType)
        {
            case GroupType.Player:
                _airShip_Direction = 1;
                break;
            case GroupType.Enemy:
                _airShip_Direction = -1;
                break;
        }

        _airShip_Controller_Fly = new AirShip_Controller_Fly();
        _BFS_Mark = new BFS_Mark(airship_MaxNum_X, airship_MaxNum_Y);
    }
    //初始化飞船组件的GameObject
    public void Initialize_AirShip_Component_GameObjects(GameObject airship_Component_GameObject,GameObject airship_Center)
    {
        if (airship_Component_GameObject == null || airship_Center == null)
        {
            Debug.Log("飞船组件或中心位置预制体未初始化");
            return;
        }
        Debug.Log("初始化飞船组件");
        _airShip_Center = airship_Center;
        _airShip_Rigidbody = airship_Center.GetComponent<Rigidbody2D>();
        for (int i = 0; i < _airShip_MaxNum_X; i++)
        {
            for (int j = 0; j < _airShip_MaxNum_Y; j++)
            {
                float gameObject_Pos_X = airship_Center.transform.position.x + airship_Component_GameObject.GetComponent<SpriteRenderer>().bounds.size.x * (j - _airShip_MaxNum_X / 2);
                float gameObject_Pos_Y = airship_Center.transform.position.y + airship_Component_GameObject.GetComponent<SpriteRenderer>().bounds.size.y * (_airShip_MaxNum_Y / 2 - i);
                _airShip_Component_GameObjects[i, j] = Instantiate(airship_Component_GameObject, airship_Center.transform);
                _airShip_Component_GameObjects[i, j].transform.position = new Vector2(gameObject_Pos_X, gameObject_Pos_Y);
                _airShip_Component_GameObjects[i, j].name = $"({i},{j})";
            }
        }
        Debug.Log("飞船组件初始化成功");
    }
    //更新飞船组件的GameObject
    public void Update_AirShip_Component_GameObjects(Inventory_Slot[,] inventory_EquipBag_Cube, Inventory_Slot[,] inventory_EquipBag_Equipment)
    {
        Debug.Log("更新飞船部件");
        for (int i = 0; i < _airShip_MaxNum_X; i++)
        {
            for (int j = 0; j < _airShip_MaxNum_Y; j++)
            {
                if (inventory_EquipBag_Cube[i, j]._item == null)
                {
                    _airShip_Component_GameObjects[i, j].SetActive(false);
                    continue;
                }
                if(_airShip_Component_GameObjects[i, j] == null)
                {
                    Debug.Log($"飞船组件GameObject({i},{j})未初始化");
                    continue;
                }
                //更新UI
                GameObject airship_Component_Cube_Display_UI = _airShip_Component_GameObjects[i, j].transform.Find("飞船_方块").gameObject;
                if(airship_Component_Cube_Display_UI != null)
                {
                    airship_Component_Cube_Display_UI.SetActive(true);
                    airship_Component_Cube_Display_UI.GetComponent<SpriteRenderer>().sprite = inventory_EquipBag_Cube[i, j]._item._display_UI;
                    airship_Component_Cube_Display_UI.GetComponent<SpriteRenderer>().sortingOrder = 0;
                }
                else
                {
                    Debug.Log("未找到飞船方块对应的UI");
                }
                //添加装备
                if (inventory_EquipBag_Equipment[i, j]._item != null)
                {
                    GameObject airship_Equipment = Get_AirShip_Equipment_Prefab(inventory_EquipBag_Equipment[i, j]._item._id);
                    if (airship_Equipment != null)
                    {
                        _airShip_Equipment_GameObjects[i,j] = Instantiate(airship_Equipment, _airShip_Component_GameObjects[i, j].transform);
                        _airShip_Equipment_GameObjects[i, j].name = "飞船_装备";
                        _airShip_Equipment_GameObjects[i, j].GetComponent<SpriteRenderer>().sprite = inventory_EquipBag_Equipment[i, j]._item._display_UI;
                        _airShip_Equipment_GameObjects[i, j].GetComponent<SpriteRenderer>().sortingOrder = 1;
                    }
                    else
                    {
                        Debug.Log("未找到相应组件的预制体");
                    }
                }
            }
        }
        Debug.Log("更新飞船部件成功");
    }
    //初始化飞船组件脚本
    public void Initialize_AirShip_Component_Scripts(Inventory_Slot[,] inventory_EquipBag_Cube, Inventory_Slot[,] inventory_EquipBag_Equipment)
    {
        Debug.Log("初始化飞船组件脚本");
        for (int i = 0; i < _airShip_MaxNum_X; i++)
        {
            for (int j = 0; j < _airShip_MaxNum_Y; j++)
            {
                //初始化飞船组件
                if (inventory_EquipBag_Cube[i, j]._item == null) continue;
                _airShip_Components[i, j] = _airShip_Component_GameObjects[i, j].AddComponent<AirShip_Component>();
                _airShip_Components[i, j].Initialize_AirShip_Component(inventory_EquipBag_Cube[i, j]._item, inventory_EquipBag_Equipment[i, j]._item, _groupType);
                //标记核心组件    
                if (_airShip_Components[i,j]._isCore && _airShip_Core_Component_GameObject == null)
                {
                    _airShip_Core_Component_GameObject = _airShip_Component_GameObjects[i, j];
                }
                //初始化飞船装备
                if(_airShip_Equipment_GameObjects[i, j] != null)
                {
                    AirShip_Equipment airShip_Equipment = _airShip_Equipment_GameObjects[i, j].GetComponent<AirShip_Equipment>();
                    if (airShip_Equipment == null)
                    {
                        Debug.Log("该预制体未配置飞船装备脚本");
                    }
                    _airShip_Components[i, j].Set_AirShip_Equipment(airShip_Equipment,inventory_EquipBag_Equipment[i, j]._item, _groupType, _airShip_Direction);
                }
                //初始化飞船组件的事件
                _airShip_Components[i, j].Initialize_Action_Update_AirShip_Attributes(Update_AirShip_Attributes);
                _airShip_Components[i, j].Initialize_Action_Check_AirShip_Components_Valid(Check_AirShip_Components_Valid);
            }
        }
        Debug.Log("初始化飞船组件脚本成功");
    }
    //更新飞船的属性
    public void Update_AirShip_Attributes()
    {
        if(_airShip_Controller_Attribute == null)
        {
            Debug.Log("飞船属性未初始化");
            return;
        }
        _airShip_Controller_Attribute.Update_AirShip_Attributes(_airShip_Components);
        if(_subscribe_AirShip_Controller_Update_Attribute != null)
        {
            _subscribe_AirShip_Controller_Update_Attribute.Invoke(_airShip_Controller_Attribute, _groupType);
        }
    }

    /// BFS_Mark方法

    //检测飞船组件是否合理
    public void Check_AirShip_Components_Valid()
    {
        if (_BFS_Mark == null) return;
        if(_airShip_Components == null) return;

        _BFS_Mark.Update_BFS_AirShip_Controller(_airShip_Components);
        _BFS_Mark.Mark_FromCore();
        _BFS_Mark.Destory_IllegalComponent_AirShip_Controller(_airShip_Components);

        if (!_BFS_Mark.Check_Core_AirShip_Controller(_airShip_Components))
        {
            Debug.Log($"({_groupType})飞船战败");
            if(Battle_Manager.Instance != null)
            {
                Battle_Manager.Instance.Battle_Over_Check(_groupType);
            }
        }
    }

    /// 飞船运动状态

    public void Initialize_AirShip_RigidBody_GamePrepare()
    {
        Debug.Log($"初始化飞船({_groupType})运动状态(游戏准备阶段)");
        if (_airShip_Center == null) return;
        //朝向
        _airShip_Center.transform.eulerAngles = new Vector3(0, 180 * (-(float)_airShip_Direction/2 + 0.5f), 0);

        Reset_AirShip_RigidBody();
        Debug.Log($"初始化飞船({_groupType})运动状态成功(游戏准备阶段)");
    }

    public void Reset_AirShip_RigidBody()
    {
        if (_airShip_Rigidbody == null) return;
        //重力
        float gravity_Scale = 0;
        _airShip_Rigidbody.gravityScale = gravity_Scale;
        //初速度
        Vector2 vector = new Vector2(0, 0);
        _airShip_Rigidbody.velocity = vector;
    }

    //飞船操控
    public void AirShip_Fly_UP()
    {
        if (_airShip_Controller_Fly == null) return;
        _airShip_Controller_Fly.AirShip_Fly_UP(_airShip_Rigidbody,_airShip_Controller_Attribute);
        Debug.Log($"飞船({_groupType})上升，速度({_airShip_Rigidbody.velocity})");
    }
    public void AirShip_Fly_Down()
    {
        if (_airShip_Controller_Fly == null) return;
        _airShip_Controller_Fly.AirShip_Fly_Down(_airShip_Rigidbody);
        Debug.Log($"飞船({_groupType})下降，速度({_airShip_Rigidbody.velocity})");
    }

    public void AirShip_Fall_Down()
    {
        if (_airShip_Controller_Fly == null) return;
        _airShip_Controller_Fly.AirShip_Fall_Down(_airShip_Rigidbody);
    }
    //获取飞船装备预制体
    private GameObject Get_AirShip_Equipment_Prefab(int id)
    {
        string path_AirShip_Equipment_Prefab_Default = "Prefab/AirShip/Equipment/飞船_装备";
        GameObject airShip_Equipment = Resources.Load<GameObject>($"{path_AirShip_Equipment_Prefab_Default}_{id}");
        if (airShip_Equipment == null)
        {
            Debug.Log($"未找到(id:{id})对应组件的预制体");
            return Resources.Load<GameObject>(path_AirShip_Equipment_Prefab_Default);
        }
        else
        {
            return airShip_Equipment;
        }
    }

    //获取飞船的各个参数
    public Vector3 Get_AirShip_Velocity()
    {
        if (_airShip_Rigidbody == null)
        {
            Debug.Log($"飞船({_groupType})的rigidbody未初始化");
            return Vector3.zero;
        }
        return _airShip_Rigidbody.velocity;
    }

    public Vector3 Get_AirShip_CenterPosition()
    {
        if(_airShip_Core_Component_GameObject == null)
        {
            Debug.Log($"飞船({_groupType})并不存在核心组件");
            return Vector3.zero;
        }
        return _airShip_Core_Component_GameObject.transform.position;
    }
    //修改飞船的参数
    public void Change_AirShip_Speed_Y(float change_Value)
    {
        if (_airShip_Controller_Fly != null)
        {
            _airShip_Controller_Fly.Change_AirShip_Speed_Y(_airShip_Rigidbody, change_Value);
        }
    }

    //获得全局Buff
    public void Get_Global_Buff()
    {
        if(Game_Manager.Instance != null)
        {
            Game_Manager.Instance.Buff_AirShip_Controller(_groupType, _airShip_Components, _airShip_Controller_Fly);
        }
    }


}
