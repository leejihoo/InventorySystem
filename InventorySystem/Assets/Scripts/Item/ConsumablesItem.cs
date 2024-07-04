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
        var effect = (BaseItemModel as EffectableItemModel)?.Effect;
#if UNITY_EDITOR
        if (effect == null)
        {
            throw new Exception("BaseItemModel을 EffectbleItemModel로 형변환할 수 없거나 Effect 객체가 null입니다.");
        }
#endif
        StatController.Instance.Use(effect);
        Count--;
        Use?.Invoke(BaseItemModel.Id);
    }
    
}
