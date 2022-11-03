using System;

public interface IInventorySlot
{
    IInventoryItem Item { get; }
    SlotType SlotType { get; }
    Type ItemType { get; }
    bool IsFull { get; }
    bool IsEmpty { get; }
    int Amount { get; }
    int Capacity { get; }

    void SetItem(IInventoryItem item);
    void Clear();
}