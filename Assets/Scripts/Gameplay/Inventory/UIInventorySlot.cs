using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventorySlot : UISlot
{
    [SerializeField] private UIInventoryItem _uiInventoryItem;
    private UIInventoryWithSlots _uiInventory;
    
    public IInventorySlot Slot { get; private set; }

    private void Awake()
    {
        _uiInventory = GetComponentInParent<UIInventoryWithSlots>();
    }

    public void SetSlot(IInventorySlot slot)
    {
        Slot = slot;
    }
    
    public override void OnDrop(PointerEventData eventData)
    {
        var otherItemUI = eventData.pointerDrag.GetComponent<UIInventoryItem>();
        var otherSlotUI = otherItemUI.GetComponentInParent<UIInventorySlot>();
        var otherSlot = otherSlotUI.Slot;
        var inventory = _uiInventory.Inventory;
        
        inventory.TransitFromSlotToSlot(this, otherSlot, Slot);

        Refresh();
        otherSlotUI.Refresh();
    }

    public void Refresh()
    {
        if (Slot!=null)
            _uiInventoryItem.Refresh(Slot);
    }
}