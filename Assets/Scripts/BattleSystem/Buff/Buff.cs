using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

//所有Buff的父类
public class Buff
{
    protected string Name;
    public string Name_ => Name;

    protected float ConTime;//持续时间
    private float ConTimer;//持续时间计时器

    protected Individual Carrier;//携带者
    protected Individual Attacher;//施加者

    protected bool DelTag;//删除标签（true会被摧毁）
    public bool DelTag_ => DelTag;

    public Buff()
    {
        Intialize();
    }
    protected virtual void Intialize()
    {

    }
    
    //设置施加者
    public void SetAtacher(Individual indi)
    {
        Attacher = indi;
    }

    //添加到某个单位上
    public virtual void AttachTo(Individual indi)
    {
        Carrier = indi;
        ConTimer = ConTime;
        WhenAttach();
    }
    //施加时效果
    protected virtual void WhenAttach()
    {

    }

    //叠加
    public virtual void Addition(Buff another)
    {
        ConTime = Mathf.Max(ConTime, another.ConTime);
        ConTimer = Mathf.Max(ConTimer, another.ConTime);
    }

    //按照时间刷新
    public void UpdateByTime(float deltatime)
    {
        WhenUpdate(deltatime);
        if ((ConTimer -= deltatime) <= 0)
        {
            Dissolve();
            DelTag = true;
            return;
        }
    }
    //更新时效果
    protected virtual void WhenUpdate(float deltatime)
    {

    }

    //解除
    public void Dissolve()
    {
        WhenDissolve();
    }
    //接触时效果
    protected virtual void WhenDissolve()
    {

    }
}