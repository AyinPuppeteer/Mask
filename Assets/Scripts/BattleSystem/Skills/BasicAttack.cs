using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//»ù´¡¹¥»÷
public class BasicAttack : Skill
{
    private float AttackRate;//¹¥»÷±ÈÂÊ
    private int Distance;//¹¥»÷·¶Î§
    private float AnimTime;//¶¯»­Ê±¼ä

    public BasicAttack(float rate, int distance, float animtime) : base()
    {
        AttackRate = rate;
        Distance = distance;
        AnimTime = animtime;
    }

    protected override void SkillInit()
    {
        Name = "»ù´¡¹¥»÷";
        Description = "¹¥»÷·¶Î§ÄÚµÄ1ÃûµÐÈË¡£";
    }

    public override bool JudgeTile(Tile tile)
    {
        if (!base.JudgeTile(tile)) return false;
        if (!TileManager.Instance.RangeJudge(tile, Player.Row - Distance, Player.Column - Distance, Distance * 2 + 1, Distance * 2 + 1)) return false;
        foreach(var indi in tile.Individuals_)
        {
            if (Player.AimJudge(indi))
            {
                return true;
            }
        }
        return false;
    }

    protected override void WhenUse(Tile tile)
    {
        base.WhenUse(tile);
        BattleManager.Instance.CreateEffect(0, tile.transform.position);
        DOTween.To(() => 0, x => { }, 0, AnimTime).OnComplete(() =>
        {
            foreach (var indi in tile.Individuals_)
            {
                if (Player.AimJudge(indi))
                {
                    Player.Attack(indi, Player.Strength * AttackRate);
                }
            }
        });
    }
}