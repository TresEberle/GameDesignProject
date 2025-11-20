using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{

    public GameObject menuCanvas;
    private PlayerInputSet inputActions;
    
    private void Awake()
    {
        inputActions = new PlayerInputSet();

        inputActions.UI.ToggleMenu.performed += ctx => ToggleMenu();
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
    }

    private void OnDisable()
    {
        inputActions.UI.Disable();
    }

    private void Start()
    {
        menuCanvas.SetActive(false);
    }

    private void ToggleMenu()
    {
        menuCanvas.SetActive(!menuCanvas.activeSelf);
    }
}
