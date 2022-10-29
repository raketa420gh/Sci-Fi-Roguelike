using System;

public class InventoryItem : IInventoryItem
{
    public IInventoryItemInfo Info { get; }
    public IInventoryItemState State { get; }
    public Type Type => GetType();

    public InventoryItem(IInventoryItemInfo info)
    {
        Info = info;
        State = new InventoryItemState();
    }
    
    public IInventoryItem Clone()
    {
        var clonedItem = new ItemJunk(Info)
        {
            State =
            {
                Amount = State.Amount
            }
        };

        return clonedItem;
    }
}