using System;

public class ItemJunk : IInventoryItem
{
    public IInventoryItemInfo Info { get; }
    public IInventoryItemState State { get; }
    public Type Type => GetType();

    public ItemJunk(IInventoryItemInfo info)
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