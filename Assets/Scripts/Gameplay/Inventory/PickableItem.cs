using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PickableItem : MonoBehaviour, IPickableItem
{
    [SerializeField] private InventoryItemData _itemData;
    [SerializeField] private int _amount;

    public void Pick(IInventory inventory)
    {
        var inventoryItem = new InventoryItem(_itemData)
        {
            State = { Amount = _amount }
        };

        inventory.TryToAdd(this, inventoryItem);
        
        Destroy(gameObject);
    }
}