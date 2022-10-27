using UnityEngine;

public class UIPlayerInventory : UIPanel
{
    [SerializeField] private InventoryItemData _junkData;
    [SerializeField] private InventoryItemData _pipeData;

    public InventoryWithSlots Inventory => _uiInventoryCreator.Inventory;

    private UIInventoryCreator _uiInventoryCreator;

    public void CreateInventory()
    {
        var uiSlots = GetComponentsInChildren<UIInventorySlot>();
        _uiInventoryCreator = new UIInventoryCreator(uiSlots, _junkData, _pipeData);
        _uiInventoryCreator.FillSlotsRandom();
    }
}