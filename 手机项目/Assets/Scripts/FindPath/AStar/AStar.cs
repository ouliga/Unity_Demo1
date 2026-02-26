using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStar
{
    public static bool FindPath(AStar_NodeBase startNode, AStar_NodeBase endNode)
    {
        //正在搜查的节点
        List<AStar_NodeBase> toSearch = new List<AStar_NodeBase>() { startNode };
        //已经搜查的节点路径
        List<AStar_NodeBase> processed = new List<AStar_NodeBase >();

        while (toSearch.Any())
        {
            AStar_NodeBase node_Current = toSearch[0];
            foreach(AStar_NodeBase node in toSearch)
            {
                //当搜寻节点的总代价小于当前节点的总代价，或者当总代价相等时搜寻节点到目标的代价小于当前节点到目标的代价
                if(node._cost_All < node_Current._cost_All || node._cost_All == node_Current._cost_All && node._cost_ToEnd < node_Current._cost_ToEnd)
                {
                    node_Current = node;
                }
            }

            if (node_Current == endNode)
            {
                Debug.Log("找到了两个节点间的路径");
                return true;
            }

            processed.Add(node_Current);
            toSearch.Remove(node_Current);

            foreach(AStar_NodeBase node_Neighbor in node_Current._neighbor_Nodes.Where(t => !t._isBlock && !processed.Contains(t)))
            {
                bool inSearch = toSearch.Contains(node_Neighbor);
                int cost_ToNeighbor = node_Current._cost_FromStar + node_Current.GetDistance(node_Neighbor);

                if(!inSearch || cost_ToNeighbor < node_Neighbor._cost_FromStar)
                {
                    node_Neighbor.Set_Cost_FromStar(cost_ToNeighbor);
                    if (!inSearch)
                    {
                        node_Neighbor.Set_Cost_ToEnd(node_Neighbor.GetDistance(endNode));
                        toSearch.Add(node_Neighbor);
                    }
                }
            }


        }
        Debug.Log("未找到两个节点间的路径");
        return false;
    }
}
