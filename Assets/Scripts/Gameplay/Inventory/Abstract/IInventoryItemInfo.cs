using UnityEngine;

public interface IInventoryItemInfo
{
    string ID { get; }
    string Title { get; }
    string Description { get; }
    int MaxItemsInSlot { get; }
    int Cost { get; }
    SlotType SlotType { get; }
    Sprite SpriteIcon { get; }
}