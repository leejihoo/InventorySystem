using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine.UI;

public class MiscItemFactory : ItemFactory
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
        List<GameObject> miscItemList = new List<GameObject>();
        foreach (var jToken in itemList)
        {
            var newSlot = Instantiate(slot, parent);
            MiscItem miscItem = ItemDatabase.ItemDictionary[jToken["id"].ToString()] as MiscItem;
            miscItem.Count = int.Parse(jToken["count"].ToString());
            miscItem.Sprite = InsertImage(miscItem.BaseItemModel.Id);
            newSlot.GetComponent<ItemContainer>().BaseItem = miscItem;
            newSlot.GetComponentInChildren<TMP_Text>().text = miscItem.Count.ToString();
            newSlot.GetComponentsInChildren<Image>()[1].sprite = miscItem.Sprite;
            inventoryList.Add(newSlot);
            miscItemList.Add(slot);
        }

        return miscItemList;
    }
}
