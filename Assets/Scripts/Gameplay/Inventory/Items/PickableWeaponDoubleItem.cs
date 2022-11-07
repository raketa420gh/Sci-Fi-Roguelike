using UnityEngine;

public class PickableWeaponDoubleItem : MonoBehaviour, IPickableItem
{
    [SerializeField] protected InventoryItemData _itemData;
    [SerializeField] protected int _amount;

    public virtual void Pick(IInventory inventory)
    {
        var inventoryItem = new WeaponDoubleItem(_itemData)
        {
            State = { Amount = _amount }
        };

        inventory.TryToAdd(this, inventoryItem);
        
        Destroy(gameObject);
    }
}