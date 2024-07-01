
using System;

public class ConsumablesItem : BaseItem, ICountable, IEffectable
{
    public int Count { get; set; }
    public Action<string> Use;

    public ConsumablesItem(BaseItemModel baseItemModel) : base(baseItemModel)
    {
        BaseItemModel = baseItemModel;
    }
    
    public void ApplyEffect()
    {
        StatController.Instance.Use((BaseItemModel as EffectableItemModel)?.Effect);
        Count--;
        Use?.Invoke(BaseItemModel.Id);
    }
    
}
