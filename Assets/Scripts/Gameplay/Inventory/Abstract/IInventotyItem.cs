using System;

public interface IInventotyItem
{
    IInventoryItemInfo Info { get; }
    IInventoryItemState State { get; }
    Type Type { get; }

    IInventotyItem Clone();
}