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

        // Now actually do the effect. Here we just need a switch(int id) case
        // we just need to come up with some effects and add them to different id's in the switch case

        Debug.Log($"Used item: {entry.itemName} (ID {id})");
    }
}
