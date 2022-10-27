using System;

public class ItemPipe : IInventoryItem
{
    public IInventoryItemInfo Info { get; }
    public IInventoryItemState State { get; }
    public Type Type => GetType();

    public ItemPipe(IInventoryItemInfo info)
    {
        Info = info;
        State = new InventoryItemState();
    }
    
    public IInventoryItem Clone()
    {
        var clonedItem = new ItemPipe(Info)
        {
            State =
            {
                Amount = State.Amount
            }
        };

        return clonedItem;
    }
}