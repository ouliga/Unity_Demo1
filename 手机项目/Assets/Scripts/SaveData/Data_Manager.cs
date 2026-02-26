// 用于文件读写
using System.IO;
// 用于json序列化和反序列化
using Newtonsoft.Json;
using UnityEngine;

public class Data_Manager : Singleton<Data_Manager>
{
    private Data _current_Data;
    //游戏存档//
    public void SaveData()
    {
        Debug.Log("尝试存档");
        if (_current_Data == null)
        {
            CreateData();
        }
        // 在persistentDataPath下再创建一个/users文件夹，方便管理
        if (!Directory.Exists(Application.persistentDataPath + "/users"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/users");
        }
        // 转换用户数据为JSON字符串
        string jsonData = JsonConvert.SerializeObject(_current_Data);
        // 将JSON字符串写入文件中(文件名为userData.name)
        File.WriteAllText(Application.persistentDataPath + string.Format("/users/{0}.json", _current_Data._name), jsonData);
        Debug.Log("存档存储在:"+ string.Format(Application.persistentDataPath + "/users/{0}.json", _current_Data._name));
    }
    public void CreateData()
    {
        _current_Data = new Data("0");
        _current_Data.Update_DataState(DataState.NewData);
    }
    public void LoadData(string filename)
    {
        Debug.Log($"读取存档(Application.persistentDataPath+{string.Format("/users/{0}.json", filename)})");
        string path = Application.persistentDataPath + string.Format("/users/{0}.json", filename);
        // 检查用户配置文件是否存在
        if (File.Exists(path))
        {
            // 从文本文件中加载JSON字符串
            string jsonData = File.ReadAllText(path);
            // 将JSON字符串转换为用户内存数据
            Data data = JsonConvert.DeserializeObject<Data>(jsonData);
            _current_Data = data;
            _current_Data.Update_DataState(DataState.OldData);
            Debug.Log("读取存档成功");
        }
        else
        {
            Debug.Log("存档路径不存在");
            return;
        }
    }
    public bool Check_Load_Data()
    {
        if(_current_Data == null) return false;
        if(_current_Data._dataState == DataState.OldData)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //飞船数据//
    public void SaveData_AirShip(Data_AirShip data_AirShip)
    {
        Debug.Log("尝试保存飞船数据");
        if(data_AirShip == null)
        {
            Debug.Log("无法保存空的飞船数据");
            return;
        }
        // 在persistentDataPath下再创建一个/users文件夹，方便管理
        string default_Path = "D:/Unity/Project/手机项目/Assets/Resources/Data/Data_AirShip";
        if (!Directory.Exists(default_Path))
        {
            Debug.Log($"({default_Path})路径不存在");
            Directory.CreateDirectory(default_Path);
        }
        else
        {
            Debug.Log($"({default_Path})路径已存在");
        }
        // 转换飞船数据为JSON字符串
        string jsonData = JsonConvert.SerializeObject(data_AirShip);
        //将JSON字符串写入文件中
        File.WriteAllText(default_Path + "/0.json",jsonData);
        Debug.Log("飞船数据存储在:" + default_Path + "/0.json");
    }

    public Data_AirShip LoadData_AirShip(string filename)
    {
        Data_AirShip data_AirShip = null;
        string path = "Data/Data_AirShip/"+filename;
        Debug.Log($"尝试飞船读取{filename}");
        TextAsset data = Resources.Load<TextAsset>(path);

        if (data == null)
        {
            //读取默认数据
            Debug.Log("读取默认数据");
            path = "Data/Data_AirShip/0";
            data = Resources.Load<TextAsset>(path);
        }

        if (data != null)
        {
            data_AirShip = JsonConvert.DeserializeObject<Data_AirShip>(data.text);
            Debug.Log($"读取飞船数据({path})成功");
        }
        return data_AirShip;
    }
    //根据房间类型读取敌方飞船数据
    public Data_AirShip LoadData_AirShip_By_RoomType(RoomType roomType,int layer,int index)
    {
        string filename = "";
        if(roomType == RoomType.Normal)
        {
            filename = "Normal/";
        }
        else if(roomType == RoomType.Hard)
        {
            filename = "Hard/";
        }
        else if(roomType == RoomType.Boss)
        {
            filename = "Boss/";
        }
        filename = filename + $"/{layer}/{index}";
        return LoadData_AirShip(filename);
    }
    //保存或读取游戏数据
    public void Save_Game_Difficulty_Degree_Data(int data_Game_Difficulty_Degree)
    {
        if (_current_Data != null)
        {
            _current_Data.Save_Game_Difficulty_Degree_Data(data_Game_Difficulty_Degree);
        }
    }
    public void Load_Game_Difficulty_Degree_Data(Game_Manager_Buff game_Manager_Buff)
    {
        if (_current_Data != null)
        {
            _current_Data.Load_Game_Difficulty_Degree_Data(game_Manager_Buff);
        }
    }
    //保存或读取仓库背包数据
    public void Save_Inventory_Bag_Data(Inventory_Slot[] inventory_Bag)
    {
        if (_current_Data != null)
        {
            _current_Data.Save_Inventory_Bag_Data(inventory_Bag);
        }
    }
    public void Load_Inventory_Bag_Data(Inventory_Slot[] inventory_Bag)
    {
        if(_current_Data != null)
        {
            _current_Data.Load_Inventory_Bag_Data(inventory_Bag);
        }
    }
    public void Save_Inventory_EquipBag_Data(Inventory_Slot[,] inventory_EquipBag_Cube, Inventory_Slot[,] inventory_EquipBag_Equipment)
    {
        if (_current_Data != null)
        {
            _current_Data.Save_Inventory_EquipBag_Data(inventory_EquipBag_Cube, inventory_EquipBag_Equipment);
        }
    }
    public void Load_Inventory_EquipBag_Data(Inventory_Slot[,] inventory_EquipBag_Cube, Inventory_Slot[,] inventory_EquipBag_Equipment)
    {
        if(_current_Data != null)
        {
            _current_Data.Load_Inventory_EquipBag_Data(inventory_EquipBag_Cube, inventory_EquipBag_Equipment);
        }
    }
    //保存或读取商店数据
    public void Save_Shop_Data(Inventory_Slot[] shop_Items, Shop_Resource shop_Resource)
    {
        if (_current_Data != null)
        {
            _current_Data.Save_Shop_Data(shop_Items, shop_Resource);
        }
    }
    public void Load_Shop_Data(Inventory_Slot[] shop_Items, Shop_Resource shop_Resource)
    {
        if(_current_Data != null)
        {
            _current_Data.Load_Shop_Data(shop_Items, shop_Resource);
        }
    }
    //读取地图数据
    public void Save_Map_Data(Map_Node[,] map_Nodes)
    {
        if(_current_Data != null)
        {
            _current_Data.Save_Map_Data(map_Nodes);
        }
    }
    public void Load_Map_Data(Map_Node[,] map_Nodes)
    {
        if(_current_Data != null)
        {
            _current_Data.Load_Map_Data(map_Nodes);
        }
    }

    

}
