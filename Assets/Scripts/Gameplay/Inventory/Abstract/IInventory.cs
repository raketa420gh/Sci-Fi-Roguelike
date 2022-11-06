using System;

public interface IInventory
{
    event Action<object, IInventoryItem, int> OnItemAdded;
    event Action<object, Type, int> OnItemRemoved;
    event Action<object> OnStateChanged;
    
    int Capacity { get; set; }
    bool IsFull { get; }

    IInventorySlot[] GetAllSlots();
    IInventoryItem GetItem(Type itemType);
    IInventoryItem[] GetAllItems();
    IInventoryItem[] GetAllItems(Type itemType);
    IInventoryItem[] GetEquippedItems();
    int GetItemAmount(Type itemType);
    bool TryToAdd(object sender, IInventoryItem item);
    void Remove(object sender, Type itemType, int amount = 1);
    bool HasItem(Type itemType, out IInventoryItem item);
}