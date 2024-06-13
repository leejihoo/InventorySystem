using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseItem
{
    public BaseItemModel BaseItemModel;
    public Sprite Sprite;

    public BaseItem(BaseItemModel baseItemModel)
    {
        BaseItemModel = baseItemModel;
    }
}
