using UnityEngine;

public class UIInventoryWithSlots : MonoBehaviour
{
    [SerializeField] private InventoryItemData _junkData;
    [SerializeField] private InventoryItemData _pipeData;

    public InventoryWithSlots Inventory => _uiInventoryTester.Inventory;

    private UIInventoryTester _uiInventoryTester;

    private void Start() => 
        CreateTester();

    public void CreateTester()
    {
        var uiSlots = GetComponentsInChildren<UIInventorySlot>();
        _uiInventoryTester = new UIInventoryTester(uiSlots, _junkData, _pipeData);
        _uiInventoryTester.FillSlotsRandom();
    }
}