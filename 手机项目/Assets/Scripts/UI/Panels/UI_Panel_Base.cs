using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_Panel_Base : MonoBehaviour
{
    public abstract int _panelLevel {  get; }

    public abstract void RequestOpen();
    public abstract void OnEnter();
    /// <summary>
    /// 暂停界面
    /// </summary>
    public abstract void OnPause();
    /// <summary>
    /// 唤醒界面
    /// </summary>
    public abstract void OnResume();
    public abstract void OnExit();


}
