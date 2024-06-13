using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablesItem : BaseItem, ICountable, IEffectable
{
    public int Count { get; set; }

    public void ApplyEffect()
    {
        throw new System.NotImplementedException();
    }

    public ConsumablesItem(BaseItemModel baseItemModel) : base(baseItemModel)
    {
        BaseItemModel = baseItemModel;
    }
}
