using UnityEngine;

public class UIInventoryWithSlots
{
    private readonly UIInventorySlot[] _uiSlots;
    
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

    private void OnInventoryStateChanged(object sender)
    {
        foreach (var uiSlot in _uiSlots)
            uiSlot.Refresh();

        if (_uiSlots[0].Slot.IsEmpty) 
            return;
        
        if (_uiSlots[0].Slot.Item.Info.SlotType == SlotType.EquipmentWeapon)
            Debug.Log("Weapon slot updated");
    }
}