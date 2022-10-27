using UnityEngine;

[CreateAssetMenu(menuName = "Data/Items", fileName = "ItemData", order = 51)]
public class InventoryItemData : ScriptableObject, IInventoryItemInfo
{
    [SerializeField] private string _id = "0";
    [SerializeField] private string _title = "Title";
    [SerializeField] private string _description = "Description";
    [SerializeField] private int _maxItemsInSlot = 99;
    [SerializeField] private Sprite _spriteIcon;

    public string ID => _id;
    public string Title => _title;
    public string Description => _description;
    public int MaxItemsInSlot => _maxItemsInSlot;
    public Sprite SpriteIcon => _spriteIcon;
}