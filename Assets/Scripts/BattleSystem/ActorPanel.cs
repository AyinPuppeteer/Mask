using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//角色面板（显示角色数值和技能的面板）
public class ActorPanel : MonoBehaviour
{
    public static ActorPanel Instance;

    public Actor Actor => BattleManager.Instance.ChoosingActor_;

    /*
    [SerializeField]
    private TextMeshProUGUI HealthText;
    [SerializeField]
    private TextMeshProUGUI ManaText;
    [SerializeField]
    private TextMeshProUGUI StrengthText;
    [SerializeField]
    private TextMeshProUGUI IntelligenceText;
    */

    //开始战斗按钮
    [SerializeField]
    private Button StartButton;

    [SerializeField]
    private Button Skill1, Skill2, Skill3;
    [SerializeField]
    private Button BasicAttack;
    [SerializeField]
    private TextMeshProUGUI SkillName1, SkillName2, SkillName3;
    [SerializeField]
    private TextMeshProUGUI BasicAttackName;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (BattleManager.Instance.Phase_ == BattlePhase.分析)
        {
            StartButton.interactable = true;
        }
        else
        {
            StartButton.interactable = false;
        }

        BasicAttack.gameObject.SetActive(false);
        Skill1.gameObject.SetActive(false);
        Skill2.gameObject.SetActive(false);
        Skill3.gameObject.SetActive(false);

        if (Actor != null)
        {
            /*
            StrengthText.text = Actor.Strength.ToString();
            HealthText.text = Actor.Health_ + " / " + Actor.MaxHealth_;
            ManaText.text = Actor.Mana_ + " / " + Actor.MaxMana_;
            IntelligenceText.text = Actor.Intelligence.ToString();
            */

            if(Actor.SkillList_.Count >= 1)
            {
                BasicAttackName.text = Actor.SkillList_[0].Name_;
                BasicAttack.gameObject.SetActive(true);
            }
            if(Actor.SkillList_.Count >= 2)
            {
                SkillName1.text = Actor.SkillList_[1].Name_;
                Skill1.gameObject.SetActive(true);
            }
            if (Actor.SkillList_.Count >= 3)
            {
                SkillName2.text = Actor.SkillList_[2].Name_;
                Skill2.gameObject.SetActive(true);
            }
            if (Actor.SkillList_.Count >= 4)
            {
                SkillName3.text = Actor.SkillList_[3].Name_;
                Skill3.gameObject.SetActive(true);
            }
        }
    }

    private void Shift(int dir)
    {
        Tile tile = Actor.InTile_;
        List<Actor> Actors = tile.Individuals_.Where(indi => indi is Actor).Select(indi => indi as Actor).ToList();
        if (Actors.Count > 1)
        {
            int index = Actors.IndexOf(Actor);
            index += dir;
            if (index < 0) index = Actors.Count - 1;
            else if (index >= Actors.Count) index = 0;
            BattleManager.Instance.ChooseActor(Actors[index]);
        }
    }

    //选择第t个技能
    public void ChooseSkill(int t)
    {
        BattleManager.Instance.ChooseSkill(Actor.SkillList_[t]);
    }
}