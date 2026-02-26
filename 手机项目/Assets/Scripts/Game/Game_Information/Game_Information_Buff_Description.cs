using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class Game_Information_Buff_Description : MonoBehaviour
{
    private GameObject _buff_Description_Position;
    private GameObject _buff_Description_Prefab;
    private ObjectPool<GameObject> _pool_Buff_Description;
    private List<GameObject> _buff_Description_GameObjects;

    public void Set_Game_Information_Buff_Description(GameObject buff_Description_Position, GameObject buff_Description_Prefab)
    {
        _buff_Description_Position = buff_Description_Position;
        _buff_Description_Prefab = buff_Description_Prefab;
        _pool_Buff_Description = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy, true, 10, 1000);
        _buff_Description_GameObjects = new();
    }
    public void Update_Buff_Description(GroupType groupType,List<AirShip_Controller_Buff_Base> airShip_Controller_Buffs)
    {
        foreach(GameObject buff_Description in _buff_Description_GameObjects)
        {
            if (!buff_Description.activeSelf) continue;
            _pool_Buff_Description.Release(buff_Description);
        }
        foreach(AirShip_Controller_Buff_Base airShip_Controller_Buff in airShip_Controller_Buffs)
        {
            GameObject buff_Description = _pool_Buff_Description.Get();
            buff_Description.SetActive(true);
            buff_Description.transform.SetAsLastSibling();
            buff_Description.GetComponent<TextMeshProUGUI>().text = Get_Buff_Description(groupType, airShip_Controller_Buff);
            _buff_Description_GameObjects.Add(buff_Description);
        }
    }
    private string Get_Buff_Description(GroupType groupType, AirShip_Controller_Buff_Base airShip_Controller_Buff)
    {
        string description = "";
        if(groupType == GroupType.Player)
        {
            description = "+ 玩家"+ airShip_Controller_Buff._buff_Description;
        }
        else if(groupType == GroupType.Enemy)
        {
            description = "- 敌方" + airShip_Controller_Buff._buff_Description;
        }
        return description;
    }
    private GameObject CreateFunc()
    {
        if (_buff_Description_Prefab == null)
        {
            Debug.Log("属性UI预制体未初始化");
            return null;
        }
        GameObject attribute = Instantiate(_buff_Description_Prefab, _buff_Description_Position.transform);
        _buff_Description_GameObjects.Add(attribute);
        return attribute;
    }
    private void ActionOnGet(GameObject obj)
    {
        obj.SetActive(true);
    }
    private void ActionOnRelease(GameObject obj)
    {
        obj.SetActive(false);
    }
    private void ActionOnDestroy(GameObject obj)
    {
        obj.SetActive(false);
    }
}
