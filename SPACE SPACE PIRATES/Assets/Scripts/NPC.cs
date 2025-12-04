using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour,Iinteractible
{
    public NPC_Dial dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;
    private bool isPaused = false;
   

    public bool canInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        

        if (dialogueData.npcName == null) //|| isPaused == true && isDialogueActive == false
        {
            Debug.Log("dialogueData == null || !isDialogueActive");
            return;
        }


        if (isDialogueActive)
        {
            Debug.Log("NextLine();");
            NextLine();
        }
        else 
        {
            Debug.Log("//start");
            
            StartDialogue();
        }
        

    }

    void NextLine() 
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else {
            EndDialogue(); //_End Dialogue...
        }
    }



    void StartDialogue() 
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        dialoguePanel.SetActive(true);

        nameText.SetText(dialogueData.npcName);
        portraitImage.sprite = dialogueData.npcPortrait;

        
        //pause player movement

        StartCoroutine(TypeLine());

    }

    IEnumerator TypeLine() 
    { 
        isTyping = true;
        dialogueText.SetText("");

        foreach (char letter in dialogueData.dialogueLines[dialogueIndex]) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex]) {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();

        }
    
    }

    public void EndDialogue() {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
        //pause pLAYER
    }



}
