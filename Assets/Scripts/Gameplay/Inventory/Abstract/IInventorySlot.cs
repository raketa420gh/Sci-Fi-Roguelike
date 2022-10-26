using System;

public interface IInventorySlot
{
    IInventotyItem Item { get; }
    Type ItemType { get; }
    bool IsFull { get; }
    bool IsEmpty { get; }
    int Amount { get; }
    int Capacity { get; }

    void SetItem(IInventotyItem item);
    void Clear();
}