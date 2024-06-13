using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscItem : BaseItem,ICountable
{
    public int Count { get; set; }

    public MiscItem(BaseItemModel baseItemModel) : base(baseItemModel)
    {
        BaseItemModel = baseItemModel;
    }
}
