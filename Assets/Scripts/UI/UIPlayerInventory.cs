using System.Collections.Generic;
using UnityEngine;

public class UIPlayerInventory : UIPanel
{
    [SerializeField] private List<UIInventorySlot> _uiEquipmentSlots;
    [SerializeField] private List<UIInventorySlot> _uiInventorySlots;

    private UIInventoryWithSlots _uiInventoryWithSlots;
    
    public InventoryWithSlots Inventory => _uiInventoryWithSlots.Inventory;


    public void Setup()
    {
        var allUiSlots = new List<UIInventorySlot>(_uiEquipmentSlots);
        allUiSlots.AddRange(_uiInventorySlots);
        
        _uiInventoryWithSlots = new UIInventoryWithSlots(allUiSlots.ToArray());
    }
}