using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class ItemFactory: MonoBehaviour
{
    [SerializeField] protected GameObject slotPrefab;
    [SerializeField] protected GameObject popupPrefab;
    [SerializeField] protected PopupManager popupManager;
    public abstract void CreateItem(JArray itemList, List<GameObject> inventoryList, Transform parent);
    public abstract void CreatePopup(BaseItem baseItem, Stack<GameObject> popupStack);

    public virtual void Awake()
    {
        if (popupManager == null)
        {
            popupManager = FindObjectOfType<PopupManager>();
        }
    }

    public virtual Sprite InsertImage(string id)
    {
        var op = Addressables.LoadAssetAsync<Sprite>(id);
        return op.WaitForCompletion();
    }
}
