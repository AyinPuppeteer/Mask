using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    protected override void EnemyInit()
    {
        base.EnemyInit();
        Name = "÷¼÷Ã";
        MaxHealth = 100;
        InitialDexterity = 20;
    }

    protected override void EnemyUpdate()
    {
        base.EnemyUpdate();
        if (Aim == null)
        {
            FindAim();
        }
        if (Controlable)
        {
            if(Aim != null)
            {
                if(Aim.InTile_.MaxDis(InTile) <= 1)
                {
                    Attack(Aim);
                }
                else
                {
                    int deltax = Aim.InTile_.Row_ - Row;
                    int deltay = Aim.InTile_.Column_ - Column;
                    if(deltax >= deltay)
                    {
                        MoveTo(TileManager.Instance.GetTile(Row + Math.Sign(deltax), Column));
                    }
                    else if(deltax < deltay)
                    {
                        MoveTo(TileManager.Instance.GetTile(Row, Column + Math.Sign(deltay)));
                    }
                }
            }
        }
    }

    private void Attack(Individual another)
    {

    }
}