using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine.UI;

public class MiscItemFactory : ItemFactory
{
    public override void CreateItem(JArray itemList, List<GameObject> inventoryList, Transform parent)
    {
        foreach (var jToken in itemList)
        {
            var newSlot = Instantiate(slotPrefab, parent);
            MiscItem miscItem = ItemDatabase.ItemDictionary[jToken["id"].ToString()] as MiscItem;
            miscItem.Count = int.Parse(jToken["count"].ToString());
            miscItem.Sprite = InsertImage(miscItem.BaseItemModel.Id);
            
            newSlot.GetComponent<ItemContainer>().BaseItem = miscItem;
            newSlot.GetComponentInChildren<TMP_Text>().text = miscItem.Count.ToString();
            newSlot.GetComponentsInChildren<Image>()[1].sprite = miscItem.Sprite;
            inventoryList.Add(newSlot);
        }
    }

    public override void CreatePopup(BaseItem baseItem, Stack<GameObject> popupStack)
    {
        var instance = Instantiate(popupPrefab, GameObject.Find("Canvas").transform);
        popupStack.Push(instance);
        instance.GetComponent<PopupChildrenContainer>().itemImage.sprite = baseItem.Sprite;
        instance.GetComponent<PopupChildrenContainer>().itemName.text = baseItem.BaseItemModel.Name;
        instance.GetComponent<PopupChildrenContainer>().itemType.text = PopupManager.Instance.LocalizeTypeText(baseItem.BaseItemModel.Type);
        instance.GetComponent<PopupChildrenContainer>().itemDescription.text = baseItem.BaseItemModel.Description;
    }
}
