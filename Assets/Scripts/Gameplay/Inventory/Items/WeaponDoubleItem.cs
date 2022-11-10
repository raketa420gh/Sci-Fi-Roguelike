public class WeaponDoubleItem : InventoryItem
{
    public WeaponDoubleItem(IInventoryItemInfo info) : base(info)
    {
    }
    
    public override IInventoryItem Clone() => 
        new WeaponDoubleItem(Info) { State = { Amount = State.Amount } };
}