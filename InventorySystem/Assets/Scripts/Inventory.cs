using System;
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
    private const string InventoryJsonPath = "Assets/Json/inventory.json";
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
        string json = File.ReadAllText(InventoryJsonPath);
        var inventoryInfo = JsonConvert.DeserializeObject<JObject>(json);
#if UNITY_EDITOR
        if (inventoryInfo == null)
        {
            throw new NullReferenceException("inventoryInfo가 null입니다.");
        }
#endif
        
        foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
        {
            var itemTypeName = Enum.GetName(typeof(ItemType), itemType)?.ToLower();
#if UNITY_EDITOR
            if (itemTypeName == null)
            {
                throw new NullReferenceException("ItemType enum 내에 itemType이 존재하지 않아 이름을 찾을 수 없습니다.");
            }
#endif
            factoryManager.CreateItem(inventoryInfo[itemTypeName + "List"] as JArray, _inventoryList, transform, itemType);
        }
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
        var countableItemContainer = target.GetComponent<CountableItemContainer>();
        var castingItem = countableItemContainer.BaseItem as ConsumablesItem;
#if UNITY_EDITOR
        if (castingItem == null)
        {
            throw new NullReferenceException("BaseItem을 ConsumablesItem로 형변환 할 수 없습니다.");
        }
#endif
        var targetCount = castingItem.Count;
        
        if (targetCount > 0)
        {
            countableItemContainer.itemCount.text = targetCount.ToString();
        }
        else
        {
            castingItem.Use -= UseItem;
            PopupManager.Instance.PopupExit();
            _inventoryList.Remove(target);
            Destroy(target);
        }
    }
}
