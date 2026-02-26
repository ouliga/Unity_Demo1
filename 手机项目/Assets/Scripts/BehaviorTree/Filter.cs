using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class Filter : Sequence
{
    public void AddCondition(Behavior condition)//添加条件，就用头插入
    {
        _children.AddFirst(condition);
    }
    public void AddAction(Behavior action)//添加动作，就用尾插入
    {
        _children.AddLast(action);
    }
}
