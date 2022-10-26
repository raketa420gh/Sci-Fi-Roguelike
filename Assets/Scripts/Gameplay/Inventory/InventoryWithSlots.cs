using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryWithSlots : IInventory
{
    public event Action<object, IInventotyItem, int> OnItemAdded;
    public event Action<object, Type, int> OnItemRemoved;

    public int Capacity { get; set; }
    public bool IsFull => _slots.All(slot => slot.IsFull);

    private List<IInventorySlot> _slots;

    public InventoryWithSlots(int capacity)
    {
        Capacity = capacity;

        _slots = new List<IInventorySlot>(capacity);

        for (var i = 0; i < capacity; i++)
            _slots.Add(new InventorySlot());
    }

    public IInventorySlot[] GetAllSlots() =>
        _slots.ToArray();
    
    public IInventotyItem GetItem(Type itemType) => 
        _slots.Find(slot => slot.ItemType == itemType).Item;

    public IInventotyItem[] GetAllItems() => 
        (from slot in _slots where !slot.IsEmpty select slot.Item).ToArray();

    public IInventotyItem[] GetAllItems(Type itemType) => 
        _slots.FindAll(slot => !slot.IsEmpty && slot.ItemType == itemType).Select(slot => slot.Item).ToArray();

    public IInventotyItem[] GetEquippedItems() =>
        _slots.FindAll(slot => !slot.IsEmpty && slot.Item.State.IsEquipped).Select(slot => slot.Item).ToArray();
    
    public int GetItemAmount(Type itemType) => 
        _slots.FindAll(slot => !slot.IsEmpty && slot.ItemType == itemType).Sum(slot => slot.Amount);

    public bool TryToAdd(object sender, IInventotyItem item)
    {
        var suitableSlot = _slots.Find(slot => !slot.IsEmpty && !slot.IsFull && slot.ItemType == item.Type);

        if (suitableSlot != null)
            return TryToAddToSlot(sender, suitableSlot, item);

        var emptySlot = _slots.Find(slot => slot.IsEmpty);

        if (emptySlot != null)
            return TryToAddToSlot(sender, emptySlot, item);

        Debug.Log($"Impossible to add an item to {this}");
        
        return false;
    }

    public void Remove(object sender, Type itemType, int amount = 1)
    {
        var slotsWithItem = GetAllSlots(itemType);
        
        if (slotsWithItem.Length == 0)
            return;

        var amountToRemove = amount;
        var slotsCount = slotsWithItem.Length;

        for (var i = slotsCount - 1; i >= 0; i--)
        {
            var slot = slotsWithItem[i];
            
            if (slot.Amount >= amountToRemove)
            {
                slot.Item.State.Amount -= amountToRemove;
                
                if (slot.Amount <= 0)
                    slot.Clear();
                
                Debug.Log($"Item removed from inventory. Type = {itemType}, amount = {amountToRemove}");
                OnItemRemoved?.Invoke(sender, itemType, amountToRemove);

                break;
            }

            var amountRemoved = slot.Amount;
            
            amountToRemove -= slot.Amount;
            slot.Clear();
            
            Debug.Log($"Item removed from inventory. Type = {itemType}, amount = {amountToRemove}");
            OnItemRemoved?.Invoke(sender, itemType, amountRemoved);
        }
    }

    public bool HasItem(Type itemType, out IInventotyItem item)
    {
        item = GetItem(itemType);
        
        return item != null;
    }

    public void TransitFromSlotToSlot(object sender, IInventorySlot fromSlot, IInventorySlot toSlot)
    {
        
    }

    private bool TryToAddToSlot(object sender, IInventorySlot slot, IInventotyItem item)
    {
        var isFits = slot.Amount + item.State.Amount <= item.Info.MaxItemsInSlot;
        var amountToAdd = isFits ? item.State.Amount : item.Info.MaxItemsInSlot - slot.Amount;
        var amountLeft = item.State.Amount - amountToAdd;
        var clonedItem = item.Clone();
        clonedItem.State.Amount = amountToAdd;
        
        if (slot.IsEmpty)
            slot.SetItem(clonedItem);
        else
            slot.Item.State.Amount += amountToAdd;
        
        Debug.Log($"Item added to inventory. Type = {item.Type}, amount = {amountToAdd}");
        OnItemAdded?.Invoke(sender, item, amountToAdd);

        if (amountLeft <= 0)
            return true;

        item.State.Amount = amountLeft;
        
        return TryToAdd(sender, item);
    }

    private IInventorySlot[] GetAllSlots(Type itemType) =>
        _slots.FindAll(slot => !slot.IsEmpty && slot.ItemType == itemType).ToArray();
}