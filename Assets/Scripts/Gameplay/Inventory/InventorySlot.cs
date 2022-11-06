using System;

public class InventorySlot : IInventorySlot
{
    public IInventoryItem Item { get; private set; }
    public SlotType SlotType { get; private set; }
    public Type ItemType => Item.Type;
    public bool IsFull => !IsEmpty && Amount == Capacity;
    public bool IsEmpty => Item == null;
    public int Amount => IsEmpty ? 0 :  Item.State.Amount;
    public int Capacity { get; private set; }

    public InventorySlot(SlotType slotType) 
        => SlotType = slotType;

    public void SetItem(IInventoryItem item)
    {
        if (!IsEmpty)
            return;

        Item = item;
        Capacity = item.Info.MaxItemsInSlot;
    }

    public void Clear()
    {
        if (IsEmpty)
            return;

        Item.State.Amount = 0;
        Item = null;
    }
}