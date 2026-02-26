using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Panel_Menu : UI_Panel_Base
{
    [SerializeField] private int _panelLevel_Menu = 2;
    public override int _panelLevel
    {
        get
        {
            return _panelLevel_Menu;
        }
    }
    
    public override void OnEnter()
    {
        Debug.Log("打开菜单");
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
    }

    public override void OnExit()
    {
        Debug.Log("关闭菜单");
        gameObject.SetActive(false);
    }

    public override void OnPause()
    {
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public override void OnResume()
    {
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public override void RequestOpen()
    {
        throw new System.NotImplementedException();
    }
}
