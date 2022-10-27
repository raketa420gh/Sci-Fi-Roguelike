using System.Collections.Generic;
using UnityEngine;

public class UIInventoryCreator
{
    private UIInventorySlot[] _uiSlots;
    private InventoryItemData _junkData;
    private InventoryItemData _pipeData;
    
    public InventoryWithSlots Inventory { get; }

    public UIInventoryCreator(UIInventorySlot[] uiSlots, 
        InventoryItemData junkData, 
        InventoryItemData pipeData)
    {
        _uiSlots = uiSlots;
        _junkData = junkData;
        _pipeData = pipeData;

        Inventory = new InventoryWithSlots(uiSlots.Length);
        Inventory.OnStateChanged += OnInventoryStateChanged;
    }

    private void SetupUIInventory(InventoryWithSlots inventory)
    {
        var allSlots = inventory.GetAllSlots();
        var allSlotsCount = allSlots.Length;

        for (int i = 0; i < allSlotsCount; i++)
        {
            var slot = allSlots[i];
            var uiSlot = _uiSlots[i];
            uiSlot.SetSlot(slot);
            uiSlot.Refresh();
        }
    }

    public void AddItemIntoSlot(IInventorySlot slot, IInventoryItem item)
    {
        Inventory.TryToAddToSlot(this, slot, item);
    }

    public void FillSlotsRandom()
    {
        var allSlots = Inventory.GetAllSlots();
        var availableSlots = new List<IInventorySlot>(allSlots);

        var filledSlots = 5;

        for (int i = 0; i < filledSlots; i++)
        {
            var filledSlot = AddRandomJunkIntoRandomSlot(availableSlots);
            availableSlots.Remove(filledSlot);

            filledSlot = AddRandomPipeIntoRandomSlot(availableSlots);
            availableSlots.Remove(filledSlot);
        }
        
        SetupUIInventory(Inventory);
    }

    private IInventorySlot AddRandomJunkIntoRandomSlot(List<IInventorySlot> slots)
    {
        var rSlotIndex = Random.Range(0, slots.Count);
        var rSlot = slots[rSlotIndex];
        var rCount = Random.Range(1, 20);

        var junk = new ItemJunk(_junkData)
        {
            State = { Amount = rCount }
        };

        Inventory.TryToAddToSlot(this, rSlot, junk);
        
        return rSlot;
    }
    
    private IInventorySlot AddRandomPipeIntoRandomSlot(List<IInventorySlot> slots)
    {
        var rSlotIndex = Random.Range(0, slots.Count);
        var rSlot = slots[rSlotIndex];
        var rCount = Random.Range(1, 20);

        var pipe = new ItemPipe(_pipeData)
        {
            State = { Amount = rCount }
        };

        Inventory.TryToAddToSlot(this, rSlot, pipe);
        
        return rSlot;
    }

    private void OnInventoryStateChanged(object sender)
    {
        foreach (var uiSlot in _uiSlots)
            uiSlot.Refresh();
    }
}