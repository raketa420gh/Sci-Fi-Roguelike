using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController : UIPanel
{
    [SerializeField] private List<UIInventorySlot> _uiEquipmentSlots;
    [SerializeField] private List<UIInventorySlot> _uiInventorySlots;

    private UIInventoryWithSlots _uiInventoryWithSlotsWithSlots;
    
    public UIInventoryWithSlots UIInventoryWithSlots => _uiInventoryWithSlotsWithSlots;

    public void Setup()
    {
        var allUiSlots = new List<UIInventorySlot>(_uiEquipmentSlots);
        allUiSlots.AddRange(_uiInventorySlots);
        
        _uiInventoryWithSlotsWithSlots = new UIInventoryWithSlots(allUiSlots.ToArray());
    }
}