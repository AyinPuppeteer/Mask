using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//所有技能的父类
public class Skill
{
    protected string Name;
    public string Name_ => Name;

    protected string Description = "";//描述
    public string Description_ => Description;

    protected bool IsChoosing;

    protected float CoolTime;//冷却时间
    protected float CoolTimer;//冷却计时器
    public bool Ready => CoolTimer <= 0;
    public float CoolPercent => CoolTime == 0 ? 0 : CoolTimer / CoolTime;

    protected int ManaCost;//蓝耗
    public int ManaCost_ => ManaCost;

    protected Individual Player;

    public Skill()
    {
        SkillInit();
        CoolTimer = CoolTime;
    }
    protected virtual void SkillInit()
    {

    }

    public void SetPlayer(Individual player)
    {
        Player = player;
    }

    public void Refresh(float deltatime)
    {
        if (!Ready)
        {
            CoolTimer = Mathf.Max(CoolTimer - deltatime, 0);
        }
    }

    #region 选择相关脚本
    //选择时
    public void Choose()
    {
        
    }
    protected virtual void WhenChoose()
    {

    }

    //选择中
    protected void ChoosingUpdate()
    {
        if (Player.Mana_ < ManaCost)
        {
            BattleManager.Instance.CancelChooseSkill();
            return;
        }
    }

    public virtual bool JudgeTile(Tile tile)
    {
        return false;
    }
    #endregion

    #region 释放相关脚本
    //释放时
    public void Use(Tile tile)
    {
        Player.UseMana(ManaCost);
        CoolTimer = CoolTime;
        WhenUse(tile);
    }
    protected virtual void WhenUse(Tile tile)
    {
        
    }
    #endregion

    private void Update()
    {
        if (IsChoosing)
        {
            ChoosingUpdate();
        }
    }
}