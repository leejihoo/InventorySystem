using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class ItemDatabase : MonoBehaviour
{
    public static Dictionary<string, BaseItem> ItemDictionary;
    private string _equipmentItemJsonPath = "Assets/Json/EquipmentItem.json";
    private string _consumablesItemJsonPath = "Assets/Json/ConsumablesItem.json";
    private string _miscItemJsonPath = "Assets/Json/MiscItem.json";
    
    private void Awake()
    {
        ItemDictionary = new Dictionary<string, BaseItem>();
        ConvertEffectableItemJsonToList(_equipmentItemJsonPath,ItemType.Equipment);
        ConvertEffectableItemJsonToList(_consumablesItemJsonPath, ItemType.Consumables);
        ConvertBaseItemJsonToList(_miscItemJsonPath, ItemType.Misc);
    }

    private void ConvertEffectableItemJsonToList(string filePath, ItemType itemType)
    {
        var json = File.ReadAllText(filePath);
        
        var result = JsonConvert.DeserializeObject<List<EffectableItemModel>>(json);

        foreach (var value in result)
        {
            //Debug.Log(JsonConvert.SerializeObject(value));
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
            //Debug.Log(JsonConvert.SerializeObject(value));
            if (itemType == ItemType.Misc)
            {
                ItemDictionary.Add(value.Id,new MiscItem(value));
            }
        }
    }

}
