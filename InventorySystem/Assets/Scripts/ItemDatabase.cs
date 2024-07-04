using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class ItemDatabase : MonoBehaviour
{
    public static Dictionary<string, BaseItem> ItemDictionary;
    private const string EquipmentItemJsonPath = "Assets/Json/EquipmentItem.json";
    private const string ConsumablesItemJsonPath = "Assets/Json/ConsumablesItem.json";
    private const string MiscItemJsonPath = "Assets/Json/MiscItem.json";
    
    private void Awake()
    {
        ItemDictionary = new Dictionary<string, BaseItem>();
        ConvertEffectableItemJsonToList(EquipmentItemJsonPath,ItemType.Equipment);
        ConvertEffectableItemJsonToList(ConsumablesItemJsonPath, ItemType.Consumables);
        ConvertBaseItemJsonToList(MiscItemJsonPath, ItemType.Misc);
    }

    private void ConvertEffectableItemJsonToList(string filePath, ItemType itemType)
    {
        var json = File.ReadAllText(filePath);
        
        var result = JsonConvert.DeserializeObject<List<EffectableItemModel>>(json);

        foreach (var value in result)
        {
            if (itemType == ItemType.Equipment)
            {
                ItemDictionary.Add(value.Id,new EquipmentItem(value));
            }
            else if(itemType == ItemType.Consumables)
            {
                ItemDictionary.Add(value.Id,new ConsumablesItem(value));
            }
        }
        
    }

    private void ConvertBaseItemJsonToList(string filePath, ItemType itemType)
    {
        var json = File.ReadAllText(filePath);
        
        var result = JsonConvert.DeserializeObject<List<BaseItemModel>>(json);

        foreach (var value in result)
        {
            if (itemType == ItemType.Misc)
            {
                ItemDictionary.Add(value.Id,new MiscItem(value));
            }
        }
    }

}
