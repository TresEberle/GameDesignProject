using UnityEngine;


[CreateAssetMenu(fileName = "newNPCDialogue", menuName = "NPC Dialogue")]



public class NPC_Dial : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLines;
    public float typingSpeed = 0.5f;

    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;
    public AudioClip voiceSound;
    public float voicePitch = 1f;
    




}
