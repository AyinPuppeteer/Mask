using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//價插馴僻
public class Ridicule : Skill
{
    protected override void SkillInit()
    {
        Name = "陸當";
        CoolTime = 12f;
    }

    protected override void WhenChoose()
    {
        base.WhenChoose();
        TileManager.Instance.TileChooseSquare(Player.Row - 5, Player.Column - 5, 11, 11);
    }

    public override bool JudgeTile(Tile tile)
    {
        if (!base.JudgeTile(tile)) return false;
        if (!TileManager.Instance.RangeJudge(tile, Player.Row - 5, Player.Column - 5, 11, 11)) return false;
        return true;
    }

    protected override void WhenUse(Tile tile)
    {
        base.WhenUse(tile);
        foreach(var t in TileManager.Instance.ReturnTiles(Player.Row - 5, Player.Column - 5, 11, 11))
        {
            foreach (var indi in t.Individuals_)
            {
                if (Player.AimJudge(indi))
                {
                    //囥樓陸當虴彆
                }
            }
        }
    }
}