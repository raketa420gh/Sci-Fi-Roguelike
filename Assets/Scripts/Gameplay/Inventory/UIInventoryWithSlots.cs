using System.Collections.Generic;
using UnityEngine;

public class UIInventoryWithSlots
{
    private UIInventorySlot[] _uiSlots;
    
    public InventoryWithSlots Inventory { get; }

    public UIInventoryWithSlots(UIInventorySlot[] uiSlots)
    {
        _uiSlots = uiSlots;

        Inventory = new InventoryWithSlots(uiSlots.Length);
        Inventory.OnStateChanged += OnInventoryStateChanged;
        
        Setup(Inventory);
    }

    private void Setup(InventoryWithSlots inventory)
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

    public void FillSlotsRandomJunk()
    {
        var allSlots = Inventory.GetAllSlots();
        var availableSlots = new List<IInventorySlot>(allSlots);

        var filledSlots = 5;

        for (int i = 0; i < filledSlots; i++)
        {
            var filledSlot = AddRandomJunkIntoRandomSlot(availableSlots);
            availableSlots.Remove(filledSlot);
        }
    }

    private IInventorySlot AddRandomJunkIntoRandomSlot(List<IInventorySlot> slots)
    {
        var rSlotIndex = Random.Range(0, slots.Count);
        var rSlot = slots[rSlotIndex];
        var rCount = Random.Range(1, 20);

        //var junk = new ItemJunk(_junkData)
        //{
       //     State = { Amount = rCount }
        //};

        //Inventory.TryToAddToSlot(this, rSlot, junk);
        
        return rSlot;
    }

    private void OnInventoryStateChanged(object sender)
    {
        foreach (var uiSlot in _uiSlots)
            uiSlot.Refresh();
    }
}