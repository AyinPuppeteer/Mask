using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//所有技能的父类
public class Skill
{
    protected string Name;
    public string Name_ => Name;

    protected string Description;//描述
    public string Description_ => Description;

    protected bool IsChoosing;

    protected float CoolTime;//冷却时间
    private float CoolTimer;//冷却计时器

    public Skill()
    {
        SkillInit();
    }
    public void SkillInit()
    {

    }

    #region 选择相关脚本
    //选择时
    public void WhenChoose()
    {

    }
    //选择中
    protected void ChoosingUpdate()
    {

    }
    #endregion

    #region 释放相关脚本
    //释放时
    protected void WhenUse()
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