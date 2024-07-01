using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsumablesItemFactory : ItemFactory
{
    [SerializeField] private Inventory inventory;
    public override void CreateItem(JArray itemList, List<GameObject> inventoryList, Transform parent)
    {
        foreach (var jToken in itemList)
        {
            var newSlot = Instantiate(slotPrefab, parent);
            ConsumablesItem consumablesItem = ItemDatabase.ItemDictionary[jToken["id"].ToString()] as ConsumablesItem;
            consumablesItem.Count = int.Parse(jToken["count"].ToString());
            consumablesItem.Sprite = InsertImage(consumablesItem.BaseItemModel.Id);
            
            newSlot.GetComponent<ItemContainer>().BaseItem = consumablesItem;
            newSlot.GetComponentInChildren<TMP_Text>().text = consumablesItem.Count.ToString();
            newSlot.GetComponent<ItemContainer>().itemImage.sprite = consumablesItem.Sprite;
            (newSlot.GetComponent<ItemContainer>().BaseItem as ConsumablesItem).Use += inventory.UseItem;
            
            inventoryList.Add(newSlot);
        }
    }

    public override void CreatePopup(BaseItem baseItem, Stack<GameObject> popupStack)
    {
        var instance = Instantiate(popupPrefab, GameObject.Find("Canvas").transform);
        instance.GetComponent<CanvasGroup>().alpha = 0f;
        UIAnimationManager.Instance.ExecuteOpenUIAnimationByAlpha(instance.transform);
        popupStack.Push(instance);
        
        instance.GetComponent<PopupChildrenContainer>().itemImage.sprite = baseItem.Sprite;
        instance.GetComponent<PopupChildrenContainer>().itemName.text = baseItem.BaseItemModel.Name;
        instance.GetComponent<PopupChildrenContainer>().itemType.text = LocalizationManager.Instance.LocalizeTypeText(baseItem.BaseItemModel.Type);
        instance.GetComponent<PopupChildrenContainer>().itemDescription.text = baseItem.BaseItemModel.Description;
        instance.GetComponent<PopupChildrenContainer>().button.onClick.AddListener((baseItem as ConsumablesItem).ApplyEffect);
        
        instance.GetComponent<PopupChildrenContainer>().itemEffect.text = "";
        foreach (var property in typeof(Effect).GetProperties())
        {
            var value = (int)property.GetValue(((EffectableItemModel)baseItem.BaseItemModel).Effect);
            // if (value == 0)
            // {
            //     continue;
            // }

            instance.GetComponent<PopupChildrenContainer>().itemEffect.text += LocalizationManager.Instance.LocalizeEffectText(property.Name) + " +" + value + "\n";
        }
    }
}
