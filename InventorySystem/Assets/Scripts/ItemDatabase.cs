using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.AddressableAssets.Build.Layout;

public class ItemDatabase : MonoBehaviour
{
    public static Dictionary<string, BaseItem> ItemDictionary = new Dictionary<string, BaseItem>();
    private string _equipmentItemJsonPath = "Assets/Json/EquipmentItem.json";
    private string _consumablesItemJsonPath = "Assets/Json/ConsumablesItem.json";
    private string _miscItemJsonPath = "Assets/Json/MiscItem.json";
    
    private void Awake()
    {
        ConvertEffectableItemJsonToList(_equipmentItemJsonPath, BaseItemModel.ItemType.Equipment);
        ConvertEffectableItemJsonToList(_consumablesItemJsonPath, BaseItemModel.ItemType.Consumables);
        ConvertBaseItemJsonToList(_miscItemJsonPath, BaseItemModel.ItemType.Misc);
    }

    private void ConvertEffectableItemJsonToList(string filePath, BaseItemModel.ItemType itemType)
    {
        var json = File.ReadAllText(filePath);
        
        var result = JsonConvert.DeserializeObject<List<EffectableItemModel>>(json);

        foreach (var value in result)
        {
            Debug.Log(JsonConvert.SerializeObject(value));
            if (itemType == BaseItemModel.ItemType.Equipment)
            {
                ItemDictionary.Add(value.Id,new EquipmentItem(value));
            }
            else if(itemType == BaseItemModel.ItemType.Consumables)
            {
                ItemDictionary.Add(value.Id,new ConsumablesItem(value));
            }
        }
        
    }

    private void ConvertBaseItemJsonToList(string filePath, BaseItemModel.ItemType itemType)
    {
        var json = File.ReadAllText(filePath);
        
        var result = JsonConvert.DeserializeObject<List<BaseItemModel>>(json);

        foreach (var value in result)
        {
            Debug.Log(JsonConvert.SerializeObject(value));
            if (itemType == BaseItemModel.ItemType.Misc)
            {
                ItemDictionary.Add(value.Id,new MiscItem(value));
            }
        }
    }

}
