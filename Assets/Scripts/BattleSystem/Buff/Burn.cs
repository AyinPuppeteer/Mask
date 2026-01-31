using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : Buff
{
    private float BurnTime = 1f;
    private float BurnTimer;//¼ÆÊ±Æ÷

    private int Attack;

    protected override void Intialize()
    {
        base.Intialize();
        Name = "×ÆÉÕ";
    }

    public Burn(Individual attacher, int attack, float time) : base()
    {
        Attacher = attacher;
        Attack = attack;
        ConTime = time;
    }

    protected override void WhenUpdate(float deltatime)
    {
        base.WhenUpdate(deltatime);
        if((BurnTimer += deltatime) >= BurnTime)
        {
            BurnTimer -= BurnTime;
            Carrier.Hurt(Attack);
        }
    }
}