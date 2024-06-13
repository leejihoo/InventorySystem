
public class ConsumablesItem : BaseItem, ICountable, IEffectable
{
    public int Count { get; set; }

    public ConsumablesItem(BaseItemModel baseItemModel) : base(baseItemModel)
    {
        BaseItemModel = baseItemModel;
    }
    
    public void ApplyEffect()
    {
        throw new System.NotImplementedException();
    }
}
