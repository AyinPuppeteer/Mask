using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private LevelPack LevelNow;

    private int Turn;//当前回合数
    private BattlePhase Phase = BattlePhase.执行;//当前状态

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
        SetLevelPack(LevelManager.ReturnPack());
        TileManager.Instance.GenerateMap(LevelNow.MapPack_);//生成土地
        CreateIndividualWhenSatrt();
    }

    //战斗开始时生成单位
    private void CreateIndividualWhenSatrt()
    {
        foreach(var pair in LevelNow.IndividualNames_)
        {
            Tile tile = TileManager.Instance.GetTile(pair.Key.Item1, pair.Key.Item2);
            string name = pair.Value;
            IndividualManager.Instance.CreateIndividual(name, tile);//生成单位
        }
    }

    #region 有关选择与交互的脚本
    public void ChooseTile(Tile tile)
    {
        if(Phase == BattlePhase.执行)
        {
            if(ChoosingActor != null)
            {
                //移动
                ChoosingActor.MoveTo(tile);
                ChoosingActor = null;
            }
            else
            {
                foreach (var indi in tile.Individuals_)
                {
                    if (indi != null && indi is Actor actor && !actor.Acting_)
                    {
                        ChooseActor(actor);//更新战斗管理器中的选中角色
                    }
                }
            }
        }
    }
    public void ChooseActor(Actor actor)
    {
        ChoosingActor = actor;
        //选中高亮
    }
    #endregion

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