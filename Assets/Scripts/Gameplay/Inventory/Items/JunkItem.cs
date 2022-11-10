public class JunkItem : InventoryItem
{
    public JunkItem(IInventoryItemInfo info) : base(info)
    {
    }
    
    public override IInventoryItem Clone() => 
        new JunkItem(Info) { State = { Amount = State.Amount } };
}