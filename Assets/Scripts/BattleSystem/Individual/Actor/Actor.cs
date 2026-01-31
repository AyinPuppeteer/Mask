using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//所有角色的父类
public class Actor : Individual
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
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

    #region 管理移动
    public override void MoveTo(Tile tile, float time = 0.5f)
    {
        Acting = true;
        DOTween.To(() => 0, x => { }, 0, time).OnComplete(() => Acting = false);
        base.MoveTo(tile, time);
    }
    #endregion
}