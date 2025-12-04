using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    public Iinteractible IinteractibleRange = null;
    public GameObject interactionIcon;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactionIcon.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context) {
        if (context.performed) {
            Debug.Log(context.action.name);
            IinteractibleRange?.Interact();
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Iinteractible interactable) && interactable.canInteract()) {
            IinteractibleRange = interactable;
            interactionIcon.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Iinteractible interactable) && interactable == IinteractibleRange)
        {
            IinteractibleRange = null;
            interactionIcon.SetActive(false);
        }
    }



}
