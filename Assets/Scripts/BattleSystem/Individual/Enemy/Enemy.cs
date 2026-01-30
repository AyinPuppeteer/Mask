using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//所有敌人的父类
public class Enemy : Individual
{
    protected override void IndividualInit()
    {
        base.IndividualInit();
        EnemyInit();
    }
    protected virtual void EnemyInit()
    {

    }

    protected override void IndividualUpdate()
    {
        base.IndividualUpdate();
        EnemyUpdate();
    }
    protected virtual void EnemyUpdate()
    {

    }
}