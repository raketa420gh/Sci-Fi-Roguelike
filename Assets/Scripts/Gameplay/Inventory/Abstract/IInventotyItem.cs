using System;

public interface IInventotyItem
{
    Type Type { get; }
    int MaxItemsInInventorySlot { get; }
    int Amount { get; set; }
    bool IsEquipped { get; set; }

    IInventotyItem Clone();
}