using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using DG.Tweening;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GameObject blur;
    private Stack<GameObject> _popupStack;

    [SerializeField]
    private FactoryManager factoryManager;

    public static PopupManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        _popupStack = new Stack<GameObject>();

        if (factoryManager == null)
        {
            factoryManager = FindObjectOfType<FactoryManager>();
        }
    }

    public void CreateItemPopup(BaseItem baseItem, ItemType itemtype)
    {
        CreateBlur();
        factoryManager.CreatePopup(baseItem, _popupStack, itemtype);
    }

    public void PopupExit()
    {
        while (_popupStack.Count > 0)
        {
            var popup = _popupStack.Pop();
            popup.transform.DOKill();
            popup.GetComponent<CanvasGroup>()?.DOKill();
            Destroy(popup);
        }
    }

    public void CreateBlur()
    {
        var instance =Instantiate(blur, GameObject.Find("Canvas").transform);
        _popupStack.Push(instance);
    }
}
