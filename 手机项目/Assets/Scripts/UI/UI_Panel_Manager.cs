using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Panel_Manager : Singleton<UI_Panel_Manager>
{
    [SerializeField] private UI_Panel_Shop _UI_Panel_Shop;
    [SerializeField] private UI_Panel_Inventory _UI_Panel_Inventory;
    [SerializeField] private UI_Panel_Difficulty _UI_Panel_Difficulty;
    [SerializeField] private UI_Panel_Battle_Over _UI_Panel_Battle_Over;
    private Stack<UI_Panel_Base> _panels = new();
    private void Awake() { }

    private void OpenPanel(UI_Panel_Base panel)
    {
        if (_panels.Count != 0)
        {
            //目标面板已打开，且在顶端显示
            if (panel == _panels.Peek())
            {
                Debug.Log("关闭相同页面");
                _panels.Pop().OnExit();
                return;
            }
            //目标面板尚未打开，且当前顶端显示面板是同层次面板对象
            else if (panel._panelLevel == _panels.Peek()._panelLevel)
            {
                _panels.Pop().OnExit();
                Debug.Log("关闭当前页面");
            }
            //目标面板尚未打开，且当前顶端显示面板是低层次面板对象
            else if (panel._panelLevel > _panels.Peek()._panelLevel)
            {
                _panels.Peek().OnPause();
                Debug.Log("暂停当前页面");
            }
            //目标面板尚未打开，且当前顶端显示面板是高层次面板对象
            else if (panel._panelLevel < _panels.Peek()._panelLevel)
            {
                return;
            }
        }
        _panels.Push(panel);
        panel.OnEnter();
    }

    public void CloseCurrentPanel()
    {
        if (_panels.Count == 0) return;

        _panels.Pop().OnExit();

        if( _panels.Count > 0)
        {
            _panels.Peek().OnResume();
        }
    }
    public void Open_Shop_Panel()
    {
        if (_UI_Panel_Shop != null)
        {
            OpenPanel( _UI_Panel_Shop );
        }
    }
    public void Open_Inventory_Panel()
    {
        if(_UI_Panel_Inventory != null)
        {
            OpenPanel(_UI_Panel_Inventory);
        }
    }
    public void Open_Difficulty_Panel()
    {
        if( _UI_Panel_Difficulty != null)
        {
            OpenPanel( _UI_Panel_Difficulty );
        }
    }
    public void Open_Battle_Over_Panel()
    {
        if(_UI_Panel_Battle_Over != null)
        {
            OpenPanel(_UI_Panel_Battle_Over);
        }
    }

}
