using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using File = System.IO.File;

public class Inventory : MonoBehaviour
{
    private List<GameObject> inventoryList;
    private string inventoryJsonPath = "Assets/Json/inventory.json";

    private void Awake()
    {
        inventoryList = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        string json = File.ReadAllText(inventoryJsonPath);
        var inventoryInfo = JsonConvert.DeserializeObject<JObject>(json);
        // 이것도 줄이고 싶다.
        FactoryManager.Instance.CreateItem(inventoryInfo["miscList"] as JArray, inventoryList, transform,ItemType.Misc);
        FactoryManager.Instance.CreateItem(inventoryInfo["consumablesList"] as JArray, inventoryList, transform,ItemType.Consumables);
        FactoryManager.Instance.CreateItem(inventoryInfo["equipmentList"] as JArray, inventoryList, transform,ItemType.Equipment);
    }

    public void ShowClassifiedItem(int itemType) 
    {
        ShowAll();
        
        var notTypeItems = inventoryList.Where(x =>
            x.GetComponent<ItemContainer>().BaseItem.BaseItemModel.Type != (ItemType)itemType);

        foreach (var notTypeItem in notTypeItems)
        {
            notTypeItem.SetActive(false);    
        }
    }

    public void ShowAll()
    {
        foreach (var element in inventoryList)
        {
            element.SetActive(true);    
        }
    }
}
