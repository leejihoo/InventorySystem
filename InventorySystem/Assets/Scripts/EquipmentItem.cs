public class EquipmentItem : BaseItem,IEffectable
{
    public bool IsWear = false;
    public void ApplyEffect()
    {
        if (!IsWear)
        {
            StatController.Instance.Wear((BaseItemModel as EffectableItemModel)?.Effect);    
        }
        else
        {
            StatController.Instance.UnWear((BaseItemModel as EffectableItemModel)?.Effect);
        }

        Toggle(ref IsWear);
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
