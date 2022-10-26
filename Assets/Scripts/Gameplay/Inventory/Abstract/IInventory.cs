using System;

public interface IInventory
{
    int Capacity { get; set; }
    bool IsFull { get; }

    IInventotyItem GetItem(Type itemType);
    IInventotyItem[] GetAllItems();
    IInventotyItem[] GetAllItems(Type itemType);
    IInventotyItem[] GetEquippedItems();

    int GetItemAmount(Type itemType);
    bool Add(object sender, IInventotyItem item);
    void Remove(object sender, Type itemType, int amount = 1);
    bool HasItem(Type itemType, out IInventotyItem item);
}