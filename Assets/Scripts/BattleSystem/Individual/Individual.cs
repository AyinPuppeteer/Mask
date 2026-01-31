using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//单位的共同父类
public class Individual : MonoBehaviour
{
    protected string Name;//名字
    public string Name_ { get => Name; }

    protected Career Career;//职业
    public Career Career_ { get => Career; set => Career = value; }

    private SpriteRenderer Image;

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

    public int MainValue
    {
        get
        {
            if(Career == Career.战士 || Career == Career.游侠)
            {
                return Strength;
            }
            else if(Career == Career.法师 || Career == Career.牧师)
            {
                return Intelligence;
            }
            return 0;
        }
    }

    #region 生命值
    protected int Health;
    public int Health_ { get => Health; }
    protected int MaxHealth;
    public int MaxHealth_ { get => MaxHealth; }

    protected int Shield;
    public int Shield_ { get => Shield; }
    #endregion

    #region 魔力值
    private int Mana;//魔力值
    public int Mana_ => Mana;
    protected int MaxMana;//最大魔力值
    public int MaxMana_ => MaxMana;

    protected float ManaRate = 0.5f;//魔力恢复速率
    protected float ManaTimer;//魔力回复计时器

    public void UseMana(int cost)
    {
        Mana -= cost;
    }

    public void GainMana(int mana)
    {
        Mana = Mathf.Min(Mana + mana, MaxMana);
    }

    public void NatrualMana(float time)
    {
        ManaTimer += time;
        while(ManaTimer > ManaRate)
        {
            ManaTimer -= ManaRate;
            GainMana(1);
        }
    }
    #endregion

    #region 灵巧
    public float Dexterity => InitialDexterity * DexterityPercent + DexterityBonus;
    protected float InitialDexterity;
    protected float DexterityPercent = 1f;
    protected float DexterityBonus;
    #endregion

    //技能列表
    protected List<Skill> SkillList = new();
    public List<Skill> SkillList_ => SkillList;

    protected Tile InTile;//所处的格子
    public Tile InTile_ => InTile;
    public int Row => InTile.Row_;
    public int Column => InTile.Column_;
    public void SetTile(Tile tile) => InTile = tile;

    #region 基本Buff
    #region 冰冻
    protected float FrozenTimer;//冰冻计时器
    public bool IsFrozen => FrozenTimer > 0;
    public void SetFrozen(float time)
    {
        FrozenTimer = Mathf.Max(FrozenTimer, time);
    }
    #endregion
    #endregion
    #region Buff管理
    public List<Buff> BuffList = new();

    public Buff FindBuff(string name)
    {
        foreach(var buff in BuffList)
        {
            if(buff.Name_ == name) return buff;
        }
        return null;
    }

    public void AddBuff(Buff buff)
    {
        Buff oldbuff = FindBuff(buff.Name_);
        if (oldbuff == null)
        {
            buff.AttachTo(this);
            BuffList.Add(buff);
        }
        else
        {
            oldbuff.Addition(buff);
        }
    }

    public void RemoveBuff(Buff buff)
    {
        if (BuffList.Contains(buff)) BuffList.Remove(buff);
        else Debug.LogError($"试图删除某个{buff.Name_}，但其不存在！");
    }

    protected void BuffUpdate(float time)
    {
        foreach(var buff in BuffList)
        {
            buff.UpdateByTime(time);
        }
        
        BuffList = BuffList.Where(buff => !buff.DelTag_).ToList();
    }
    #endregion

    private bool Acting;//是否处于行动状态
    public bool Acting_ { get => Acting; set => Acting = value; }
    public bool Controlable => !Acting && !IsFrozen;

    public Individual()
    {
        IndividualInit();
    }
    protected virtual void IndividualInit()
    {

    }

    private void Awake()
    {
        Image = GetComponent<SpriteRenderer>();
        foreach (var skill in SkillList)
        {
            skill.SetPlayer(this);
        }
    }

    private void Start()
    {
        Health = MaxHealth;
        Mana = MaxMana;
        Instantiate(BattleManager.Instance.HealthBarOb, transform.position, Quaternion.identity, transform).GetComponent<HealthBar>().SetIndividual(this);
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

    //根据传入的时间差更新
    public void TimeFresh(float time)
    {
        FrozenTimer = Mathf.Max(FrozenTimer - time, 0);
        BuffUpdate(time);
        NatrualMana(time);
        foreach(var skill in SkillList)
        {
            skill.Refresh(time);
        }
    }

    #region 伤害相关
    public bool AimJudge(Individual another)
    {
        if(this is Actor)
        {
            return another is Enemy;
        }
        else if(this is Enemy)
        {
            return another is Actor;
        }
        return false;
    }
    /// <summary>
    /// 伤害实施（返回实际受伤量）
    /// </summary>
    public int Hurt(float damage)
    {
        if (Shield > 0)
        {
            if(Shield >= damage)
            {
                Shield -= (int)damage;
                damage = 0;
                BattleManager.Instance.TextJump(transform.position, "完全防御", Color.blue);
            }
            else
            {
                damage -= Shield;
                Shield = 0;
            }

        }
        Health = Math.Max(Health - (int)damage, 0);
        if (Health <= 0)
        {
            DeadSolve();
        }
        BattleManager.Instance.TextJump(transform.position, ((int)damage).ToString(), Color.red);
        return (int)damage;
    }

    /// <summary>
    /// 攻击（返回实际伤害量）
    /// </summary>
    public int Attack(Individual another, float attack = -1)
    {
        if(attack == -1)
        {
            attack = Strength;
        }
        return another.Hurt(attack);
    }
    #endregion
    #region 恢复
    public void Heal(int heal)
    {
        Health += heal;
        Health = Mathf.Min(Health, MaxHealth);
        BattleManager.Instance.TextJump(transform.position, heal.ToString(), Color.green);
    }
    #endregion

    #region 死亡处理
    //死亡处理
    public virtual void DeadSolve()
    {
        Destroy(gameObject);
    }
    #endregion

    #region 管理移动
    public virtual Tween MoveTo(Tile tile, float speed = 1f)
    {
        var tween = transform.DOMove(tile.transform.position, tile.Distance(this) / speed);
        tween.OnUpdate(() =>
        {
            Tile newtile = TileManager.Instance.GetTile(transform.position);
            InTile.Individuals_.Remove(this);
            InTile = newtile;
            transform.parent = InTile.transform;
            InTile.Individuals_.Add(this);
        });
        return tween;
    }
    #endregion

    public void SetMat(Material basemat)
    {
        Image.material = new(basemat);
    }
}

public enum Career
{
    战士, 游侠, 法师, 牧师
}