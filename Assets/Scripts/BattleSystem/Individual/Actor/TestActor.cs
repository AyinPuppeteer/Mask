using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestActor : Actor
{
    protected override void ActorInit()
    {
        base.ActorInit();
        Name = "²âÊÔ½ÇÉ«";
        MaxHealth = 100;
    }

    protected override void IndividualStart()
    {
        base.IndividualStart();
        Buff buff = new Burn(this, 10, 5f);
        AddBuff(buff);
    }
}