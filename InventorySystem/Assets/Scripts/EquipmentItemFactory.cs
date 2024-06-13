using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentItemFactory : ItemFactory
{
    public override void CreateItem(JArray itemList, GameObject slot, List<GameObject> inventoryList, Transform parent)
    {
        foreach (var jToken in itemList)
        {
            var newSlot = Instantiate(slot, parent);
            var item = ItemDatabase.ItemDictionary[jToken.ToString()];
            item.Sprite = InsertImage(item.BaseItemModel.Id);
            
            newSlot.GetComponent<ItemContainer>().BaseItem = item;
            newSlot.GetComponentsInChildren<Image>()[1].sprite = item.Sprite;
            inventoryList.Add(newSlot);
        }
    }
}
