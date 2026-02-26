using System.Collections.Generic;
using UnityEngine;

public class Map_Node_Controller : MonoBehaviour
{
    private Map_Node _map_Node_Active_Current;
    private List<Map_Node> _map_Node_Active;

    public void Initialize_Map_Node_Controller(Map_Node[,] map_Nodes)
    {
        if (_map_Node_Active == null) _map_Node_Active = new();
        int layer_MaxNum = map_Nodes.GetLength(0);
        int layer_Node_MaxNum = map_Nodes.GetLength(1);

        for (int layer = 0; layer < layer_MaxNum; layer++)
        {
            for (int index = 0; index < layer_Node_MaxNum; index++)
            {
                Map_Node map_Node = map_Nodes[layer, index];
                if (map_Node._controller_Active)
                {
                    _map_Node_Active.Add(map_Node);
                }
            }
        }

        if (Map_UI_Manager.Instance != null)
        {
            Map_UI_Manager.Instance.Initialize_Map_Node_Controller(map_Nodes);
        }
    }
    //更新控制器
    private void Update_Map_Node_Controller(Map_Node map_Node, bool active)
    {
        if (Map_UI_Manager.Instance != null)
        {
            map_Node.Change_Active_Controller(active);
            Map_UI_Manager.Instance.Update_Map_Node_Controller(map_Node, active);
        }
    }

    //触发控制器
    public void Trigger_Map_Node_Controller(Map_Node map_Node_Current)
    {
        if (map_Node_Current == null) return;
        //if (Check_Trigger_Event(map_Node_Current))
        //{
        //    _map_Node_Active_Current = map_Node_Current;
        //    Trigger_Event(map_Node_Current);
        //}
        _map_Node_Active_Current = map_Node_Current;
        Trigger_Event(map_Node_Current);
    }
    public bool Check_Trigger_Event(Map_Node map_Node_Current)
    {
        int layer = map_Node_Current._layer;
        int index = map_Node_Current._index;
        RoomType roomType = map_Node_Current._roomType;
        if (roomType == RoomType.Start)
        {
            return true;
        }
        else if (roomType == RoomType.Normal || roomType == RoomType.Hard || roomType == RoomType.Boss)
        {
            if (Data_Manager.Instance == null)
            {
                Debug.LogError("数据管理器未初始化");
                return false;
            }
            if (Inventory_Manager.Instance == null)
            {
                Debug.LogError("仓库管理器未初始化");
                return false;
            }
            if (Scene_Manager.Instance == null)
            {
                Debug.LogError("场景管理器未初始化");
                return false;
            }
            if (!Inventory_Manager.Instance.Check_EquipBag_Current_Valid())
            {
                if (Notice_Manager.Instance != null)
                {
                    Notice_Manager.Instance.Create_Notice("飞船的配置不合理！");
                }
                return false;
            }
            else
            {
                return true;
            }
        }
        else if (roomType == RoomType.Shop)
        {
            if (Shop_Manager.Instance == null)
            {
                Debug.LogError("商店管理器未初始化");
                return false;
            }
            if (UI_Panel_Manager.Instance == null)
            {
                Debug.LogError("UI界面管理器未初始化");
                return false;
            }
            return true;
        }

        return false;
    }
    //触发事件
    private void Trigger_Event(Map_Node map_Node_Current)
    {
        int layer = map_Node_Current._layer;
        int index = map_Node_Current._index;
        RoomType roomType = map_Node_Current._roomType;

        if (roomType == RoomType.Normal || roomType == RoomType.Hard || roomType == RoomType.Boss)
        {
            Data_AirShip data_AirShip = null;
            data_AirShip = Data_Manager.Instance.LoadData_AirShip_By_RoomType(roomType, layer, index);
            Inventory_Manager.Instance.Load_AirShip_Data_Enemy(data_AirShip);
            Scene_Manager.Instance.Enter_Scene(Scene_Type.BattleScene);

            if(roomType != RoomType.Boss)
            {
                Enter_Next_Layer(map_Node_Current);
            }
        }
        else if (roomType == RoomType.Shop)
        {
            Shop_Manager.Instance.Refresh_Shop();
            UI_Panel_Manager.Instance.Open_Shop_Panel();
            Enter_Next_Layer(map_Node_Current);
        }
        else if (roomType == RoomType.Start)
        {
            Shop_Manager.Instance.Refresh_Shop();
            UI_Panel_Manager.Instance.Open_Shop_Panel();
            Enter_Next_Layer(map_Node_Current);
        }
    }
    //进入下一层
    private void Enter_Next_Layer(Map_Node map_Node_Current)
    {
        //去除当前激活的节点
        int size = _map_Node_Active.Count;
        for (int i = 0; i < size; i++)
        {
            Map_Node map_Node = _map_Node_Active[0];
            Update_Map_Node_Controller(map_Node, false);
            _map_Node_Active.RemoveAt(0);
        }
        //激活下一层连接的节点
        foreach (Map_Node map_Node_Next in map_Node_Current._nextNodes)
        {
            _map_Node_Active.Add(map_Node_Next);
            Update_Map_Node_Controller(map_Node_Next, true);
        }
        Debug.Log("进入下一层");
    }
    public Map_Node Get_Map_Node_Active_Current()
    {
        return _map_Node_Active_Current;
    }
}
