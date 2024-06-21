using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemContainer: MonoBehaviour, IPointerClickHandler
{
    public BaseItem BaseItem;
    private PopupManager _popupManager;

    public void Awake()
    {
        _popupManager = FindObjectOfType<PopupManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _popupManager.CreateItemPopup(BaseItem, BaseItem.BaseItemModel.Type);
    }
}
