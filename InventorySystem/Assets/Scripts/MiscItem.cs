public class MiscItem : BaseItem,ICountable
{
    public int Count { get; set; }

    public MiscItem(BaseItemModel baseItemModel) : base(baseItemModel)
    {
        BaseItemModel = baseItemModel;
    }
}
