using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PickableJunkItem : MonoBehaviour, IPickableItem
{
    [SerializeField] protected InventoryItemData _itemData;
    [SerializeField] protected int _amount;

    public virtual void Pick(IInventory inventory)
    {
        var inventoryItem = new JunkItem(_itemData)
        {
            State = { Amount = _amount }
        };

        inventory.TryToAdd(this, inventoryItem);
        
        Destroy(gameObject);
    }
}