using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialogueCanvas;   // The canvas to show when interacting
    public KeyCode interactKey = KeyCode.E;

    private bool playerInRange = false;

    void Start()
    {
        // Make sure the canvas starts hidden
        if (dialogueCanvas != null)
            dialogueCanvas.SetActive(false);
    }

    void Update()
    {
        // Check if the player is close and presses the key
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            ToggleDialogue();
        }
    }

    void ToggleDialogue()
    {
        if (dialogueCanvas != null)
        {
            bool isActive = dialogueCanvas.activeSelf;
            dialogueCanvas.SetActive(!isActive);
        }
    }

    // Detect when the player enters trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something entered: " + other.name);

        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("player is in range");
        }
    }

    // Detect when the player leaves trigger zone
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("player is not in range");
            // Hide canvas when player walks away
            if (dialogueCanvas != null)
                dialogueCanvas.SetActive(false);
        }
    }
}
