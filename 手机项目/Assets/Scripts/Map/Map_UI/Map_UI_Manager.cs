using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map_UI_Manager : Singleton<Map_UI_Manager>
{
    [SerializeField] GameObject _map_Node_Prefab;
    [SerializeField] GameObject _map_Node_Position_Prefab;
    //[SerializeField] GameObject _map_Node_Start_Prefab;
    //[SerializeField] GameObject _map_Node_Normal_Prefab;
    //[SerializeField] GameObject _map_Node_Hard_Prefab;
    //[SerializeField] GameObject _map_Node_Shop_Prefab;
    //[SerializeField] GameObject _map_Node_Boss_Prefab;
    [SerializeField] GameObject _map_Position;
    [SerializeField] GameObject _map_Line_Prefab;
    private int _layer_MaxNum;
    private int _layer_Node_MaxNum;
    private GameObject[,] _map_Node_Position_GameObjects;
    private GameObject[,] _map_Node_GameObjects;
    private Button_MapNode[,] _map_Node_Controllers;
    public void Awake() { }

    public void Set_Map_UI_Manager(int layer_MaxNum,int layer_Node_MaxNum)
    {
        _layer_MaxNum = layer_MaxNum;
        _layer_Node_MaxNum = layer_Node_MaxNum;
        _map_Node_Position_GameObjects = new GameObject[_layer_MaxNum, _layer_Node_MaxNum];
        _map_Node_GameObjects = new GameObject[_layer_MaxNum,_layer_Node_MaxNum];
        _map_Node_Controllers = new Button_MapNode[_layer_MaxNum, _layer_Node_MaxNum];
    }
    //初始化地图节点
    public void Initialize_UI_Map_Nodes(Map_Node[,] map_Nodes)
    {
        if (map_Nodes == null || _map_Node_GameObjects == null) return;
        if (_map_Position == null)
        {
            Debug.Log("地图格子放置位置未初始化");
            return;
        }
        for (int i = 0; i < _layer_MaxNum; i++)
        {
            for (int j = 0; j < _layer_Node_MaxNum; j++)
            {
                if(_map_Node_Position_GameObjects[i, j] == null)
                {
                    _map_Node_Position_GameObjects[i, j] = Instantiate(_map_Node_Position_Prefab, _map_Position.transform);
                }
                if(_map_Node_GameObjects[i, j] == null)
                {
                    _map_Node_GameObjects[i, j] = Instantiate(_map_Node_Prefab, _map_Node_Position_GameObjects[i, j].transform);
                    _map_Node_GameObjects[i, j] = _map_Node_GameObjects[i, j].transform.Find("地图_图标").gameObject;
                }

                Image image = _map_Node_GameObjects[i, j].GetComponent<Image>();
                if (image == null) continue;
                if (map_Nodes[i, j]._roomType == RoomType.Start)
                {
                    image.sprite = Load_Map_Node_Sprite("图标_地图_事件");
                    //_map_Node_GameObjects[i, j] = Instantiate(_map_Node_Start_Prefab, _map_Node_Position_GameObjects[i, j].transform);
                }
                else if (map_Nodes[i, j]._roomType == RoomType.Normal)
                {
                    image.sprite = Load_Map_Node_Sprite("图标_地图_普通");
                    //_map_Node_GameObjects[i, j] = Instantiate(_map_Node_Normal_Prefab, _map_Node_Position_GameObjects[i, j].transform);
                }
                else if (map_Nodes[i, j]._roomType == RoomType.Hard)
                {
                    image.sprite = Load_Map_Node_Sprite("图标_地图_精英");
                    //_map_Node_GameObjects[i, j] = Instantiate(_map_Node_Hard_Prefab, _map_Node_Position_GameObjects[i, j].transform);
                }
                else if (map_Nodes[i, j]._roomType == RoomType.Shop)
                {
                    image.sprite = Load_Map_Node_Sprite("图标_地图_事件");
                    //_map_Node_GameObjects[i, j] = Instantiate(_map_Node_Shop_Prefab, _map_Node_Position_GameObjects[i, j].transform);
                }
                else if (map_Nodes[i, j]._roomType == RoomType.Boss)
                {
                    image.sprite = Load_Map_Node_Sprite("图标_地图_BOSS");
                    //_map_Node_GameObjects[i, j] = Instantiate(_map_Node_Boss_Prefab, _map_Node_Position_GameObjects[i, j].transform);
                }
                else
                {
                    _map_Node_GameObjects[i, j].transform.parent.gameObject.SetActive(false);
                    //_map_Node_GameObjects[i, j] = Instantiate(_map_Node_None_Prefab, _map_Node_Position_GameObjects[i, j].transform);
                }
            }
        }
    }
    //获得地图节点贴图
    private Sprite Load_Map_Node_Sprite(string name)
    {
        string path = $"UI/图标/地图/{name}";
        Sprite sprite = Resources.Load<Sprite>(path);
        if (sprite == null)
        {
            Debug.LogError("并未找到对应图标：" + name);
        }
        return sprite;
    }

    //初始化地图连线
    public void Initialize_UI_Map_Line(Map_Node[,] map_Nodes)
    {
        if (map_Nodes == null) return;
        if (_map_Node_Position_GameObjects == null) return;
        if (_map_Line_Prefab == null) return;
        //强制刷新更新Grid的坐标
        Canvas.ForceUpdateCanvases();
        int _layer_MaxNum = map_Nodes.GetLength(0);
        int _layer_Node_MaxNum = map_Nodes.GetLength(1);
        for (int layer = 0; layer < _layer_MaxNum; layer++)
        {
            for (int index = 0; index < _layer_Node_MaxNum; index++)
            {
                if (map_Nodes[layer, index]._roomType != RoomType.None)
                {
                    RectTransform start_Transform = _map_Node_Position_GameObjects[layer, index].GetComponent<RectTransform>();
                    List<Map_Node> map_Next_Nodes = map_Nodes[layer, index]._nextNodes;
                    foreach (Map_Node map_Next_Node in map_Next_Nodes)
                    {
                        GameObject map_Node_Position_GameObject = _map_Node_Position_GameObjects[map_Next_Node._layer, map_Next_Node._index];
                        RectTransform end_Transform = map_Node_Position_GameObject.GetComponent<RectTransform>();
                        //Debug.Log($"({layer},{index})与({map_Next_Node._layer},{map_Next_Node._index})连接");
                        Create_UI_Line(start_Transform, end_Transform);
                    }
                }
            }
        }
    }
    //创建节点间的连线
    private void Create_UI_Line(RectTransform start_Transform, RectTransform end_Transform)
    {
        if (start_Transform == null || end_Transform == null) return;
        if (_map_Line_Prefab == null) return;
        GameObject line = Instantiate(_map_Line_Prefab, start_Transform);
        line.transform.SetAsFirstSibling();
        //调整线段的中心位置，长度，角度
        Vector2 start_Position = start_Transform.position;
        Vector2 end_Posiiton = end_Transform.position;
        RectTransform line_RectTransform = line.GetComponent<RectTransform>();
        //设置长度
        float distance = Vector2.Distance(start_Transform.anchoredPosition, end_Transform.anchoredPosition);
        //Debug.Log($"两点的距离为{distance}");
        line_RectTransform.sizeDelta = new Vector2(distance, 15);
        //设置中心位置
        Vector2 center_Positon = (start_Position + end_Posiiton) / 2;
        //Debug.Log($"中心点位置为{center_Positon}");
        line_RectTransform.position = center_Positon;
        //设置角度
        float line_Angle = Vector2.Angle(end_Posiiton - start_Position, Vector2.right);
        //Debug.Log($"线段旋转的角度为{line_Angle}");
        if ((end_Posiiton - start_Position).y >= 0)
        {
            line_RectTransform.Rotate(0, 0, line_Angle);
        }
        else
        {
            line_RectTransform.Rotate(0, 0, -line_Angle);
        }
        //调整线段的间隔
        //为每个线段创建特有的材质
        Material line_Material = line.GetComponent<Image>().material;
        Material line_Material_Clone = Instantiate(line_Material);
        line.GetComponent<Image>().material = line_Material_Clone;

        //修改间隔
        float line_Interval = 30;
        Vector2 tiling = new Vector2(distance / line_Interval, 0);
        int id = Shader.PropertyToID("_MainTex");
        line_Material_Clone.SetTextureScale(id, tiling);
    }
    //初始化地图节点控制器
    public void Initialize_Map_Node_Controller(Map_Node[,] map_Nodes)
    {
        for (int i = 0; i < _layer_MaxNum; i++)
        {
            for (int j = 0; j < _layer_Node_MaxNum; j++)
            {
                Map_Node map_Node = map_Nodes[i, j];
                if (_map_Node_GameObjects[i, j] != null)
                {
                    _map_Node_Controllers[i, j] = _map_Node_GameObjects[i, j].AddComponent<Button_MapNode>();
                    _map_Node_Controllers[i, j].Set_Map_Node(map_Node);
                    Update_Map_Node_Controller(map_Node, map_Node._controller_Active);
                    //_map_Node_Controllers[i, j].GetComponent<Button>().interactable = map_Node._controller_Active;
                }
            }
        }
    }
    public void Update_Map_Node_Controller(Map_Node map_Node,bool active)
    {
        int layer = map_Node._layer;
        int index = map_Node._index;
        if(_map_Node_Controllers[layer, index] != null)
        {
            _map_Node_Controllers[layer, index].GetComponent<Button>().interactable = active;
        }
    }
}
