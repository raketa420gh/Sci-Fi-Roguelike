using System;

public interface IInventotyItem
{
    bool IsEquipped { get; }
    Type Type { get; }
    int MaxAmountInInventorySlot { get; }
    int Amount { get; }

    IInventotyItem Clone();
}