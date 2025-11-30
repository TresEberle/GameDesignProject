using UnityEngine;

public class InvItemPickup : PickupBase
{
    public GameObject itemPrefab;

    protected override bool ApplyPickup(Collider2D player)
    {
        return InventoryController.Instance.AddItem(itemPrefab);
    }
}
