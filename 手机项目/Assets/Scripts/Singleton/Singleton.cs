using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected bool _has_Initialize = false;
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                return FindObjectOfType<T>();
            }
            return instance;
        }
    }
    protected void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
