using UnityEngine;
using TMPro;
using Microsoft.Unity.VisualStudio.Editor;

public class CutsceneSpeechManager : MonoBehaviour
{
    public GameObject CutsceneCanvas; //Reference to CutsceneCanvas

    public TMP_Text convoText; //Text component of person speaking

    [TextArea] public string[] speakerDialogueParagraphs; //Actual Speech 

    public bool turnOnCutscene = false; //True if we want to play cutscene, false otherwise

    public int index;

    public bool cutsceneDone = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && turnOnCutscene == true) 
        {
            if (index >= speakerDialogueParagraphs.Length)
            {
                CutsceneCanvas.SetActive(false);

                index = 0;

                cutsceneDone = true;
            }
            else
            {
                convoText.text = speakerDialogueParagraphs[index];

                index++;
            }
        }
    }
}
