using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//所有角色的父类
public class Actor : Individual
{
    private bool Acting;//是否处于行动状态
    public bool Acting_ { get => Acting; set => Acting = value; }
    public bool Controlable => !Acting;

    protected override void IndividualInit()
    {
        base.IndividualInit();
        ActorInit();
    }
    protected virtual void ActorInit()
    {

    }

    protected override void IndividualUpdate()
    {
        base.IndividualUpdate();
        ActorUpdate();
    }
    protected virtual void ActorUpdate()
    {

    }
}