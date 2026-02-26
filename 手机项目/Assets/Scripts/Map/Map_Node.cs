using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    None, Start, Normal, Hard, Shop, Boss
}
public class Map_Node
{
    public int _layer { get; private set; }
    public int _index {  get; private set; }
    public RoomType _roomType { get; private set; }
    public List<Map_Node> _nextNodes { get; private set; }
    public bool _controller_Active { get; private set; }
    public Map_Node(int layer,int index,RoomType roomType)
    {
        _layer = layer;
        _index = index;
        _roomType = roomType;
        _nextNodes = new();
        _controller_Active = false;
    }
    public Map_Node AddNextRoom(Map_Node map_Node)
    {
        _nextNodes.Add(map_Node);
        return this;
    }
    public void Change_RoomType(RoomType roomType)
    {
        _roomType = roomType;
    }
    public void Change_Active_Controller(bool active)
    {
        _controller_Active = active;
    }
}
