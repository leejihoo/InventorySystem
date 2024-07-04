using System.Collections.Generic;
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

    public void CreateItemPopup(BaseItem baseItem, ItemType itemType)
    {
        CreateBlur();
        factoryManager.CreatePopup(baseItem, _popupStack, itemType);
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

    private void CreateBlur()
    {
        var instance =Instantiate(blur, GameObject.Find("Canvas").transform);
        _popupStack.Push(instance);
    }
}
