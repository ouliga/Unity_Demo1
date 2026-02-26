using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Time_Manager : Singleton<Time_Manager>
{
    private List<Time_Counter> _time_Counters_List;

    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        if(Scene_Manager.Instance != null)
        {
            Scene_Manager.Instance._subscribe += Initialize_Time_Manager;
        }
    }
    private void OnDisable()
    {
        if (Scene_Manager.Instance != null)
        {
            Scene_Manager.Instance._subscribe -= Initialize_Time_Manager;
        }
    }
    public void Update_Time_Manager()
    {
        if(_time_Counters_List != null)
        {
            for (int i = 0; i < _time_Counters_List.Count; i++)
            {
                if (_time_Counters_List[i] == null)continue;

                _time_Counters_List[i].Count();
                if (_time_Counters_List[i]._cooling_Time > 0) continue;

                _time_Counters_List[i].Trigger();
                _time_Counters_List[i].Reset_Counter();
            }
        }
    }
    private void Initialize_Time_Manager(Scene_Type scene_Type)
    {
        if(scene_Type == Scene_Type.BattleScene)
        {
            _time_Counters_List = new();
            Debug.Log("时间管理器初始化完成");
        }
    }

    public Time_Counter Create_Time_Counter(float time,UnityAction action)
    {
        Time_Counter time_Counter = new Time_Counter(time,action);
        if (_time_Counters_List != null)
        {
            _time_Counters_List.Add(time_Counter);
        }
        Debug.Log($"添加了计时器(id:{time_Counter._id})时间:{time_Counter._cooling_Time_Max}");
        return time_Counter;
    }

    public void Add_Time_Counter(Time_Counter time_Counter)
    {
        if (time_Counter == null) return;
        time_Counter.Reset_Counter();
        _time_Counters_List.Add(time_Counter);
        Debug.Log($"添加了计时器(id:{time_Counter._id}):{time_Counter._cooling_Time_Max}");
    }

    public void Remove_Time_Counter(int id)
    {
        if (_time_Counters_List != null)
        {
            for(int i = 0; i < _time_Counters_List.Count; i++)
            {
                if (_time_Counters_List[i] == null) continue;
                if (_time_Counters_List[i]._id == id)
                {
                    Debug.Log($"移除了计时器(id:{_time_Counters_List[i]._id}):{_time_Counters_List[i]._cooling_Time_Max}");
                    _time_Counters_List.RemoveAt(i);
                    break;
                }
            }
        }
    }
 
}
