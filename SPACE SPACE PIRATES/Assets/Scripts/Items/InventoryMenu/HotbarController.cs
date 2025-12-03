using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarController : MonoBehaviour
{
    public GameObject hotbarPanel;
    public GameObject slot;
    public int slotCount = 9;

    public GameObject[] itemPrefabs;   

    private ItemDictionary itemDictionary;
    private Key[] hotbarKeys;

    private void Awake()
    {
        itemDictionary = FindObjectOfType<ItemDictionary>();
        hotbarKeys = new Key[slotCount];
        for(int i =0; i < slotCount; i++)
        {
            hotbarKeys[i] = (Key)((int)Key.Digit1 + i);
        }

        for (int i = 0; i < slotCount; i++)
        {
            // Create a new slot under the hotbarPanel
            GameObject slotGO = Instantiate(slot, hotbarPanel.transform);
            slotGO.name = $"HotbarSlot_{i}";

            Slot slotComp = slotGO.GetComponent<Slot>();

            // If we have an item prefab for this index, put it in the slot
            if (itemPrefabs != null && i < itemPrefabs.Length && itemPrefabs[i] != null)
            {
                GameObject item = Instantiate(itemPrefabs[i], slotGO.transform);
                RectTransform itemRect = item.GetComponent<RectTransform>();
                if (itemRect != null)
                {
                    itemRect.anchoredPosition = Vector2.zero;
                }

                slotComp.currentItem = item;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        for(int i =0; i < slotCount; i++)
        {
            if(Keyboard.current[hotbarKeys[i]].wasPressedThisFrame)
            {
                UseItemInSlot(i);
            }
        }
    }

    void UseItemInSlot(int index)
    {

    if (index < 0 || index >= hotbarPanel.transform.childCount) return;
    Slot slot = hotbarPanel.transform.GetChild(index).GetComponent<Slot>();
    if (slot == null)
    {
        Debug.LogWarning($"HotbarController: No Slot component on child {index}");
        return;
    }

    if (slot.currentItem == null)
    {
        Debug.Log($"HotbarController: Slot {index} is empty.");
        return;
    }

    ItemDragHandler dragHandler = slot.currentItem.GetComponent<ItemDragHandler>();
    if (dragHandler == null)
    {
        Debug.LogWarning($"HotbarController: Item in slot {index} has no ItemDragHandler.");
        return;
    }

    dragHandler.UseItem();
    }
}
