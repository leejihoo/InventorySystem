using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class ItemFactory: MonoBehaviour
{ 
    public abstract void CreateItem(JArray itemList, GameObject slot, List<GameObject> inventoryList, Transform parent);

    public virtual Sprite InsertImage(string id)
    {
        var op = Addressables.LoadAssetAsync<Sprite>(id);
        return op.WaitForCompletion();
    }
}
