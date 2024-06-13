using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsumablesItemFactory : ItemFactory
{
    public override void CreateItem(JArray itemList, GameObject slot, List<GameObject> inventoryList, Transform parent)
    {
        foreach (var jToken in itemList)
        {
            var newSlot = Instantiate(slot, parent);
            ConsumablesItem consumablesItem = ItemDatabase.ItemDictionary[jToken["id"].ToString()] as ConsumablesItem;
            consumablesItem.Count = int.Parse(jToken["count"].ToString());
            consumablesItem.Sprite = InsertImage(consumablesItem.BaseItemModel.Id);
            
            newSlot.GetComponent<ItemContainer>().BaseItem = consumablesItem;
            newSlot.GetComponentInChildren<TMP_Text>().text = consumablesItem.Count.ToString();
            newSlot.GetComponentsInChildren<Image>()[1].sprite = consumablesItem.Sprite;
            inventoryList.Add(newSlot);
        }
    }
}
