using UnityEngine;

public class Item : PickupBase
{
    public GameObject slotIconPrefab;
    [Header("ID")]
    public int ID;

    protected override bool ApplyPickup(Collider2D player)
    {
        bool added = false;

        if (HotbarController.Instance != null)
        {
        added = HotbarController.Instance.AddItem(slotIconPrefab);
        }

        if (!added && InventoryController.Instance != null)
        {
        added = InventoryController.Instance.AddItem(slotIconPrefab);
        }

        return added;    
    }

    public virtual void UseItem()
    {
        Debug.Log("Using item with ID: " + ID);
    }
}
