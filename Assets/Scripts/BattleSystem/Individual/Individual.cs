using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//单位的共同父类
public class Individual : MonoBehaviour
{
    private string Name;//名字
    public string Name_ { get => Name; }

    private Career Career;//职业
    public Career Career_ { get => Career; set => Career = value; }

    #region 力量
    public int Strength => (int)(InitialStrength * StrengthPercent + StrengthBonus);
    protected int InitialStrength;
    protected float StrengthPercent = 1f;//百分比攻击力
    protected int StrengthBonus = 0;//额外攻击力
    #endregion

    #region 智力
    public int Intelligence => (int)(InitialIntelligence * IntelligencePercent + IntelligenceBonus);
    protected int InitialIntelligence;
    protected float IntelligencePercent = 1f;
    protected float IntelligenceBonus = 0;
    #endregion

    #region 生命值
    protected int Health;
    public int Health_ { get => Health; }
    protected int MaxHealth;
    public int MaxHealth_ { get => MaxHealth; }

    public int Shield;
    public int Shield_ { get => Shield; }
    #endregion

    #region 魔力值
    private int Mana;//魔力值
    public int Mana_ => Mana;
    protected int MaxMana;//最大魔力值
    public int MaxMana_ => MaxMana;
    #endregion

    #region 灵巧
    public float Dexterity => InitialDexterity * DexterityPercent + DexterityBonus;
    protected float InitialDexterity;
    protected float DexterityPercent = 1f;
    protected float DexterityBonus;
    #endregion

    //技能列表
    protected List<Skill> SkillList = new();

    public Individual()
    {
        IndividualInit();
    }
    protected virtual void IndividualInit()
    {

    }

    private void Start()
    {
        IndividualStart();
    }
    protected virtual void IndividualStart()
    {

    }

    private void Update()
    {
        IndividualUpdate();
    }
    protected virtual void IndividualUpdate()
    {

    }

    #region 伤害相关
    /// <summary>
    /// 伤害实施（返回实际受伤量）
    /// </summary>
    public int Hurt(int damage)
    {
        if (Shield > 0)
        {
            if(Shield >= damage)
            {
                Shield -= damage;
                damage = 0;
            }
            else
            {
                damage -= Shield;
                Shield = 0;
            }
        }
        Health -= damage;
        if(Health <= 0)
        {
            DeadSolve();
        }
        return damage;
    }

    /// <summary>
    /// 攻击（返回实际伤害量）
    /// </summary>
    public int Attack(Individual another, int strength = -1)
    {
        if(strength == -1)
        {
            strength = Strength;
        }
        return another.Hurt(strength);
    }
    #endregion
    #region 恢复
    public void Heal(int heal)
    {
        Health += heal;
        Health = Mathf.Min(Health, MaxHealth);
    }
    #endregion

    #region 死亡处理
    //死亡处理
    private void DeadSolve()
    {

    }
    #endregion
}

public enum Career
{
    战士, 游侠, 法师, 牧师
}