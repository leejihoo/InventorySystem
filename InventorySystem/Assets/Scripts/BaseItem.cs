using UnityEngine;

public class BaseItem
{
    public BaseItemModel BaseItemModel { get; set; }
    public Sprite Sprite { get; set; }

    protected BaseItem(BaseItemModel baseItemModel)
    {
        BaseItemModel = baseItemModel;
    }
}
