using System;
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
#if UNITY_EDITOR
            if (jToken["id"] == null || jToken["count"] == null)
            {
                throw new NullReferenceException("인벤토리 기타아이템 json에 id 또는 count가 null인 jToken이 존재합니다.");
            }
            
            if (!ContainsId(jToken["id"].ToString()))
            {
                throw new NullReferenceException("데이터베이스에 존재하지 않는 id 입니다.");
            }
            
#endif
            MiscItem miscItem = GetItemFrame(jToken["id"].ToString()) as MiscItem;
            
#if UNITY_EDITOR
            if (miscItem == null)
            {
                throw new NullReferenceException("id: " + jToken["id"] + "인 아이템이 데이터베이스에 value가 존재하지 않습니다.");
            }
            
#endif
            miscItem.Count = int.Parse(jToken["count"].ToString());
            miscItem.Sprite = InsertImage(miscItem.BaseItemModel.Id);
            
            var itemContainer = newSlot.GetComponent<CountableItemContainer>();
            
            itemContainer.BaseItem = miscItem;
            itemContainer.itemCount.text = miscItem.Count.ToString();
            itemContainer.itemImage.sprite = miscItem.Sprite;
            itemContainer.OnClicked += popupManager.CreateItemPopup;
            inventoryList.Add(newSlot);
        }
    }

    public override void CreatePopup(BaseItem baseItem, Stack<GameObject> popupStack)
    {
        var instance = Instantiate(popupPrefab, GameObject.Find("Canvas").transform);
        ExecuteAnimation(instance.transform,AnimaitonType.Rotation);
        
        popupStack.Push(instance);
        instance.GetComponent<PopupChildrenContainer>().itemImage.sprite = baseItem.Sprite;
        instance.GetComponent<PopupChildrenContainer>().itemName.text = baseItem.BaseItemModel.Name;
        instance.GetComponent<PopupChildrenContainer>().itemType.text = LocalizeTypeText(baseItem.BaseItemModel.Type);
        instance.GetComponent<PopupChildrenContainer>().itemDescription.text = baseItem.BaseItemModel.Description;
    }
}
