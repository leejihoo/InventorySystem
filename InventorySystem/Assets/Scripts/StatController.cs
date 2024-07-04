using TMPro;
using UnityEngine;

public class StatController : Singleton<StatController>
{
    [SerializeField] private Effect charaterStat;
    [SerializeField] private TMP_Text strengthText;
    [SerializeField] private TMP_Text luckText;
    [SerializeField] private TMP_Text agilityText;
    [SerializeField] private TMP_Text intellectText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text mpText;
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text defenseText;
    
    public void Wear(Effect effect)
    {
        foreach (var stat in typeof(Effect).GetProperties())
        {
            var value = (int)stat.GetValue(effect);
            var currentStat = (int)stat.GetValue(charaterStat);
            stat.SetValue(charaterStat,currentStat+value);
        }

        UpdateText();
    }
    
    public void UnWear(Effect effect)
    {
        foreach (var stat in typeof(Effect).GetProperties())
        {
            var value = (int)stat.GetValue(effect);
            var currentStat = (int)stat.GetValue(charaterStat);
            stat.SetValue(charaterStat,currentStat-value);
        }

        UpdateText();
    }
    
    // wear과 동일하지만 따로 둔다. 왜냐하면 소비 아이템 중에는 시간제한이 있는 아이템도 존재하여 유지보수적 측면에서 나누는게 좋다고 판단 
    public void Use(Effect effect)
    {
        foreach (var stat in typeof(Effect).GetProperties())
        {
            var value = (int)stat.GetValue(effect);
            var currentStat = (int)stat.GetValue(charaterStat);
            stat.SetValue(charaterStat,currentStat+value);
        }

        UpdateText();
    }

    private void UpdateText()
    {
        strengthText.text = charaterStat.Strength.ToString();
        luckText.text = charaterStat.Luck.ToString();
        agilityText.text = charaterStat.Agility.ToString();
        intellectText.text = charaterStat.Intellect.ToString();
        hpText.text = charaterStat.Hp.ToString();
        mpText.text = charaterStat.Mp.ToString();
        attackText.text = charaterStat.Attack.ToString();
        defenseText.text = charaterStat.Defense.ToString();
    }
}
