using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;

public class ConsumablesItemFactory : ItemFactory
{
    [SerializeField] private Inventory inventory;
    public override void CreateItem(JArray itemList, List<GameObject> inventoryList, Transform parent)
    {
        foreach (var jToken in itemList)
        {
            var newSlot = Instantiate(slotPrefab, parent);
            
            #if UNITY_EDITOR
            if (jToken["id"] == null || jToken["count"] == null)
            {
                throw new NullReferenceException("인벤토리 소비아이템 json에 id 또는 count가 null인 jToken이 존재합니다.");
            }
            
            if (!ItemDatabase.ItemDictionary.ContainsKey(jToken["id"].ToString()))
            {
                throw new NullReferenceException("데이터베이스에 존재하지 않는 id 입니다.");
            }
            
            #endif
            
            ConsumablesItem consumablesItem = ItemDatabase.ItemDictionary[jToken["id"].ToString()] as ConsumablesItem;
            
            #if UNITY_EDITOR
            if (consumablesItem == null)
            {
                throw new NullReferenceException("id: " + jToken["id"] + "인 아이템이 데이터베이스에 value가 존재하지 않습니다.");
            }
            
            #endif
            
            consumablesItem.Count = int.Parse(jToken["count"].ToString());
            consumablesItem.Sprite = InsertImage(consumablesItem.BaseItemModel.Id);

            var itemContainer = newSlot.GetComponent<CountableItemContainer>();
            
            itemContainer.BaseItem = consumablesItem;
            itemContainer.itemCount.text = consumablesItem.Count.ToString();
            itemContainer.itemImage.sprite = consumablesItem.Sprite;
            itemContainer.OnClicked += popupManager.CreateItemPopup;
            var castingItem = itemContainer.BaseItem as ConsumablesItem;
            
            #if UNITY_EDITOR
            if (castingItem == null)
            {
                throw new InvalidCastException("itemContainer의 BaseItem을 ConsumablesItem으로 형변환 할 수 없습니다.");
            }
            
            #endif
            castingItem.Use += inventory.UseItem;
            
            inventoryList.Add(newSlot);
        }
    }

    public override void CreatePopup(BaseItem baseItem, Stack<GameObject> popupStack)
    {
        var instance = Instantiate(popupPrefab, GameObject.Find("Canvas").transform);
        instance.GetComponent<CanvasGroup>().alpha = 0f;
        UIAnimationManager.Instance.ExecuteOpenUIAnimationByAlpha(instance.transform);
        popupStack.Push(instance);
        
        var popupChildrenContainer = instance.GetComponent<PopupChildrenContainer>();
        popupChildrenContainer.itemImage.sprite = baseItem.Sprite;
        popupChildrenContainer.itemName.text = baseItem.BaseItemModel.Name;
        popupChildrenContainer.itemType.text = LocalizationManager.Instance.LocalizeTypeText(baseItem.BaseItemModel.Type);
        popupChildrenContainer.itemDescription.text = baseItem.BaseItemModel.Description;
        
        var castingItem = baseItem as ConsumablesItem;
        #if UNITY_EDITOR
            if (castingItem == null)
        {
            throw new InvalidCastException("BaseItem을 ConsumablesItem으로 형변환 할 수 없습니다.");
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
                throw new InvalidCastException("baseItem의 BaseItemModel을 EffectableItemModel로 형변환할 수 없습니다.");
            }
            #endif
            var value = (int)property.GetValue(castingModel.Effect);
            // if (value == 0)
            // {
            //     continue;
            // }

            instance.GetComponent<PopupChildrenContainer>().itemEffect.text += LocalizationManager.Instance.LocalizeEffectText(property.Name) + " +" + value + "\n";
        }
    }
}
