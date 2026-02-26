using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS_Node
{
    public List<BFS_Node> _node_Neighbors {  get; private set; }
    public bool _isBlock {  get; private set; }
    public bool _isMark {  get; private set; }

    public BFS_Node(bool isBlock)
    {
        _node_Neighbors = new List<BFS_Node>();
        _isBlock = isBlock;
        _isMark = false;
    }

    public void Set_Block(bool isBlock)
    {
        _isBlock = isBlock;
    }
    public void Set_Mark(bool isMark)
    {
        _isMark = isMark;
    }
    public void Add_Node_Neighbor(BFS_Node node)
    {
        if (_node_Neighbors == null) return;
        _node_Neighbors.Add(node);
    }

}
