using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Panel_Battle_Over : UI_Panel_Base
{
    [SerializeField] private int _panelLevel_Battle_Over = 1;
    public override int _panelLevel
    {
        get
        {
            return _panelLevel_Battle_Over;
        }
    }
    public override void OnEnter()
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
    }

    public override void OnExit()
    {
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
    }
}
