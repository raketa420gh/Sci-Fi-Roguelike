using System;

public abstract class InventoryItem : IInventoryItem
{
    public IInventoryItemInfo Info { get; }
    public IInventoryItemState State { get; }
    public Type Type => GetType();

    protected InventoryItem(IInventoryItemInfo info)
    {
        Info = info;
        State = new InventoryItemState();
    }
    
    public virtual IInventoryItem Clone()
    {
        return null;
    }
}