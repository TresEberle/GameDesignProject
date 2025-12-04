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


        Debug.Log($"Used item: {entry.itemName} (ID {id})");
    
        WeaponItem weaponPickup = entry.pickupPrefab as WeaponItem;
        if (weaponPickup != null && weaponPickup.weaponPrefab != null)
        {
        Player player = FindObjectOfType<Player>();

        WeaponHolder holder = player.GetComponentInChildren<WeaponHolder>();
        
        Debug.Log($"[UseItem] Equipping weapon prefab: {weaponPickup.weaponPrefab.name}");

        holder.EquipWeapon(weaponPickup.weaponPrefab, weaponPickup.weaponOffset);
        }

        Player player1 = FindObjectOfType<Player>();


    // Handle effects by ID
        switch (id)
        {
        // example: health item ID = 3
        case 1:
            float healAmount = 10f;   // tweak this value as you like
            player1.Heal(healAmount);
            Debug.Log($"Healed player by {healAmount}. Current health = {player1.health}");
            break;

        case 4:
            float speedBoost = 4f; // tweak this value as you like
            float boostDuration = 5f; // seconds
            player1.ApplySpeedBoost(speedBoost, boostDuration);
            Debug.Log($"Applied speed boost of {speedBoost} for {boostDuration} seconds.");
            break;
        }
    }
}
