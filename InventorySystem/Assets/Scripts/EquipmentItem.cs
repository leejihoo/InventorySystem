using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : BaseItem,IEffectable
{
    public void ApplyEffect()
    {
        throw new System.NotImplementedException();
    }

    public EquipmentItem(BaseItemModel baseItemModel) : base(baseItemModel)
    {
        BaseItemModel = baseItemModel;
    }
}
