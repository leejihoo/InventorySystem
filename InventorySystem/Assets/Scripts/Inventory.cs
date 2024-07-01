using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using File = System.IO.File;

public class Inventory : MonoBehaviour
{
    private List<GameObject> _inventoryList;
    private string _inventoryJsonPath = "Assets/Json/inventory.json";
    [SerializeField] private FactoryManager factoryManager;

    [SerializeField] private TMP_Text currentSelectedButtonText;
    private void Awake()
    {
        _inventoryList = new List<GameObject>();
        if (factoryManager == null)
        {
            factoryManager = FindObjectOfType<FactoryManager>();
        }

        ChangeButtonTextColor(currentSelectedButtonText);
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

    public void ChangeButtonTextColor(TMP_Text text)
    {
        currentSelectedButtonText.color = Color.black;
        text.color = Color.red;
        currentSelectedButtonText = text;
    }

    public void SortInventoryAscending()    
    {
        var list = _inventoryList.OrderBy(x => x.GetComponent<ItemContainer>().BaseItem.BaseItemModel.Name).ToList();

        int index = 0;
        foreach (var item in list)
        {
            item.transform.SetSiblingIndex(index);
            index++;
        }
    }

    public void UseItem(string id)
    {
        var target = _inventoryList.Find(x => x.GetComponent<ItemContainer>().BaseItem.BaseItemModel.Id == id);
        var targetCount = (target.GetComponent<CountableItemContainer>().BaseItem as ConsumablesItem)?.Count;
        if (targetCount > 0)
        {
            target.GetComponent<CountableItemContainer>().itemCount.text = targetCount.ToString();
        }
        else
        {
            (target.GetComponent<CountableItemContainer>().BaseItem as ConsumablesItem).Use -= UseItem;
            PopupManager.Instance.PopupExit();
            _inventoryList.Remove(target);
            Destroy(target);
        }
    }
}
