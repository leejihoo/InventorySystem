public class EquipmentItem : BaseItem,IEffectable
{
    private bool _isWear = false;
    public void ApplyEffect()
    {
        if (!_isWear)
        {
            StatController.Instance.Wear((BaseItemModel as EffectableItemModel)?.Effect);    
        }
        else
        {
            StatController.Instance.UnWear((BaseItemModel as EffectableItemModel)?.Effect);
        }

        Toggle(ref _isWear);
    }

    private void Toggle(ref bool target)
    {
        target = !target;
    }

    public EquipmentItem(BaseItemModel baseItemModel) : base(baseItemModel)
    {
        BaseItemModel = baseItemModel;
    }
}
