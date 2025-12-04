using UnityEngine;
using UnityEngine.InputSystem;

public class WirePuzzleActivator : MonoBehaviour,Iinteractible
{

    [Header("playerPrompt")]
    [SerializeField] public GameObject playerPrompt;

    [Header("Wire Puzzle Panel (UI)")]
    public GameObject laserGamePanel;   

    [Header("Player movement script to disable/enable")]
    public Player playerScript;       

    private bool playerInRange;

    public bool canInteract()
    {
       return !playerInRange;
    }

    public void Interact()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerPrompt.SetActive(true);
            // can show a "Press E" prompt here
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            // If player walks away (which they shouldn't) close the puzzle if it's open
            if (laserGamePanel != null)
                laserGamePanel.SetActive(false);

            if (playerScript != null)
                playerScript.enabled = true; //movement is re-enabled

            playerPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        if (!playerInRange) return;
        if (Keyboard.current == null) return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (laserGamePanel != null)
                laserGamePanel.SetActive(true);

            if (playerScript != null)
                playerScript.enabled = false; // freeze player while puzzle is open
        }
    }
}
