using UnityEngine;

public class Dial_Disable : MonoBehaviour
{

    public GameObject DisabledCall; //this GameObject

    public void onInteract_seq03() {
        DisabledCall.SetActive(false);
        GameManager.instance.isUIPressed = true; 
        GameManager.instance.playerAllowMovement(GameState.Sequence03);
    }

    public void onInteract_()
    {
        DisabledCall.SetActive(false);

    }





}
