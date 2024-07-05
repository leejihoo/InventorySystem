using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

// 1번. GameObject들을 각각의 팩토리에 래퍼랜스를 붙여주면 if else로 딥스가 깊어지는 걸 막아줄 수 있어서 더 객체지향적으로 작성 가능할 듯 하다.
// 근데 2번. countableSlot처럼 동일한 형식의 래퍼런스가 여러 팩토리에서 사용될 경우 각 팩토리에 동일한 래퍼런스를 등록하면 메모리 낭비가 된다... 어떤게 좋을까
// 1번의 구조가 solid의 srp에 만족한다고 생각하여 1번 구조로 변경,, 왜냐하면 factoryManager는 말그대로 관리만 할 뿐 실제 팩토리에서 무엇을 생산하는지는 알 수 없다 그렇기 때문에
// 실제 생성되는 프리팹들을 manager에 붙이는게 아니라 각 팩토리에서 관리해줘야 된다고 생각한다.
public class FactoryManager : MonoBehaviour
{
    private Dictionary<ItemType, ItemFactory> _itemFactoryDictionary;
    
    public void Awake()
    {
        _itemFactoryDictionary = new Dictionary<ItemType, ItemFactory>();
        _itemFactoryDictionary.Add(ItemType.Misc,GetComponent<MiscItemFactory>());
        _itemFactoryDictionary.Add(ItemType.Equipment,GetComponent<EquipmentItemFactory>());
        _itemFactoryDictionary.Add(ItemType.Consumables,GetComponent<ConsumablesItemFactory>());
    }

    public void CreateItem(JArray itemList, List<GameObject> inventoryList, Transform parent, ItemType itemType)
    {
        var factory = _itemFactoryDictionary[itemType];
        factory.CreateItem(itemList, inventoryList, parent);
    }

    public void CreatePopup(BaseItem baseItem, Stack<GameObject> popupStack, ItemType itemType)
    {
        var factory = _itemFactoryDictionary[itemType];
        factory.CreatePopup(baseItem, popupStack);
    }
}
