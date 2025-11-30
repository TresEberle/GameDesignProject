using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance { get; private set; }
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Multiple InventoryController instances found. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        for (int i = 0 ; i<slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
            if (i < itemPrefabs.Length)
            {
                GameObject item = Instantiate(itemPrefabs[i], slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = item;
            }
        }
    }

        public bool AddItem(GameObject itemPrefab)
    {
        if (itemPrefab == null)
        {
            Debug.LogWarning("InventoryController.AddItem called with null prefab.");
            return false;
        }

        // Look through all children of inventoryPanel for a Slot with no item
        foreach (Transform child in inventoryPanel.transform)
        {
            Slot slot = child.GetComponent<Slot>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject item = Instantiate(itemPrefab, slot.transform);
                var rect = item.GetComponent<RectTransform>();
                if (rect != null)
                    rect.anchoredPosition = Vector2.zero;

                slot.currentItem = item;
                return true;
            }
        }

        Debug.Log("Inventory full, cannot add item: " + itemPrefab.name);
        return false;
    }
}
