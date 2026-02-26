using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class AirShip_Equipment_Shoot : MonoBehaviour
{
    private AirShip_Equipment_Attribute _equipment_Attribute;
    private AirShip_Bullet_Attribute _airShip_Bullet_Attribute;
    private GameObject _bullet;
    private ObjectPool<GameObject> _pool_Bullet;
    private List<GameObject> _bullet_List;

    private Time_Counter _time_Counter_Shoot;
    private Coroutine _coroutine_Shoot_Delay;

    private bool _isActive;

    public void Initialize_AirShip_Equipment_Shoot(AirShip_Equipment_Attribute equipment_Attribute, AirShip_Bullet_Attribute airShip_Bullet_Attribute)
    {
        _equipment_Attribute = equipment_Attribute;
        _airShip_Bullet_Attribute = airShip_Bullet_Attribute;
        Get_Bullet_Prefab();

        Initialize_Pool_Bullet();
        Initialize_Time_Counter();

        _isActive = true;
    }

    private void Shoot()
    {
        if (_pool_Bullet != null)
        {
            if (_coroutine_Shoot_Delay == null)
            {
                _coroutine_Shoot_Delay = StartCoroutine(Shoot_Delay());
            }
            else
            {
                StopCoroutine(_coroutine_Shoot_Delay);
                _coroutine_Shoot_Delay = StartCoroutine(Shoot_Delay());
            }
        }
    }
    IEnumerator Shoot_Delay()
    {
        if (_equipment_Attribute != null)
        {
            int times = _equipment_Attribute._attack_Times;
            for (int i = 0; i < times; i++)
            {
                GameObject bullet = _pool_Bullet.Get();
                yield return new WaitForSeconds(_equipment_Attribute._attack_Delay);
            }
        }
        yield return null;
    }
    public void Active_Shoot()
    {
        if (!_isActive)
        {
            _isActive = true;
            if (_time_Counter_Shoot != null)
            {
                _time_Counter_Shoot.Reset_Counter();
                _time_Counter_Shoot.Active_Counter();
            }
        }
    }
    public void Stop_Shoot()
    {
        if (_isActive)
        {
            _isActive = false;
            if (_coroutine_Shoot_Delay != null) StopCoroutine(_coroutine_Shoot_Delay);
            if(_time_Counter_Shoot != null) _time_Counter_Shoot.Stop_Counter();
            Release_All_Bullets();
        }
    }
    private void Release_All_Bullets()
    {
        if (_bullet_List == null) return;
        foreach (GameObject bullet in _bullet_List)
        {
            if (bullet.activeSelf)
            {
                AirShip_Bullet airShip_Bullet = bullet.GetComponent<AirShip_Bullet>();
                if (airShip_Bullet != null)
                {
                    airShip_Bullet.Release_AirShip_Bullet();
                }
            }
        }
    }

    private void Initialize_Time_Counter()
    {
        if (Time_Manager.Instance == null) return;
        if (_equipment_Attribute == null) return;
        if (_time_Counter_Shoot == null)
        {
            _time_Counter_Shoot = Time_Manager.Instance.Create_Time_Counter(_equipment_Attribute._attack_CD, Shoot);
        }
    }
    //获得子弹预制体
    private void Get_Bullet_Prefab()
    {
        if (_airShip_Bullet_Attribute == null) return;
        Bullet_Type _bullet_Type = _airShip_Bullet_Attribute._bullet_Type;
        string path = "Prefab/AirShip/Bullet";
        switch (_bullet_Type)
        {
            case Bullet_Type.Common:
                path = path + "/Bullet_Common";
                break;
            case Bullet_Type.Pierce:
                path = path + "/Bullet_Pierce";
                break;
            case Bullet_Type.Block:
                path = path + "/Bullet_Block";
                break;
        }
        _bullet = Resources.Load<GameObject>(path);
        if (_bullet == null)
        {
            Debug.Log($"该类型({_bullet_Type})的子弹未能读取");
        }
        else
        {
            Debug.Log($"该类型({_bullet_Type})的子弹读取成功");
        }
    }
    //子弹对象池
    private void Initialize_Pool_Bullet()
    {
        if (_bullet != null)
        {
            _pool_Bullet = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy, true, 10, 1000);
            _bullet_List = new();
        }
        else
        {
            Debug.Log("未配置子弹预制体");
        }
    }

    private GameObject CreateFunc()
    {
        if (_airShip_Bullet_Attribute == null)
        {
            Debug.Log("物品信息未初始化");
            return null;
        }

        int direction = _airShip_Bullet_Attribute._bullet_Direction;
        Transform position = _airShip_Bullet_Attribute._bullet_Position;
        GroupType groupType = _airShip_Bullet_Attribute._bullet_GroupType;

        GameObject bullet = Instantiate(_bullet, position);
        _bullet_List.Add(bullet);
        //子弹的朝向
        bullet.transform.eulerAngles = new Vector3(0, 180 * (-(float)direction / 2 + 0.5f), 0);
        bullet.GetComponent<AirShip_Bullet>().Initialize_AirShip_Bullet(_airShip_Bullet_Attribute, _pool_Bullet, groupType);
        bullet.GetComponent<SpriteRenderer>().sortingOrder = 2;
        return bullet;
    }
    private void ActionOnGet(GameObject obj)
    {
        obj.SetActive(true);
        Transform position = _airShip_Bullet_Attribute._bullet_Position;
        obj.transform.position = position.position;
        obj.GetComponent<AirShip_Bullet>().Reset_AirShip_Bullet();
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
