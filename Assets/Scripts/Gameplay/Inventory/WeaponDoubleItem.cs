using System;

public class WeaponDoubleItem : IInventoryItem
{
    public IInventoryItemInfo Info { get; }
    public IInventoryItemState State { get; }
    public Type Type => GetType();

    public WeaponDoubleItem(IInventoryItemInfo info)
    {
        Info = info;
        State = new InventoryItemState();
    }
    
    public IInventoryItem Clone() => 
        new WeaponDoubleItem(Info) { State = { Amount = State.Amount } };
}