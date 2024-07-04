using TMPro;
using UnityEngine.Localization.Settings;

public class LocalizationManager : Singleton<LocalizationManager>
{
    public LocalizedStringDatabase local;
    public LocalizationSettings localSetting;
    
    // Start is called before the first frame update
    void Start()
    {
        localSetting.SetStringDatabase(local);
    }

    public void ChangeNation(TMP_Dropdown dropdown)
    {
        // dropdown 순서와 localize setting의 available Local의 순서가 일치하도록 해놓음. 
        localSetting.SetSelectedLocale(localSetting.GetAvailableLocales().Locales[dropdown.value]);
    }

    
    public string LocalizeEffectText(string property)
    {
        return localSetting.GetStringDatabase().GetLocalizedString(property,localSetting.GetSelectedLocale());
    }
    
    public string LocalizeTypeText(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Consumables:
                return "소비";
            case ItemType.Equipment:
                return "장비";
            case ItemType.Misc:
                return "기타";
            default:
                return "";
        }
    }
}
