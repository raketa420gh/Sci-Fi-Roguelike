public class WeaponSingleItem : InventoryItem
{
    public WeaponSingleItem(IInventoryItemInfo info) : base(info)
    {
    }
    
    public override IInventoryItem Clone() => 
        new WeaponSingleItem(Info) { State = { Amount = State.Amount } };
}