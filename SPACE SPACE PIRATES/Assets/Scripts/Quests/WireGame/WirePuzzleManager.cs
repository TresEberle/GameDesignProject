using UnityEngine;

public class WirePuzzleManager : MonoBehaviour
{
    [Header("Panel to hide when solved")]
    [SerializeField] private GameObject laserGamePanel;  

    [Header("Optional: Player movement to re-enable")]
    [SerializeField] private Player playerScript;        // drag your Player here if you want to unfreeze

    private WireScript[] wires;

    private void Awake()
    {
        wires = GetComponentsInChildren<WireScript>();

    }

    public void CheckIfSolved()
    {
        foreach (var wire in wires)
        {
            if (!wire.IsInCorrectRotation())
            {
                return;
            }
        }

        OnPuzzleSolved();
    }

    private void OnPuzzleSolved()
    {
        Debug.Log("Wire puzzle solved!");

        if (laserGamePanel != null)
            laserGamePanel.SetActive(false);

        if (playerScript != null)
            playerScript.enabled = true;

        // TODO: Hook into quest system here:
        GameManager.instance.UpdateGameState(GameState.Sequence03);
    }
}
