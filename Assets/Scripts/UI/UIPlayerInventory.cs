public class UIPlayerInventory : UIPanel
{
    public InventoryWithSlots Inventory => _uiInventoryWithSlots.Inventory;

    private UIInventoryWithSlots _uiInventoryWithSlots;

    public void Setup()
    {
        var uiSlots = GetComponentsInChildren<UIInventorySlot>();
        _uiInventoryWithSlots = new UIInventoryWithSlots(uiSlots);
    }
}