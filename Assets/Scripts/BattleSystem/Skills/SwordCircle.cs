using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//»ù´¡¹¥»÷
public class SwordCircle : Skill
{
    private float AttackRate = 1.5f;//¹¥»÷±ÈÂÊ
    private int Distance = 1;//¹¥»÷·¶Î§
    private float AnimTime = 0.5f;//¶¯»­Ê±³¤

    protected override void SkillInit()
    {
        Name = "ÈÐ»·";
        Description = "Ðý·çÕ¶£¡";
    }

    public override bool JudgeTile(Tile tile)
    {
        if (!base.JudgeTile(tile)) return false;
        if (Player.InTile_.MaxDis(tile) > Distance) return false;
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
        (Player as Actor).Acting_ = true;
        BattleManager.Instance.CreateEffect(0, tile.transform.position).PlayAnim("AxeAttack");
        DOTween.To(() => 0, x => { }, 0, AnimTime).OnComplete(() =>
        {
            foreach (var t in TileManager.Instance.ReturnTiles(Player.Row - 1, Player.Column - 1, 3, 3))
            {
                foreach (var indi in tile.Individuals_)
                {
                    if (Player.AimJudge(indi))
                    {
                        Player.Attack(indi, Player.Strength * AttackRate);
                    }
                }
                Player.Acting_ = false;
            }
        });
    }
}