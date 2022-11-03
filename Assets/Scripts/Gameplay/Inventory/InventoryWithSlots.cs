using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryWithSlots : IInventory
{
    public event Action<object, IInventoryItem, int> OnItemAdded;
    public event Action<object, Type, int> OnItemRemoved;
    public event Action<object> OnStateChanged;

    public int Capacity { get; set; }
    public bool IsFull => _slots.All(slot => slot.IsFull);

    private List<IInventorySlot> _slots;

    public InventoryWithSlots(int capacity)
    {
        Capacity = capacity;

        _slots = new List<IInventorySlot>(capacity);

        var equipmentWeaponSlot = new InventorySlot(SlotType.EquipmentWeapon);
        var equipmentShieldSlot = new InventorySlot(SlotType.EquipmentShield);
        var equipmentMovementSlot = new InventorySlot(SlotType.EquipmentMovement);
        var equipmentAlternativeSlot = new InventorySlot(SlotType.EquipmentAlternative);

        _slots.Add(equipmentWeaponSlot);
        _slots.Add(equipmentShieldSlot);
        _slots.Add(equipmentMovementSlot);
        _slots.Add(equipmentAlternativeSlot);

        for (var i = 4; i < capacity; i++)
            _slots.Add(new InventorySlot(SlotType.Inventory));
    }

    public IInventorySlot[] GetAllSlots() =>
        _slots.ToArray();

    public IInventoryItem GetItem(Type itemType) =>
        _slots.Find(slot => slot.ItemType == itemType).Item;

    public IInventoryItem[] GetAllItems() =>
        (from slot in _slots where !slot.IsEmpty select slot.Item).ToArray();

    public IInventoryItem[] GetAllItems(Type itemType) =>
        _slots.FindAll(slot => !slot.IsEmpty && slot.ItemType == itemType).Select(slot => slot.Item).ToArray();

    public IInventoryItem[] GetEquippedItems() =>
        _slots.FindAll(slot => !slot.IsEmpty && slot.Item.State.IsEquipped).Select(slot => slot.Item).ToArray();

    public int GetItemAmount(Type itemType) =>
        _slots.FindAll(slot => !slot.IsEmpty && slot.ItemType == itemType).Sum(slot => slot.Amount);

    public bool TryToAdd(object sender, IInventoryItem item)
    {
        var suitableSlot = _slots.Find(slot =>
            !slot.IsEmpty
            && !slot.IsFull
            && slot.ItemType == item.Type
            && slot.SlotType == SlotType.Inventory);

        if (suitableSlot != null)
            return TryToAddToSlot(sender, suitableSlot, item);

        var emptySlot = _slots.Find(slot => slot.IsEmpty && slot.SlotType == SlotType.Inventory);

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
                OnStateChanged?.Invoke(sender);

                break;
            }

            var amountRemoved = slot.Amount;

            amountToRemove -= slot.Amount;
            slot.Clear();

            Debug.Log($"Item removed from inventory. Type = {itemType}, amount = {amountToRemove}");
            OnItemRemoved?.Invoke(sender, itemType, amountRemoved);
            OnStateChanged?.Invoke(sender);
        }
    }

    public bool HasItem(Type itemType, out IInventoryItem item)
    {
        item = GetItem(itemType);

        return item != null;
    }

    public void TransitFromSlotToSlot(object sender, IInventorySlot fromSlot, IInventorySlot toSlot)
    {
        if (fromSlot.IsEmpty)
            return;

        if (toSlot.IsFull)
            return;

        if (!toSlot.IsEmpty && fromSlot.ItemType != toSlot.ItemType)
            return;
        
        if (toSlot.SlotType != SlotType.Inventory)
                if(toSlot.SlotType != fromSlot.Item.Info.SlotType)
                    return;

        var fromSlotCapacity = fromSlot.Capacity;
        var isFits = fromSlot.Amount + toSlot.Amount <= fromSlotCapacity;
        var amountToAdd = isFits ? fromSlot.Amount : fromSlotCapacity - toSlot.Amount;
        var amountLeft = fromSlot.Amount - amountToAdd;

        if (toSlot.IsEmpty)
        {
            toSlot.SetItem(fromSlot.Item);
            fromSlot.Clear();
            
            OnStateChanged?.Invoke(sender);
        }
        
        toSlot.Item.State.Amount += amountToAdd;
        
        if(isFits)
            fromSlot.Clear();
        else
            fromSlot.Item.State.Amount = amountLeft;
        
        OnStateChanged?.Invoke(sender);
    }

    public bool TryToAddToSlot(object sender, IInventorySlot slot, IInventoryItem item)
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
        OnStateChanged?.Invoke(sender);

        if (amountLeft <= 0)
            return true;

        item.State.Amount = amountLeft;
        
        return TryToAdd(sender, item);
    }

    private IInventorySlot[] GetAllSlots(Type itemType) =>
        _slots.FindAll(slot => !slot.IsEmpty && slot.ItemType == itemType).ToArray();
}