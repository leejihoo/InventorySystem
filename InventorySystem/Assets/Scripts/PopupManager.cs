using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GameObject blur;
    
    public Stack<GameObject> PopupStack;

    public static PopupManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        PopupStack = new Stack<GameObject>();
    }

    public void CreateItemPopup(BaseItem baseItem, ItemType itemtype)
    {
        CreateBlur();
        FactoryManager.Instance.CreatePopup(baseItem, PopupStack, itemtype);
    }

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
        _popupStack.Push(instance);
    }
}
