using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using File = System.IO.File;

public class Inventory : MonoBehaviour
{
    private List<GameObject> inventoryList = new List<GameObject>();
    private string inventoryJsonPath = "Assets/Json/inventory.json";

    private MiscItemFactory _miscItemFactory;
    private ConsumablesItemFactory _consumablesItemFactory;
    private EquipmentItemFactory _equipmentItemFactory;

    public GameObject CountableSlot;
    public GameObject UncountableSlot;

    private void Awake()
    {
        _miscItemFactory = GetComponent<MiscItemFactory>();
        _consumablesItemFactory = GetComponent<ConsumablesItemFactory>();
        _equipmentItemFactory = GetComponent<EquipmentItemFactory>();
    }

    // Start is called before the first frame update
    void Start()
    {
        string json = File.ReadAllText(inventoryJsonPath);
        var temp = JsonConvert.DeserializeObject<JObject>(json);
        var tempMisc =_miscItemFactory.CreateItem(temp["miscList"] as JArray, CountableSlot, inventoryList, transform);
        var tempCon = _consumablesItemFactory.CreateItem(temp["consumablesList"] as JArray, CountableSlot, inventoryList, transform);
        var tempEqu = _equipmentItemFactory.CreateItem(temp["equipmentList"] as JArray, UncountableSlot, inventoryList, transform);
    }

    void DebugItemList(List<BaseItem> list)
    {
        foreach (var element in list)
        {
            Debug.Log(JsonConvert.SerializeObject(element.BaseItemModel));
        }
    }

    public void ShowEquipmentItem()
    {
        ShowAll();
        
        var notEquips = inventoryList.Where(x =>
            x.GetComponent<ItemContainer>().BaseItem.BaseItemModel.Type != BaseItemModel.ItemType.Equipment);

        foreach (var notEquip in notEquips)
        {
            notEquip.SetActive(false);    
        }
    }
    
    public void ShowConsumablesItem()
    {
        ShowAll();
        
        var notEquips = inventoryList.Where(x =>
            x.GetComponent<ItemContainer>().BaseItem.BaseItemModel.Type != BaseItemModel.ItemType.Consumables);

        foreach (var notEquip in notEquips)
        {
            notEquip.SetActive(false);    
        }
    }
    
    public void ShowMiscItem()
    {
        ShowAll();
        
        var notEquips = inventoryList.Where(x =>
            x.GetComponent<ItemContainer>().BaseItem.BaseItemModel.Type != BaseItemModel.ItemType.Misc);

        foreach (var notEquip in notEquips)
        {
            notEquip.SetActive(false);    
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
