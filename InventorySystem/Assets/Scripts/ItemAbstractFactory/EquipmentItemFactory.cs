using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class EquipmentItemFactory : ItemFactory
{
    public override void CreateItem(JArray itemList, List<GameObject> inventoryList, Transform parent)
    {
        foreach (var jToken in itemList)
        {
            var newSlot = Instantiate(slotPrefab, parent);
            var item = GetItemFrame(jToken.ToString());
            item.Sprite = InsertImage(item.BaseItemModel.Id); 
            
            var itemContainer = newSlot.GetComponent<ItemContainer>();
            itemContainer.BaseItem = item;
            itemContainer.itemImage.sprite = item.Sprite;
            itemContainer.OnClicked += popupManager.CreateItemPopup;
            
            inventoryList.Add(newSlot);
        }
    }
    
    public override void CreatePopup(BaseItem baseItem, Stack<GameObject> popupStack)
    {
        var instance = Instantiate(popupPrefab, GameObject.Find("Canvas").transform);
        ExecuteAnimation(instance.transform,AnimaitonType.Scale);
        popupStack.Push(instance);

        var popupChildrenContainer = instance.GetComponent<PopupChildrenContainer>();
        popupChildrenContainer.itemImage.sprite = baseItem.Sprite;
        popupChildrenContainer.itemName.text = baseItem.BaseItemModel.Name;
        popupChildrenContainer.itemType.text = LocalizeTypeText(baseItem.BaseItemModel.Type);
        popupChildrenContainer.itemDescription.text = baseItem.BaseItemModel.Description;
        var castingItem = baseItem as EquipmentItem;
        #if UNITY_EDITOR
        if (castingItem == null)
        {
            throw new InvalidCastException("baseItem을 EquipmentItem으로 형변환 할 수 없습니다.");
        }
        #endif
        popupChildrenContainer.button.onClick.AddListener(castingItem.ApplyEffect);
        
        instance.GetComponent<PopupChildrenContainer>().itemEffect.text = "";
        foreach (var property in typeof(Effect).GetProperties())
        {
            var castingModel = baseItem.BaseItemModel as EffectableItemModel;
            #if UNITY_EDITOR
            if (castingModel == null)
            {
                throw new InvalidCastException("BaseItemModel을 EffectableItemModel으로 형변환 할 수 없습니다.");
            }
            #endif
            var value = (int)property.GetValue(castingModel.Effect);
            if (value == 0)
            {
                continue;
            }

            instance.GetComponent<PopupChildrenContainer>().itemEffect.text += LocalizeEffectText(property.Name) + " +" + value + "\n";
        }
    }
}
