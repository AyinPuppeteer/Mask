using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//单位的共同父类
public class Individual : MonoBehaviour
{
    private string Name;//名字
    public string Name_ { get => Name; }

    public int Attack => (int)(InitialAttack * AttackPercent + AttackBonus);
    private int InitialAttack;
    private float AttackPercent = 1f;//百分比攻击力
    private int AttackBonus = 0;//额外攻击力

    private int Health;
    public int Health_ { get => Health; }
    private int MaxHealth;
    public int MaxHealth_ { get => MaxHealth; }

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
    public int Attackit(Individual another, int attack = -1)
    {
        if(attack == -1)
        {
            attack = Attack;
        }
        return another.Hurt(attack);
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