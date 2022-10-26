using System;

public class ItemJunk : IInventotyItem
{
    public IInventoryItemInfo Info { get; }
    public IInventoryItemState State { get; }
    public Type Type => GetType();

    public ItemJunk(IInventoryItemInfo info)
    {
        Info = Info;
        State = new InventoryItemState();
    }
    
    public IInventotyItem Clone()
    {
        var clonedItemJunk = new ItemJunk(Info)
        {
            State =
            {
                Amount = State.Amount
            }
        };

        return clonedItemJunk;
    }
}