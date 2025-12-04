using UnityEngine;

public class ItemDictionary : MonoBehaviour
{
    [System.Serializable]
    public class ItemEntry
    {
        public int id;
        public string itemName;
        public PickupBase pickupPrefab;   
    }

    public ItemEntry[] items;

    public void UseItem(int id)
    {
        ItemEntry entry = null;
        foreach (var e in items)
        {
            if (e.id == id)
            {
                entry = e;
                break;
            }
        }

        if (entry == null)
        {
            Debug.LogWarning($"ItemDictionary: No item found for ID {id}");
            return;
        }


        Debug.Log($"[UseItem] Used item: {entry.itemName} (ID {id})");
        Debug.Log($"[UseItem] pickupPrefab on entry = {entry.pickupPrefab}");
        Debug.Log($"Used item: {entry.itemName} (ID {id})");
    
        WeaponItem weaponPickup = entry.pickupPrefab as WeaponItem;
        if (weaponPickup != null && weaponPickup.weaponPrefab != null)
        {
        Player player = FindObjectOfType<Player>();

        WeaponHolder holder = player.GetComponentInChildren<WeaponHolder>();
        if (holder == null)
        {
            Debug.LogWarning("ItemDictionary: No WeaponHolder found on Player.");
            return;
        }
        
        Debug.Log($"[UseItem] Equipping weapon prefab: {weaponPickup.weaponPrefab.name}");

        holder.EquipWeapon(weaponPickup.weaponPrefab, weaponPickup.weaponOffset);
        }

    }
}
