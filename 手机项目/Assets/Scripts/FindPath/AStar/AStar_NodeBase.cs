using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class AStar_NodeBase
{
    public int _index_X {  get;private set; }
    public int _index_Y { get;private set; }
    public List<AStar_NodeBase> _neighbor_Nodes {  get; private set; }
    public int _cost_FromStar {  get; private set; }
    public int _cost_ToEnd {  get; private set; }
    public int _cost_All => _cost_FromStar + _cost_ToEnd;
    public bool _isBlock {  get; private set; }

    public void Initialize_AStar_NodeBase(int index_X,int index_Y, bool isBlock)
    {
        _index_X = index_X;
        _index_Y = index_Y;
        _isBlock = isBlock;
    }
    public void Set_Cost_FromStar(int cost)
    {
        _cost_FromStar = cost;
    }
    public void Set_Cost_ToEnd(int cost)
    {
        _cost_ToEnd = cost;
    }
    public void Set_Block(bool isBlock)
    {
        _isBlock = isBlock;
    }
    public int GetDistance(AStar_NodeBase node)
    {
        return Mathf.Abs(node._index_X - _index_X) + Mathf.Abs(node._index_Y - _index_Y);
    }
}
