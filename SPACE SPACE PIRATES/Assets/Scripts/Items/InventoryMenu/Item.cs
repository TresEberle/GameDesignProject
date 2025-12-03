using UnityEngine;

public class Item : PickupBase
{
    public GameObject slotIconPrefab;
    [Header("ID")]
    public int ID;

    protected override bool ApplyPickup(Collider2D player)
    {
        return InventoryController.Instance.AddItem(slotIconPrefab);
    }

    public virtual void UseItem()
    {
        Debug.Log("Using item with ID: " + ID);
    }
}
