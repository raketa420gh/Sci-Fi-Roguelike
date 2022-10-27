using System;

public class InventoryItem : IInventoryItem
{
    public IInventoryItemInfo Info { get; }
    public IInventoryItemState State { get; }
    public Type Type => GetType();

    public InventoryItem(InventoryItemData itemData)
    {
        
    }
    
    public IInventoryItem Clone()
    {
        throw new NotImplementedException();
    }
}