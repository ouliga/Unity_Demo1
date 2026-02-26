using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BFS_Mark
{
    private int _node_MaxNum_X;
    private int _node_MaxNum_Y;
    public BFS_Node[,] _nodes {  get; private set; }

    private BFS_Node _node_Core;

    public BFS_Mark(int node_MaxNum_X,int node_MaxNum_Y)
    {
        _node_MaxNum_X = node_MaxNum_X;
        _node_MaxNum_Y = node_MaxNum_Y;
        _nodes = new BFS_Node[node_MaxNum_X+2,node_MaxNum_Y+2];
        Initialize_BFS_Mark();
    }
    private void Initialize_BFS_Mark()
    {
        _node_Core = null;
        for (int i = 0; i < _node_MaxNum_X + 2; i++)
        {
            for (int j = 0; j < _node_MaxNum_Y + 2; j++)
            {
                bool isBlock = true;
                _nodes[i, j] = new BFS_Node(isBlock);

            }
        }
        for (int i = 0; i < _node_MaxNum_X; i++)
        {
            for (int j = 0; j < _node_MaxNum_Y; j++)
            {
                _nodes[i + 1, j + 1].Add_Node_Neighbor(_nodes[i, j + 1]);
                _nodes[i + 1, j + 1].Add_Node_Neighbor(_nodes[i + 2, j + 1]);
                _nodes[i + 1, j + 1].Add_Node_Neighbor(_nodes[i + 1, j]);
                _nodes[i + 1, j + 1].Add_Node_Neighbor(_nodes[i + 1, j + 2]);
            }
        }
    }

    // 仓库装备栏

    public void Update_BFS_EquipBag(Inventory_Slot[,] inventory_EquipBag_Cube, Inventory_Slot[,] inventory_EquipBag_Equipment)
    {
        ResetMark();
        for (int i = 0;i < _node_MaxNum_X; i++)
        {
            for(int j=0;j < _node_MaxNum_Y; j++)
            {
                if (inventory_EquipBag_Cube[i,j]._item != null)
                {
                    _nodes[i + 1, j + 1].Set_Block(false);
                }
            }
        }
    }
    public bool Check_Core_EquipBag(Inventory_Slot[,] inventory_EquipBag_Cube, Inventory_Slot[,] inventory_EquipBag_Equipment)
    {
        _node_Core = null;
        for (int i = 0; i < _node_MaxNum_X; i++)
        {
            for (int j = 0; j < _node_MaxNum_Y; j++)
            {
                if (inventory_EquipBag_Equipment[i, j]._item != null)
                {
                    if (inventory_EquipBag_Equipment[i, j]._item._isCore)
                    {
                        if (_node_Core == null)
                        {
                            _node_Core = _nodes[i + 1, j + 1];
                        }
                        else
                        {
                            Debug.Log("飞船存在不止一个核心");
                            return false;
                        }
                    }
                }
            }
        }
        if(_node_Core == null) return false;
        return true;
    }
    public bool Check_Valid_EquipBag(Inventory_Slot[,] inventory_EquipBag_Cube)
    {
        bool isValid = true;
        for (int i = 0; i < _node_MaxNum_X; i++)
        {
            for (int j = 0; j < _node_MaxNum_Y; j++)
            {
                if (inventory_EquipBag_Cube[i, j]._item == null) continue;
                if (_nodes[i + 1, j + 1] == null) continue;
                if (!_nodes[i + 1, j + 1]._isMark)
                {
                    isValid = false;
                }
            }
        }
        return isValid;
    }
    public void Show_IllegalTips_EquipBag(Inventory_Slot[,] inventory_EquipBag_Cube)
    {
        if (Inventory_UI_Manager.Instance == null) return;
        for (int i = 0; i < _node_MaxNum_X; i++)
        {
            for (int j = 0; j < _node_MaxNum_Y; j++)
            {
                if (inventory_EquipBag_Cube[i, j]._item == null) continue;
                if (_nodes[i + 1, j + 1] == null) continue;
                if (!_nodes[i + 1, j + 1]._isMark)
                {
                    Inventory_UI_Manager.Instance.ShowIllegalTips(i, j);
                }
            }
        }
    }

    /// 飞船控制器
    public void Update_BFS_AirShip_Controller(AirShip_Component[,] airShip_Components)
    {
        ResetMark();
        for (int i = 0; i < _node_MaxNum_X; i++)
        {
            for (int j = 0; j < _node_MaxNum_Y; j++)
            {
                if (airShip_Components[i, j] == null) continue;
                if (!airShip_Components[i, j]._isAlive) continue;
                _nodes[i + 1, j + 1].Set_Block(false);
                if (airShip_Components[i, j]._isCore)
                {
                    _node_Core = _nodes[i + 1, j + 1];
                }
            }
        }
    }
    public bool Check_Core_AirShip_Controller(AirShip_Component[,] airShip_Components)
    {
        ResetMark();
        for (int i = 0; i < _node_MaxNum_X; i++)
        {
            for (int j = 0; j < _node_MaxNum_Y; j++)
            {
                if (airShip_Components[i, j] == null) continue;
                if (!airShip_Components[i, j]._isAlive) continue;
                if (airShip_Components[i, j]._isCore)
                {
                    _node_Core = _nodes[i + 1, j + 1];
                }
            }
        }
        if(_node_Core == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void Destory_IllegalComponent_AirShip_Controller(AirShip_Component[,] airShip_Components)
    {
        for (int i = 0; i < _node_MaxNum_X; i++)
        {
            for (int j = 0; j < _node_MaxNum_Y; j++)
            {
                if (airShip_Components[i, j] == null) continue;
                if (!airShip_Components[i, j]._isAlive) continue;
                if (!_nodes[i + 1, j + 1]._isMark)
                {
                    airShip_Components[i,j].Destory_Component();
                }
            }
        }
    }

    //通用操作
    public void Mark_FromCore()
    {
        if(_node_Core == null)
        {
            Debug.Log("飞船没有核心");
            return;
        }

        List<BFS_Node> toSearch = new List<BFS_Node>() { _node_Core };

        while (toSearch.Any())
        {
            BFS_Node node_Current = toSearch[0];
            node_Current.Set_Mark(true);
            toSearch.Remove(node_Current);
            foreach(BFS_Node node_neighbor in node_Current._node_Neighbors.Where(t => !t._isBlock && !t._isMark))
            {
                bool inSearch = toSearch.Contains(node_neighbor);
                if (!inSearch)
                {
                    toSearch.Add(node_neighbor);
                }    
            }
        }
    }
    private void ResetMark()
    {
        _node_Core = null;
        for (int i = 0; i < _node_MaxNum_X; i++)
        {
            for (int j = 0; j < _node_MaxNum_Y; j++)
            {
                if (_nodes[i + 1, j + 1] == null) continue;
                _nodes[i + 1, j + 1].Set_Block(true);
                _nodes[i + 1, j + 1].Set_Mark(false);
            }
        }
    }

}
