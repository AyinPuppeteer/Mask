using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    private LevelPack LevelNow;

    private int Turn;//当前回合数
    private BattlePhase Phase = BattlePhase.执行;//当前阶段
    public BattlePhase Phase_ => Phase;
    private float PhaseTimer;//当前阶段的剩余时间
    private float PhaseTime = 5f;//一个阶段的时间
    [SerializeField]
    private TextMeshProUGUI PanelTimer;//面板计时器
    [SerializeField]
    private Image PhaseIcon;
    [SerializeField]
    private List<Sprite> PhaseIcons = new();

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
        Phase = BattlePhase.分析;
        PhaseTimer = PhaseTime;
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
        PanelTimer.text = $"{Mathf.FloorToInt(PhaseTimer):D2} : {Mathf.FloorToInt((PhaseTimer - Mathf.FloorToInt(PhaseTimer)) * 60):D2}";
        if (Phase == BattlePhase.分析 || Phase == BattlePhase.执行)
        {
            PhaseIcon.sprite = PhaseIcons[0];
        }
        else
        {
            PhaseIcon.sprite = PhaseIcons[1];
        }

        if (Phase == BattlePhase.执行 || Phase == BattlePhase.敌人行动)
        {
            foreach (var indi in IndividualManager.ReturnAllIndividuals())
            {
                indi.TimeFresh(Time.deltaTime);
            }

            if ((PhaseTimer -= Time.deltaTime) <= 0)
            {
                switch (Phase)
                {
                    case BattlePhase.执行:
                        {
                            Phase = BattlePhase.敌人行动;
                            PhaseTimer = PhaseTime;
                            if (ChoosingActor != null) CancelChooseActor();
                            if (ChoosingSkill != null) CancelChooseSkill();
                            break;
                        }
                    case BattlePhase.敌人行动:
                        {
                            Phase = BattlePhase.分析;
                            PhaseTimer = PhaseTime;
                            break;
                        }
                }
            }
        }
    }

    public void StartBattle()
    {
        Phase = BattlePhase.执行;
        PhaseTimer = PhaseTime;
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
                }
                CancelChooseSkill();
            }
            else if(ChoosingActor != null)
            {
                if(ChoosingActor.InTile_.MaxDis(tile) == 1)
                {
                    //移动
                    ChoosingActor.MoveTo(tile, 1 + ChoosingActor.Dexterity * 0.01f);
                }
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
        actor.SetMat(IndividualManager.Instance.HighLightMat_);
        TileManager.Instance.TileChooseSquare(actor.Row - 1, actor.Column - 1, 3, 3);
    }
    public void CancelChooseActor()
    {
        ChoosingActor.SetMat(IndividualManager.Instance.NormalMat_);
        ChoosingActor = null;
        TileManager.Instance.CancelHighlight();
    }

    public void ChooseSkill(Skill skill)
    {
        ChoosingSkill = skill;
    }
    public void CancelChooseSkill()
    {
        ChoosingSkill = null;
        TileManager.Instance.CancelHighlight();
    }
    #endregion

    //判断胜利失败条件
    private void CheckGoal()
    {
        if(IndividualManager.ReturnAllEnemys().Length > 0)
        {
            GameWin();
            return;
        }
        else if(IndividualManager.ReturnAllActors().Length > 0)
        {
            GameLose();
            return;
        }
    }

    public void GameWin()
    {

    }
    public void GameLose()
    {

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