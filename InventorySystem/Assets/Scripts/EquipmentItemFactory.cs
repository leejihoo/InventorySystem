using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EquipmentItemFactory : ItemFactory
{
    public override void CreateItem(JArray itemList, List<GameObject> inventoryList, Transform parent)
    {
        foreach (var jToken in itemList)
        {
            var newSlot = Instantiate(slotPrefab, parent);
            var item = ItemDatabase.ItemDictionary[jToken.ToString()];
            item.Sprite = InsertImage(item.BaseItemModel.Id);
            
            newSlot.GetComponent<ItemContainer>().BaseItem = item;
            newSlot.GetComponent<ItemContainer>().itemImage.sprite = item.Sprite;
            inventoryList.Add(newSlot);
        }
    }

    public override void CreatePopup(BaseItem baseItem, Stack<GameObject> popupStack)
    {
        var instance = Instantiate(popupPrefab, GameObject.Find("Canvas").transform);
        instance.transform.localScale *= 0.1f;
        UIAnimationManager.Instance.ExecuteOpenUIAnimationByScale(instance.transform);
        popupStack.Push(instance);
        instance.GetComponent<PopupChildrenContainer>().itemImage.sprite = baseItem.Sprite;
        instance.GetComponent<PopupChildrenContainer>().itemName.text = baseItem.BaseItemModel.Name;
        instance.GetComponent<PopupChildrenContainer>().itemType.text = LocalizationManager.Instance.LocalizeTypeText(baseItem.BaseItemModel.Type);
        instance.GetComponent<PopupChildrenContainer>().itemDescription.text = baseItem.BaseItemModel.Description;
        instance.GetComponent<PopupChildrenContainer>().button.onClick.AddListener((baseItem as EquipmentItem).ApplyEffect);
        
        instance.GetComponent<PopupChildrenContainer>().itemEffect.text = "";
        foreach (var property in typeof(Effect).GetProperties())
        {
            var value = (int)property.GetValue(((EffectableItemModel)baseItem.BaseItemModel).Effect);
            if (value == 0)
            {
                continue;
            }

            instance.GetComponent<PopupChildrenContainer>().itemEffect.text += LocalizationManager.Instance.LocalizeEffectText(property.Name) + " +" + value + "\n";
        }
    }
}
