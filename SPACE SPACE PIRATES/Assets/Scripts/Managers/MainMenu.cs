using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public GameObject panel;
    bool isOpen;

    public void Awake()
    {
        isOpen = panel.activeInHierarchy;
    }

    public void panelOpenClose() {

        panel.SetActive(!isOpen);
        isOpen = !isOpen;
    }



}
