using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Map_Manager : Singleton<Map_Manager>
{
    private int _layer_MaxNum;
    private int _layer_Node_MaxNum;
    private Map_Node[,] _map_Nodes;

    private Map_UI_Manager _map_UI_Manager;

    private Map_Node_Line _map_Node_Line;
    private Map_Node_Controller _map_Node_Controller;

    public void OnEnable()
    {
        if(Scene_Manager.Instance != null)
        {
            Scene_Manager.Instance._subscribe += Set_Map_Manager;
            Scene_Manager.Instance._subscribe += Set_Map_UI_Manager;
            Scene_Manager.Instance._subscribe += Initialize_UI_Map_Nodes;
            Scene_Manager.Instance._subscribe += Initialize_UI_Map_Line;
            Scene_Manager.Instance._subscribe += Initialize_UI_Map_Node_Controller;
        }
    }
    public void OnDisable()
    {
        if (Scene_Manager.Instance != null)
        {
            Scene_Manager.Instance._subscribe -= Set_Map_Manager;
            Scene_Manager.Instance._subscribe -= Set_Map_UI_Manager;
            Scene_Manager.Instance._subscribe -= Initialize_UI_Map_Nodes;
            Scene_Manager.Instance._subscribe -= Initialize_UI_Map_Line;
            Scene_Manager.Instance._subscribe -= Initialize_UI_Map_Node_Controller;
        }
    }
    private void Set_Map_Manager(Scene_Type scene_Type)
    {
        if(scene_Type == Scene_Type.WorldScene)
        {
            if (!_has_Initialize)
            {
                Set_Map_Manager();
                Initialize_Map_Nodes();
                Load_Map_Data();
                _has_Initialize = true;
            }
            else
            {
                Debug.Log("地图管理器已经初始化");
            }
        }
        else if(scene_Type == Scene_Type.StartScene)
        {
            Destroy(gameObject);
        }
    }
    private void Set_Map_Manager()
    {
        _layer_MaxNum = 9;
        _layer_Node_MaxNum = 5;
        _map_Nodes = new Map_Node[_layer_MaxNum,_layer_Node_MaxNum];
        //Set_Map_UI_Manager();

        _map_Node_Line = gameObject.AddComponent<Map_Node_Line>();
        _map_Node_Controller = gameObject.AddComponent<Map_Node_Controller>();
    }
    private void Set_Map_UI_Manager(Scene_Type scene_Type)
    {
        if(scene_Type == Scene_Type.WorldScene)
        {
            if (_map_UI_Manager == null)
            {
                _map_UI_Manager = Map_UI_Manager.Instance;
            }
            _map_UI_Manager.Set_Map_UI_Manager(_layer_MaxNum, _layer_Node_MaxNum);
        }
    }
    private void Initialize_Map_Nodes()
    {
        for (int i = 0; i < _layer_MaxNum; i++)
        {
            for (int j = 0; j < _layer_Node_MaxNum; j++)
            {
                _map_Nodes[i, j] = new Map_Node(i, j, RoomType.None);
            }
        }
    }
    //配置地图节点
    private void Config_Map_Nodes_Default()
    {
        if (_map_Nodes == null) return;
        //第1层中间节点为起始节点
        _map_Nodes[0, _layer_Node_MaxNum/2].Change_RoomType(RoomType.Start);
        _map_Nodes[0, _layer_Node_MaxNum / 2].Change_Active_Controller(true);
        //第2层为3个普通节点
        Config_Layer_Room_Nodes(1, 3, 0, 0);
        //第3层为3个商店节点
        Config_Layer_Room_Nodes(2, 0, 0, 3);
        //第4层为2个普通，2个困难
        Config_Layer_Room_Nodes(3, 2, 2, 0);
        //第5层为3个普通，1个困难
        Config_Layer_Room_Nodes(4, 3, 1, 0);
        //第6层为3个商店
        Config_Layer_Room_Nodes(5, 0, 0, 3);
        //第7层为2个普通，2个困难
        Config_Layer_Room_Nodes(6, 2, 2, 0);
        //第8层为1个普通，1个困难，2个商店
        Config_Layer_Room_Nodes(7, 1, 1, 2);
        //第9层为Boss
        _map_Nodes[8, _layer_Node_MaxNum / 2].Change_RoomType(RoomType.Boss);

    }
    
    //配置层级房间节点
    private void Config_Layer_Room_Nodes(int layer,int normal_Node_Num,int hard_Node_Num,int shop_Node_Num)
    {
        int node_Num = normal_Node_Num + hard_Node_Num + shop_Node_Num ;
        if (node_Num > _layer_Node_MaxNum)
        {
            Debug.Log("配置节点数量超过了当前层数节点数");
            return;
        }
        //获得当层所有节点
        List<Map_Node> all_Room_Nodes = new();
        for (int i = 0; i < _layer_Node_MaxNum; i++)
        {
            all_Room_Nodes.Add(_map_Nodes[layer, i]);
        }
        //随机获取n次节点
        List<Map_Node> choose_Room_Nodes = new();
        for (int i = 0; i < node_Num; i++)
        {
            int size = all_Room_Nodes.Count;
            int romdom_index = Random.Range(0, size);
            choose_Room_Nodes.Add(all_Room_Nodes[romdom_index]);
            all_Room_Nodes.RemoveAt(romdom_index);
        }

        //配置节点
        for(int i = 0;i < normal_Node_Num; i++)
        {
            Config_Room_Nodes_Random(choose_Room_Nodes, RoomType.Normal);
        }
        for(int i = 0; i < hard_Node_Num; i++)
        {
            Config_Room_Nodes_Random(choose_Room_Nodes, RoomType.Hard);
        }
        for(int i = 0;i< shop_Node_Num; i++)
        {
            Config_Room_Nodes_Random(choose_Room_Nodes, RoomType.Shop);
        }

    }
    private void Config_Room_Nodes_Random(List<Map_Node> room_Nodes,RoomType roomType)
    {
        int size = room_Nodes.Count;
        int random = Random.Range(0, size);
        room_Nodes[random].Change_RoomType(roomType);
        room_Nodes.RemoveAt(random);
    }

    private void Initialize_UI_Map_Nodes(Scene_Type scene_Type)
    {
        if(scene_Type == Scene_Type.WorldScene)
        {
            if (_map_UI_Manager != null)
            {
                _map_UI_Manager.Initialize_UI_Map_Nodes(_map_Nodes);
            }
        }
    }
    //配置节点间的连线
    private void Config_Map_Nodes_Line_Default()
    {
        if (_map_Node_Line != null)
        {
            _map_Node_Line.Config_Map_Nodes_Line(_map_Nodes);
        }
    }
    private void Initialize_UI_Map_Line(Scene_Type scene_Type)
    {
        if(scene_Type == Scene_Type.WorldScene)
        {
            if (_map_Node_Line != null)
            {
                _map_Node_Line.Initialize_UI_Map_Line(_map_Nodes);
            }
        }
    }
    //节点控制器
    private void Initialize_UI_Map_Node_Controller(Scene_Type scene_Type)
    {
        if(scene_Type == Scene_Type.WorldScene)
        {
            if (_map_Node_Controller != null)
            {
                _map_Node_Controller.Initialize_Map_Node_Controller(_map_Nodes);
            }
        }

    }
    public void Trigger_Map_Node_Controller(Map_Node map_Node_Current)
    {
        if( _map_Node_Controller != null)
        {
            _map_Node_Controller.Trigger_Map_Node_Controller(map_Node_Current);
        }
    }
    public bool Check_Trigger_Event(Map_Node map_Node_Current)
    {
        if(_map_Node_Controller == null)
        {
            Debug.LogError("节点控制器未初始化");
            return false;
        }
        return _map_Node_Controller.Check_Trigger_Event(map_Node_Current);
    }
    public Map_Node Get_Map_Node_Active_Current()
    {
        if(_map_Node_Controller != null)
        {
            return _map_Node_Controller.Get_Map_Node_Active_Current();
        }
        return null;
    }

    //保存和读取地图数据
    public void Save_Map_Data()
    {
        if (Data_Manager.Instance == null) return;
        Data_Manager.Instance.Save_Map_Data(_map_Nodes);
    }
    public void Load_Map_Data()
    {
        if(Data_Manager.Instance != null && Data_Manager.Instance.Check_Load_Data())
        {
            Data_Manager.Instance.Load_Map_Data(_map_Nodes);
        }
        else
        {
            Config_Map_Nodes_Default();
            Config_Map_Nodes_Line_Default();
        }
    }

}
