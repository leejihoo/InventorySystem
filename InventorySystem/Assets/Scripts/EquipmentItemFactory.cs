using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentItemFactory : ItemFactory
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override List<GameObject> CreateItem(JArray itemList, GameObject slot, List<GameObject> inventoryList, Transform parent)
    {
        List<GameObject> equipmentItemList = new List<GameObject>();
        foreach (var jToken in itemList)
        {
            var newSlot = Instantiate(slot, parent);
            var item = ItemDatabase.ItemDictionary[jToken.ToString()];
            item.Sprite = InsertImage(item.BaseItemModel.Id);
            newSlot.GetComponent<ItemContainer>().BaseItem = item;
            newSlot.GetComponentsInChildren<Image>()[1].sprite = item.Sprite;
            inventoryList.Add(newSlot);
            equipmentItemList.Add(newSlot);
        }

        return equipmentItemList;
    }
}
