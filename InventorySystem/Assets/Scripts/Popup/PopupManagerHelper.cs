using UnityEngine;
using UnityEngine.EventSystems;

public class PopupManagerHelper : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        PopupManager.Instance.PopupExit();
    }
}
