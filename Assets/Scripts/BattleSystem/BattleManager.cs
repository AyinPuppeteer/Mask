using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BattleManager : MonoBehaviour
{
    private LevelPack LevelNow;

    private int Turn;//当前回合数
    private BattlePhase Phase = BattlePhase.执行;//当前阶段
    private float PhaseTimer;//当前阶段的剩余时间
    private float PhaseTime = 10f;//一个阶段的时间

    private Actor ChoosingActor;//选中的角色
    public Actor ChoosingActor_ => ChoosingActor;

    private Skill ChoosingSkill;//选中的技能
    public Skill ChoosingSkill_ => ChoosingSkill;

    [SerializeField]
    private GameObject JumpTextOb;//跳动数字物体（如伤害）

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

    private void Update()
    {
        if (Phase == BattlePhase.执行 || Phase == BattlePhase.敌人行动)
        {
            foreach(var indi in IndividualManager.ReturnAllIndividuals())
            {
                indi.TimeFresh(Time.deltaTime);
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (ChoosingSkill != null)
                {
                    ChoosingSkill = null;
                }
                else if (ChoosingActor != null)
                {
                    ChoosingActor = null;
                }
            }
        }
    }

    #region 有关选择与交互的脚本
    public void ChooseTile(Tile tile)
    {
        if (Phase == BattlePhase.执行)
        {
            if(ChoosingSkill != null)
            {
                if (ChoosingSkill.JudgeTile(tile))
                {
                    ChoosingSkill.Use(tile);
                    CancelChooseSkill();
                }
            }
            else if(ChoosingActor != null)
            {
                //移动
                ChoosingActor.MoveTo(tile, 1 + ChoosingActor.Dexterity * 0.01f);
                CancelChooseActor();
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
    public void CancelChooseActor()
    {
        ChoosingActor = null;
        //取消高亮
    }

    public void ChooseSkill(Skill skill)
    {
        ChoosingSkill = skill;
    }
    public void CancelChooseSkill()
    {
        ChoosingSkill = null;
    }
    #endregion

    //判断胜利失败条件
    private void CheckGoal()
    {
        if(IndividualManager.ReturnAllEnemys().Length > 0)
        {
            //胜利
            return;
        }
        else if(IndividualManager.ReturnAllActors().Length > 0)
        {
            //失败
            return;
        }
    }

    public void TextJump(Vector3 pos, string text, Color color)
    {
        GameObject ob = Instantiate(JumpTextOb, pos, Quaternion.identity, transform);
        JumpText jt = ob.GetComponent<JumpText>();
        jt.SetText(text, color);
    }
}

public enum BattlePhase
{
    分析, 执行, 敌人行动
}