using UnityEngine;

[CreateAssetMenu(menuName = "Data/Items", fileName = "ItemData", order = 51)]
public class InventoryItemData : ScriptableObject, IInventoryItemInfo
{
    [SerializeField] private string _id = "0";
    [SerializeField] private string _title = "Title";
    [SerializeField] private string _description = "Description";
    [SerializeField] private int _maxItemsInSlot = 99;
    [SerializeField] private int _cost = 1;
    [SerializeField] private SlotType _slotType = SlotType.Inventory;
    [SerializeField] private Sprite _spriteIcon;

    public string ID => _id;
    public string Title => _title;
    public string Description => _description;
    public int MaxItemsInSlot => _maxItemsInSlot;
    public int Cost => _cost;
    public SlotType SlotType => _slotType;
    public Sprite SpriteIcon => _spriteIcon;
}