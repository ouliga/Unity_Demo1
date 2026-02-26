using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Panel_Difficulty : UI_Panel_Base
{
    [SerializeField] private int _panelLevel_Difficulty = 1;
    public override int _panelLevel
    {
        get
        {
            return _panelLevel_Difficulty;
        }
    }
    public override void OnEnter()
    {
        Debug.Log("打开难度选择界面");
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
    }

    public override void OnExit()
    {
        Debug.Log("关闭难度选择界面");
        gameObject.SetActive(false);
    }

    public override void OnPause()
    {
        Debug.Log("暂停难度选择界面");
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public override void OnResume()
    {
        Debug.Log("恢复难度选择界面");
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public override void RequestOpen()
    {
        
    }
}
