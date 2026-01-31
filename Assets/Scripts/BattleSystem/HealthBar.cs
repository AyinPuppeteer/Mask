using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//管理血条（和蓝条）
public class HealthBar : MonoBehaviour
{
    private Individual Carrier;

    [SerializeField]
    private Image HealthImg, ManaImg;
    [SerializeField]
    private TextMeshProUGUI HealthText, ManaText;

    public void SetIndividual(Individual individual)
    {
        Carrier = individual;
    }

    private void Update()
    {
        HealthImg.fillAmount = Carrier.Health_ / Carrier.MaxHealth_;
        ManaImg.fillAmount = Carrier.MaxMana_ == 0 ? 0 : Carrier.Mana_ / Carrier.MaxMana_;
        HealthText.text = $"{Carrier.Health_}/{Carrier.MaxHealth_}";
        ManaText.text = Carrier.MaxMana_ == 0 ? "" : $"{Carrier.Mana_}/{Carrier.MaxMana_}";
    }
}