using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//所有敌人的父类
public class Enemy : Individual
{
    protected Individual Aim;//攻击目标

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

    protected void FindAim()
    {
        int mindis = 9999;
        Individual mini = null;
        foreach(var actor in IndividualManager.ReturnAllActors())
        {
            int dis = actor.InTile_.MaxDis(InTile);
            if(dis < mindis)
            {
                mindis = dis;
                mini = actor;
            }
        }
        if(mini != null)
        {
            Aim = mini;
        }
    }
}