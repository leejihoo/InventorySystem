using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

// GameObject들을 각각의 팩토리에 래퍼랜스를 붙여주면 if else로 딥스가 깊어지는 걸 막아줄 수 있어서 더 객체지향적으로 작성 가능할 듯 하다.
// 근데 countableSlot처럼 동일한 형식의 래퍼런스가 여러 팩토리에서 사용될 경우 각 팩토리에 동일한 래퍼런스를 등록하면 메모리 낭비가 된다... 어떤게 좋을까
public class FactoryManager : Singleton<FactoryManager>
{
    public Dictionary<ItemType, ItemFactory> itemDictionary;

    [SerializeField] public GameObject equipmentItemPopup;
    [SerializeField] public GameObject miscItemPopup;
    [SerializeField] public GameObject consumablesItemPopup;
    
    [SerializeField] public GameObject countableSlot;
    [SerializeField] public GameObject uncountableSlot;
    
    public override void Awake()
    {
        base.Awake();
        itemDictionary = new Dictionary<ItemType, ItemFactory>();
        itemDictionary.Add(ItemType.Misc,GetComponent<MiscItemFactory>());
        itemDictionary.Add(ItemType.Equipment,GetComponent<EquipmentItemFactory>());
        itemDictionary.Add(ItemType.Consumables,GetComponent<ConsumablesItemFactory>());
        
    }

    public void CreateItem(JArray itemList, List<GameObject> inventoryList, Transform parent, ItemType itemType)
    {
        var factory = itemDictionary[itemType];
        GameObject slot = null;
        
        if (itemType == ItemType.Equipment)
        {
            slot = uncountableSlot;
        }
        else if (itemType == ItemType.Consumables || itemType == ItemType.Misc)
        {
            slot = countableSlot;
        }
        
        factory.CreateItem(itemList,slot,inventoryList,parent);
    }

    public void CreatePopup(BaseItem baseItem, Stack<GameObject> popupStack, ItemType itemType)
    {
        var factory = itemDictionary[itemType];
        GameObject popup = null;

        if (itemType == ItemType.Consumables)
        {
            popup = consumablesItemPopup;
        }
        else if (itemType == ItemType.Equipment)
        {
            popup = equipmentItemPopup;
        }
        else if (itemType == ItemType.Misc)
        {
            popup = miscItemPopup;
        }
        
        factory.CreatePopup(baseItem,popup,popupStack);
    }
}
