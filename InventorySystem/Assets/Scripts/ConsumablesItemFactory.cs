using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsumablesItemFactory : ItemFactory
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
        List<GameObject> consumablesItemList = new List<GameObject>();
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
            consumablesItemList.Add(slot);
        }

        return consumablesItemList;
    }
}
