using UnityEngine;
using Random = UnityEngine.Random;

public class InventoryTester : MonoBehaviour
{
    private IInventory _inventory;

    private void Awake()
    {
        _inventory = new InventoryWithSlots(100);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            AddRandomItemJunk();

        if (Input.GetKeyDown(KeyCode.O))
            RemoveRandomItemJunk();
    }

    private void AddRandomItemJunk()
    {
        var rCount = Random.Range(1, 10);

        var itemJunk = new ItemJunk(ScriptableObject.CreateInstance<InventoryItemData>());

        _inventory.TryToAdd(this, itemJunk);
    }

    private void RemoveRandomItemJunk()
    {
        var rCount = Random.Range(1, 10);
        
        _inventory.Remove(this, typeof(ItemJunk), rCount);
    }
}