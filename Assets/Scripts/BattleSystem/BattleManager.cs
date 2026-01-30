using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private LevelPack LevelNow = new();

    private int Turn;//当前回合数
    private BattlePhase Phase = BattlePhase.分析;//当前状态

    private Actor ChoosingActor;//选中的角色
    public Actor ChoosingActor_ => ChoosingActor;

    public static BattleManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public static void SetLevelPack(LevelPack pack)
    {
        Instance.LevelNow = pack;
    }

    private void Start()
    {
        //SolidManager生成土地
        CreateIndividualWhenSatrt();
    }

    //战斗开始时生成单位
    private void CreateIndividualWhenSatrt()
    {
        foreach(var pair in LevelNow.IndividualNames_)
        {
            //获取格子
            string name = pair.Value;
            IndividualManager.Instance.CreateIndividual(name);//生成单位
        }
    }

    public void ChooseActor(Actor actor)
    {
        ChoosingActor = actor;
        //选中高亮
    }

    //判断胜利失败条件
    private void CheckGoal()
    {
        if(IndividualManager.ReturnAllEnemys().Count > 0)
        {
            //胜利
            return;
        }
        else if(IndividualManager.ReturnAllActors().Count > 0)
        {
            //失败
            return;
        }
    }
}

public enum BattlePhase
{
    分析, 执行, 敌人行动
}