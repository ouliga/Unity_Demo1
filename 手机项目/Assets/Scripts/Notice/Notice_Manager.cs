using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class Notice_Manager : Singleton<Notice_Manager>
{
    [SerializeField]private GameObject _notice_Position;
    [SerializeField] private GameObject _notice_Prefab;
    private ObjectPool<GameObject> _pool_Notice_Prefabs;
    private List<GameObject> _list_Notice_Prefabs;

    public void Awake()
    {
        
    }
    private void OnEnable()
    {
        _pool_Notice_Prefabs = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy, true, 3, 3);
        _list_Notice_Prefabs = new();
    }
    public void Create_Notice(string content)
    {
        if (_pool_Notice_Prefabs == null) return;
        _notice_Position.transform.SetAsLastSibling();
        GameObject notice = _pool_Notice_Prefabs.Get();
        if(notice != null)
        {
            TextMeshProUGUI text = notice.transform.Find("文本").GetComponent<TextMeshProUGUI>();
            if (text != null)
            {
                text.text = content;
            }
            StartCoroutine(Release_Delay(1f, notice));
        }
    }
    IEnumerator Release_Delay(float time,GameObject notice)
    {
        yield return new WaitForSeconds(time);
        if (_pool_Notice_Prefabs != null)
        {
            _pool_Notice_Prefabs.Release(notice);
        }
        yield return null;
    }

    //对象池
    private GameObject CreateFunc()
    {
        if (_notice_Prefab == null || _notice_Position == null)
        {
            Debug.LogError("提示预制体未初始化");
            return null;
        }
        GameObject attribute = Instantiate(_notice_Prefab, _notice_Position.transform);
        _list_Notice_Prefabs.Add(attribute);
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
