using System;

public class UIInventoryWithSlots
{
    public event Action<Type> OnWeaponEquipped;
    
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

        var weaponEquipmentSlot = _uiSlots[0];
        var shieldEquipmentSlot = _uiSlots[1];
        var movementEquipmentSlot = _uiSlots[2];
        var alternativeEquipmentSlot = _uiSlots[3];

        switch (weaponEquipmentSlot.Slot.IsEmpty)
        {
            case true: 
                return;
            case false:
            {
                OnWeaponEquipped?.Invoke(weaponEquipmentSlot.Slot.Item.GetType());
                break;
            }
        }
    }
}