using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventorySlot : UISlot
{
    [SerializeField] private UIInventoryItem _uiInventoryItem;
    [SerializeField] private UIPlayerInventory _uiPlayerInventory;
    
    public IInventorySlot Slot { get; private set; }

    private void Awake()
    {
        if (_uiPlayerInventory == null)
            _uiPlayerInventory = GetComponentInParent<UIPlayerInventory>();
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
        var inventory = _uiPlayerInventory.Inventory;
        
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