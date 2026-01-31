using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraineeWarrior : Actor
{
    protected override void ActorInit()
    {
        base.ActorInit();
        Name = "见习战士";
        Career = Career.战士;
        InitialStrength = 30;
        InitialIntelligence = 5;
        MaxHealth = 180;
        MaxMana = 40;
        InitialDexterity = 20;
        Shield = 40;

        SkillList.Add(new BasicAttack(0.25f, 1));
    }
}