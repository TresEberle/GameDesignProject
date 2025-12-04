using UnityEngine;

public class WeaponItem : Item
{
    [Header("Weapon")]
    public GameObject weaponPrefab; 

    protected override bool ApplyPickup(Collider2D player)
    {
        bool added = base.ApplyPickup(player);

        // equip weapon on the Player
        Player playerComp = player.GetComponent<Player>() ?? player.GetComponentInParent<Player>();
        if (playerComp != null && weaponPrefab != null)
        {
            WeaponHolder holder = playerComp.GetComponentInChildren<WeaponHolder>();
            autoDespawnAfter = 0f;
            if (holder != null)
            {
                holder.EquipWeapon(weaponPrefab);
            }
            else
            {
                Debug.LogWarning("WeaponItem: No WeaponHolder found on player.");
            }
        }

        return added;
    }

    public override void UseItem()
    {
        Debug.Log("Using weapon item with ID: " + ID);

        Player playerComp = FindObjectOfType<Player>();
        if (playerComp != null && weaponPrefab != null)
        {
            WeaponHolder holder = playerComp.GetComponentInChildren<WeaponHolder>();
            if (holder != null)
            {
                holder.EquipWeapon(weaponPrefab);
            }
        }
    }
}
