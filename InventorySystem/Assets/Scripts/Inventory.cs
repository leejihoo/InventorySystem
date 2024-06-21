using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using File = System.IO.File;

public class Inventory : MonoBehaviour
{
    private List<GameObject> _inventoryList;
    private string _inventoryJsonPath = "Assets/Json/inventory.json";
    [SerializeField] private FactoryManager factoryManager;

    private void Awake()
    {
        _inventoryList = new List<GameObject>();
        if (factoryManager == null)
        {
            factoryManager = FindObjectOfType<FactoryManager>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        string json = File.ReadAllText(_inventoryJsonPath);
        var inventoryInfo = JsonConvert.DeserializeObject<JObject>(json);
        // 이것도 줄이고 싶다.
        factoryManager.CreateItem(inventoryInfo["miscList"] as JArray, _inventoryList, transform,ItemType.Misc);
        factoryManager.CreateItem(inventoryInfo["consumablesList"] as JArray, _inventoryList, transform,ItemType.Consumables);
        factoryManager.CreateItem(inventoryInfo["equipmentList"] as JArray, _inventoryList, transform,ItemType.Equipment);
    }

    public void ShowClassifiedItem(int itemType) 
    {
        ShowAll();
        
        var notTypeItems = _inventoryList.Where(x =>
            x.GetComponent<ItemContainer>().BaseItem.BaseItemModel.Type != (ItemType)itemType);

        foreach (var notTypeItem in notTypeItems)
        {
            notTypeItem.SetActive(false);    
        }
    }

    public void ShowAll()
    {
        foreach (var element in _inventoryList)
        {
            element.SetActive(true);    
        }
    }
}
