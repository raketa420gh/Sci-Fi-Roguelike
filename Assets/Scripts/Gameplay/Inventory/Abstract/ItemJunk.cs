using System;

public class ItemJunk : IInventotyItem
{
    public Type Type => GetType();
    public int MaxItemsInInventorySlot { get; }
    public int Amount { get; set; }
    public bool IsEquipped { get; set; }

    public ItemJunk(int maxItemsInInventorySlot)
    {
        MaxItemsInInventorySlot = maxItemsInInventorySlot;
    }
    
    public IInventotyItem Clone()
    {
        return new ItemJunk(MaxItemsInInventorySlot)
        {
            Amount = Amount
        };
    }
}