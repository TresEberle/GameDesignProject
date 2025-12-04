using UnityEngine;

public class WeaponItem : Item
{
    [Header("Weapon")]
    public GameObject weaponPrefab; 
    
    [Header("Hold Offset")]
    public Vector3 weaponOffset;
protected override bool ApplyPickup(Collider2D player)
{
    // Just add to hotbar/inventory via Item.ApplyPickup
    bool added = base.ApplyPickup(player);

    // Equipping will be handled by ItemDictionary.UseItem when the hotbar key is pressed.

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
                holder.EquipWeapon(weaponPrefab, weaponOffset);
            }
        }
    }
}
