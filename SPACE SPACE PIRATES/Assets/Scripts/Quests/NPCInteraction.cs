using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialogueCanvas;   // The canvas to show when interacting
    public KeyCode interactKey = KeyCode.E;

    [Header("Give Quest")]
    public QuestSystem questSystem;
    public ConnectStarsQuest starQuest;
    public string questName = "Connect the Stars";
    public string questDescr = "Connect the stars to reveal the door code.";

    [Header("Dialogue")]
    public bool hasGiven = false;

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

    void giveQuest()
    {
        if(hasGiven)
        {
            return;
        }

        hasGiven = true;

        if(questSystem != null)
        {
            questSystem.addQuest(questName, questDescr);
        }

        if(starQuest != null)
        {
            starQuest.startQuest();
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
