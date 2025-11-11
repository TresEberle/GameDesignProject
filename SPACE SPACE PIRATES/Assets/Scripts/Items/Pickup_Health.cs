using UnityEngine;

public class Pickup_Health : PickupBase
{
    public float healAmount = 35f;


    protected override bool ApplyPickup(Collider2D player)
    {
        var ph = player.GetComponent<Player>() ?? player.GetComponentInParent<Player>();
        if (!ph) return false;

        ph.Heal(healAmount);
        return true;
    }
}
