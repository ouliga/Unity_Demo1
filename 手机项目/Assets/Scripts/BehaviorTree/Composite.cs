using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Composite : Behavior
{
    protected LinkedList<Behavior> _children;//用双向链表构建子节点列表
    public Composite()
    {
        _children = new LinkedList<Behavior>();
    }
    //移除指定子节点
    public virtual void RemoveChild(Behavior child)
    {
        _children.Remove(child);
    }
    public void ClearChildren()//清空子节点列表
    {
        _children.Clear();
    }
    public override void AddChild(Behavior child)//添加子节点
    {
        //默认是尾插入，如：0插入「1，2，3」中，就会变成「1，2，3，0」
        _children.AddLast(child);
    }
}
