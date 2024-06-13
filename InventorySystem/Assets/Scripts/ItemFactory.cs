using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class ItemFactory: MonoBehaviour
{ 
    public abstract List<GameObject> CreateItem(JArray itemList, GameObject slot, List<GameObject> inventoryList, Transform parent);

    public virtual Sprite InsertImage(string id)
    {
        var op = Addressables.LoadAssetAsync<Sprite>(id);
        return op.WaitForCompletion();
    }
}
