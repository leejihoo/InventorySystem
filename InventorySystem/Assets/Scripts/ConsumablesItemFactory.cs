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

    public override void CreatePopup(BaseItem baseItem, GameObject popup, Stack<GameObject> popupStack)
    {
        var instance = Instantiate(popup, GameObject.Find("Canvas").transform);
        popupStack.Push(instance);
        instance.GetComponent<PopupChildrenContainer>().itemImage.sprite = baseItem.Sprite;
        instance.GetComponent<PopupChildrenContainer>().itemName.text = baseItem.BaseItemModel.Name;
        instance.GetComponent<PopupChildrenContainer>().itemType.text = PopupManager.Instance.LocalizeTypeText(baseItem.BaseItemModel.Type);
        instance.GetComponent<PopupChildrenContainer>().itemDescription.text = baseItem.BaseItemModel.Description;

        instance.GetComponent<PopupChildrenContainer>().itemEffect.text = "";
        foreach (var property in typeof(Effect).GetProperties())
        {
            var value = (int)property.GetValue(((EffectableItemModel)baseItem.BaseItemModel).Effect);
            // if (value == 0)
            // {
            //     continue;
            // }

            instance.GetComponent<PopupChildrenContainer>().itemEffect.text += PopupManager.Instance.LocalizeEffectText(property.Name) + " +" + value + "\n";
        }
    }
}
