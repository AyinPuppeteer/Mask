using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

//角色面板（显示角色数值和技能的面板）
public class ActorPanel : MonoBehaviour
{
    public static ActorPanel Instance;

    public Actor Actor => BattleManager.Instance.ChoosingActor_;

    [SerializeField]
    private TextMeshProUGUI HealthText;
    [SerializeField]
    private TextMeshProUGUI ManaText;
    [SerializeField]
    private TextMeshProUGUI StrengthText;
    [SerializeField]
    private TextMeshProUGUI IntelligenceText;

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
        if(Actor != null)
        {
            StrengthText.text = Actor.Strength.ToString();
            HealthText.text = Actor.Health_ + " / " + Actor.MaxHealth_;
            ManaText.text = Actor.Mana_ + " / " + Actor.MaxMana_;
            IntelligenceText.text = Actor.Intelligence.ToString();
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