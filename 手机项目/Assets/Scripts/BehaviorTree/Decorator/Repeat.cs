using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeat : Decorator
{
    private int conunter;//当前重复次数
    private int limit;//重复限度
    public Repeat(int limit)
    {
        this.limit = limit;
    }
    protected override void OnInitialize()
    {
        conunter = 0;//进入时，将次数清零
    }
    protected override EStatus OnUpdate()
    {
        while (true)
        {
            _child.Tick();
            if (_child._isRunning) return EStatus.Running;
            if (_child._isFailure) return EStatus.Failure;
            //子节点执行成功，就增加一次计算，达到设定限度才返回成功
            if (++conunter >= limit) return EStatus.Success;
        }
    }
}
