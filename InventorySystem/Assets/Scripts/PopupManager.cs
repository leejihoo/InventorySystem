using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GameObject equipmentItemPopup;
    public GameObject miscItemPopup;
    public GameObject consumablesItemPopup;
    public GameObject blur;
    
    public Stack<GameObject> PopupStack;

    public static PopupManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        PopupStack = new Stack<GameObject>();
    }

    public void CreateEquipmentItemPopup(BaseItem baseItem)
    {
        var instance = Instantiate(equipmentItemPopup, GameObject.Find("Canvas").transform);
        PopupStack.Push(instance);
        instance.GetComponent<PopupChildrenContainer>().itemImage.sprite = baseItem.Sprite;
        instance.GetComponent<PopupChildrenContainer>().itemName.text = baseItem.BaseItemModel.Name;
        instance.GetComponent<PopupChildrenContainer>().itemType.text = baseItem.BaseItemModel.Type.ToString();
        instance.GetComponent<PopupChildrenContainer>().itemDescription.text = baseItem.BaseItemModel.Description;

        foreach (var property in typeof(Effect).GetProperties())
        {
            var value = (int)property.GetValue(((EffectableItemModel)baseItem.BaseItemModel).Effect);
            if (value == 0)
            {
                continue;
            }

            instance.GetComponent<PopupChildrenContainer>().itemEffect.text = property.Name + " +" + value + "\n";
        }
    }
    
    public static void CreateMiscItemPopup()
    {
        
    }
    
    public static void CreateConsumablesItemPopup()
    {
        
    public void PopupExit()
    {
        while (PopupStack.Count > 0)
        {
            Destroy(PopupStack.Pop());
        }
    }

    public void CreateBlur()
    {
        var instance =Instantiate(blur, GameObject.Find("Canvas").transform);
        PopupStack.Push(instance);
    }
}
