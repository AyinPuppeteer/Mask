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
    public void Highlight(bool isChoose)
    {
        if (!isChoose)
        {
            spriteRenderer.color = new Color(0, 0, 0, 0);
           
        }
        else
        {
            spriteRenderer.color = new Color(1f, 0.2f, 0.2f, 0.7f);
        }
    }
}