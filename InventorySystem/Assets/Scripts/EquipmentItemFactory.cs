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

    public override void CreatePopup(BaseItem baseItem, GameObject popup, Stack<GameObject> popupStack)
    {
        var instance = Instantiate(popup, GameObject.Find("Canvas").transform);
        popupStack.Push(instance);
        instance.GetComponent<PopupChildrenContainer>().itemImage.sprite = baseItem.Sprite;
        instance.GetComponent<PopupChildrenContainer>().itemName.text = baseItem.BaseItemModel.Name;
        instance.GetComponent<PopupChildrenContainer>().itemType.text = PopupManager.Instance.LocalizeTypeText(baseItem.BaseItemModel.Type);
        instance.GetComponent<PopupChildrenContainer>().itemDescription.text = baseItem.BaseItemModel.Description;

        foreach (var property in typeof(Effect).GetProperties())
        {
            var value = (int)property.GetValue(((EffectableItemModel)baseItem.BaseItemModel).Effect);
            if (value == 0)
            {
                continue;
            }

            instance.GetComponent<PopupChildrenContainer>().itemEffect.text = PopupManager.Instance.LocalizeEffectText(property.Name) + " +" + value + "\n";
        }
    }
}
