using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//所有角色的父类
public class Actor : Individual
{
    private bool Acting;//是否处于行动状态
    public bool Acting_ { get => Acting; set => Acting = value; }
    public bool Controlable => !Acting && !IsFrozen;

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

    #region 管理移动
    public override Tween MoveTo(Tile tile, float speed = 1f)
    {
        Acting = true;
        return base.MoveTo(tile, speed).OnComplete(() => Acting = false);
    }
    #endregion
}