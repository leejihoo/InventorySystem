using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

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
