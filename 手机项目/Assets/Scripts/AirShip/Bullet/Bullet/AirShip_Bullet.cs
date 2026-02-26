using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public abstract class AirShip_Bullet : MonoBehaviour
{
    public GroupType _groupType {  get; private set; }
    private AirShip_Bullet_Attribute _bullet_Attribute;
    public AirShip_Bullet_Attribute _bullet_Attribute_Current {  get; private set; }

    private bool _hasRelease;
    private List<Collider2D> _colliders;

    private Vector2 _bullet_Size;
    private Rigidbody2D _rigidbody2D;
    private AirShip_Bullet_Fly _airShip_Bullet_Fly;

    private ObjectPool<GameObject> _pool_Bullet;
    private Time_Counter _time_Counter_AutoRelease;

    //当进入触发器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_bullet_Attribute_Current == null) return;
        if (_hasRelease) return;
        if (_colliders.Contains(collision)) return;

        AirShip_Component airShip_Component = collision.GetComponent<AirShip_Component>();
        AirShip_Bullet airShip_Bullet = collision.GetComponent<AirShip_Bullet>();
        AirShip_Equipment airShip_Equipment = collision.GetComponent<AirShip_Equipment>();

        if(airShip_Component != null && airShip_Component._groupType != _groupType && airShip_Component.Check_Hit())
        {
            Hit_Component(airShip_Component);
        }
        else if(airShip_Bullet != null && airShip_Bullet._groupType != _groupType && airShip_Bullet.Check_Hit())
        {
            Hit_Bullet(airShip_Bullet);
        }
        else if(airShip_Equipment != null && airShip_Equipment._groupType == _groupType && airShip_Equipment.Check_Hit())
        {
            airShip_Equipment.Buff_Bullet(_bullet_Attribute_Current);
            Update_AirShip_Bullet();
        }
        //防止重复判断
        _colliders.Add(collision);
    }

    public abstract void Hit_Component(AirShip_Component airShip_Component);
    public abstract void Hit_Bullet(AirShip_Bullet airShip_Bullet);
    public abstract void BeBlocked();

    public void Initialize_AirShip_Bullet(AirShip_Bullet_Attribute airShip_Bullet_Attribute, ObjectPool<GameObject> pool_Bullet, GroupType groupType)
    {
        _bullet_Size = Get_Bullet_Size();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _airShip_Bullet_Fly = new AirShip_Bullet_Fly(_rigidbody2D);

        _pool_Bullet = pool_Bullet;
        _groupType = groupType;
        _hasRelease = false;
        _colliders = new();

        _bullet_Attribute = airShip_Bullet_Attribute;
        _bullet_Attribute_Current = new AirShip_Bullet_Attribute();
        _bullet_Attribute_Current.Copy(_bullet_Attribute);

        Reset_AirShip_Bullet_Rigidbody();

        Initialize_Time_Counter();
    }
    //检测撞击
    public bool Check_Hit()
    {
        return !_hasRelease;
    }
    //获得子弹大小
    private Vector2 Get_Bullet_Size()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) return Vector2.zero;
        return spriteRenderer.size;
    }
    //初始化计时器
    private void Initialize_Time_Counter()
    {
        if (Time_Manager.Instance == null) return;
        if (_bullet_Attribute_Current == null) return;
        if(_time_Counter_AutoRelease == null)
        {
            _time_Counter_AutoRelease = Time_Manager.Instance.Create_Time_Counter(_bullet_Attribute_Current._bullet_ExistTime, Release_AirShip_Bullet);
        }
    }
    private void Update_AirShip_Bullet()
    {
        Update_AirShip_Bullet_Rigidbody();
        Update_AirShip_Bullet_Size();
    }
    public void Reset_AirShip_Bullet()
    {
        _bullet_Attribute_Current.Copy(_bullet_Attribute);
        _hasRelease = false;
        _colliders.Clear();


        Reset_AirShip_Bullet_Rigidbody();
        Reset_AirShip_Bullet_Size();
        if (_time_Counter_AutoRelease != null)
        {
            _time_Counter_AutoRelease.Reset_Counter();
            _time_Counter_AutoRelease.Active_Counter();
        }
    }

    //更新子弹的运动状态
    private void Update_AirShip_Bullet_Rigidbody()
    {
        if(_airShip_Bullet_Fly != null)
        {
            _airShip_Bullet_Fly.Update_AirShip_Bullet_Rigidbody(_bullet_Attribute_Current);
        }
    }
    //更新子弹的大小
    private void Update_AirShip_Bullet_Size()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer != null)
        {
            float size_X = _bullet_Size.x * _bullet_Attribute_Current._bullet_Size;
            float size_Y = _bullet_Size.y * _bullet_Attribute_Current._bullet_Size;
            spriteRenderer.size = new Vector2(size_X, size_Y);
        }
    }
    //重置子弹的运动状态
    private void Reset_AirShip_Bullet_Rigidbody()
    {
        if (_airShip_Bullet_Fly != null)
        {
            _airShip_Bullet_Fly.Reset_AirShip_Bullet_Rigidbody(_bullet_Attribute);
        }
    }
    private void Reset_AirShip_Bullet_Size()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            float size_X = _bullet_Size.x * _bullet_Attribute._bullet_Size;
            float size_Y = _bullet_Size.y * _bullet_Attribute._bullet_Size;
            spriteRenderer.size = new Vector2(size_X, size_Y);
        }
    }

    public void Release_AirShip_Bullet()
    {
        if (_pool_Bullet == null) return;
        if (_hasRelease) return;

        if (_time_Counter_AutoRelease != null)
        {
            _time_Counter_AutoRelease.Stop_Counter();
        }

        _pool_Bullet.Release(gameObject);
        _hasRelease = true;
    }
}
