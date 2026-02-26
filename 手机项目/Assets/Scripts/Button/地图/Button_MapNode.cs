using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_MapNode : Button_Base
{
    public Map_Node _map_Node {  get; private set; }
    public void Set_Map_Node(Map_Node map_Node)
    {
        _map_Node = map_Node;
    }
    public override void OnClick()
    {
        if(Map_Manager.Instance != null)
        {
            if (Map_Manager.Instance.Check_Trigger_Event(_map_Node))
            {
                Map_Manager.Instance.Trigger_Map_Node_Controller(_map_Node);
            }
            
        }

    }
}
