using System;

public interface IInventorySlot
{
    bool IsFull { get; }
    bool IsEmpty { get; }
    
    IInventotyItem Item { get; }
    Type ItemType { get; }
    int Amount { get; }
    int Capacity { get; }

    void SetItem(IInventotyItem item);
    void Clear();
}