using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class Map_Node_Line : MonoBehaviour
{
    public void Config_Map_Nodes_Line(Map_Node[,] map_Nodes)
    {
        int layer_MaxNum = map_Nodes.GetLength(0);
        int layer_Node_MaxNum = map_Nodes.GetLength(1);
        for (int layer = 0; layer < layer_MaxNum - 1; layer++)
        {
            //获取当前层节点和下一层节点
            List<Map_Node> nodes_CurrentLayer = new();
            List<Map_Node> nodes_NextLayer = new();
            for (int j = 0; j < layer_Node_MaxNum; j++)
            {
                if (map_Nodes[layer, j]._roomType != RoomType.None)
                {
                    nodes_CurrentLayer.Add(map_Nodes[layer, j]);
                }
                if (map_Nodes[layer + 1, j]._roomType != RoomType.None)
                {
                    nodes_NextLayer.Add(map_Nodes[layer + 1, j]);
                }
            }
            int node_Current_Num = nodes_CurrentLayer.Count;
            int node_Next_Num = nodes_NextLayer.Count;

            //记录每个当前节点连接多少个下一节点
            int[] node_Current_Connect_Num = new int[node_Current_Num];
            //记录当前节点是否连接重复节点
            int[] node_Current_Repeate_Connect = new int[node_Current_Num];
            //下一层被连接的个数
            int node_Next_Connect_Num = 0;
            //初始化连接边数
            for (int index_Current = 0; index_Current < node_Current_Num; index_Current++)
            {
                //初始连接的边数为1条
                node_Current_Connect_Num[index_Current] = 1;
                node_Next_Connect_Num += node_Current_Connect_Num[index_Current];
            }
            //当下一层节点的数量小于当前节点数量
            if (node_Next_Num < node_Current_Num)
            {
                //将随机的节点设置成重复连接节点
                int times = node_Current_Num - node_Next_Num;
                List<int> node_NoRepeat_Index = new();
                for (int index = 1; index < node_Current_Num; index++)
                {
                    if (node_Current_Repeate_Connect[index] == 0)
                    {
                        node_NoRepeat_Index.Add(index);
                    }
                }
                while (times > 0)
                {
                    int size = node_NoRepeat_Index.Count;
                    int random_Index = Random.Range(0, size);
                    node_Current_Repeate_Connect[node_NoRepeat_Index[random_Index]] = 1;
                    node_NoRepeat_Index.RemoveAt(random_Index);
                    node_Next_Connect_Num -= 1;
                    times--;
                }

            }
            //随机设置重复连接节点
            for (int index_Current = 1; index_Current < node_Current_Num; index_Current++)
            {
                //是否重复连接,0为不重复，1为重复
                if (node_Current_Repeate_Connect[index_Current] == 0)
                {
                    int random = Random.Range(0, 10);
                    if (random >= 9)
                    {
                        node_Current_Repeate_Connect[index_Current] = 1;
                        node_Next_Connect_Num -= 1;
                    }
                }
            }
            //当下一层的节点未被连接满时，往随机的节点增加路径
            while (node_Next_Connect_Num < node_Next_Num)
            {
                int random_Node_Current_Index = Random.Range(0, node_Current_Num);
                node_Current_Connect_Num[random_Node_Current_Index] += 1;
                node_Next_Connect_Num++;
            }
            //连接节点
            int index_Next = 0;
            for (int index_Current = 0; index_Current < node_Current_Num; index_Current++)
            {
                int connect_Num = node_Current_Connect_Num[index_Current];
                int repeat_Num = node_Current_Repeate_Connect[index_Current];

                index_Next -= repeat_Num;
                while (connect_Num > 0)
                {
                    if (index_Next >= node_Next_Num) break;
                    Map_Node current_Node = nodes_CurrentLayer[index_Current];
                    Map_Node next_Node = nodes_NextLayer[index_Next];
                    current_Node.AddNextRoom(next_Node);
                    Debug.Log($"{current_Node._layer}层({current_Node._index})与{next_Node._layer}层({next_Node._index})相连");
                    connect_Num--;
                    index_Next++;
                }

            }
        }
    }
    public void Initialize_UI_Map_Line(Map_Node[,] map_Nodes)
    {
        if(Map_UI_Manager.Instance != null)
        {
            Map_UI_Manager.Instance.Initialize_UI_Map_Line(map_Nodes);
        }
    }


}
