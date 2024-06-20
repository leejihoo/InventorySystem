using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemContainer: MonoBehaviour, IPointerClickHandler
{
    public BaseItem BaseItem;
    public void OnPointerClick(PointerEventData eventData)
    {
        PopupManager.Instance.CreateItemPopup(BaseItem, BaseItem.BaseItemModel.Type);
    }
}
