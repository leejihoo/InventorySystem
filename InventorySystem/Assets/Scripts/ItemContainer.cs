using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemContainer: MonoBehaviour, IPointerClickHandler
{
    public BaseItem BaseItem;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (BaseItem.BaseItemModel.Type == ItemType.Equipment)
        {
            PopupManager.Instance.CreateEquipmentItemPopup(BaseItem);
        }
    }
}
