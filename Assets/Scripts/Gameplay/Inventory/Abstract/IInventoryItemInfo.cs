using UnityEngine;

public interface IInventoryItemInfo
{
    string ID { get; }
    string Title { get; }
    string Description { get; }
    int MaxItemsInSlot { get; }
    Sprite SpriteIcon { get; }
}