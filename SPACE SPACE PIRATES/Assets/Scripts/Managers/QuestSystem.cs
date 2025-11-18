using UnityEngine;
using System.Collections.Generic;

// This script handles the quests that the player will
// encounter throughout the game.
// Specifically, this script will:
//      be accessible as a string for UI
//      add player quest
//      check if quest has been completed
//      give the player the reward upon completion

public class Quest
{
    public string questName;
    public string description;
    public bool isCompleted;
    public GameObject reward;

    // Quest Information = its name, what it is, is it done, and reward
    public questInfo(string name, string desc, GameObject rewardObj = null)
    {
        questName = name;
        description = desc;
        isCompleted = false;
        reward = rewardObj;
    }

    // Keeps track of the quests status for the UI
    public string getQuestInfo()
    {
        string status;
        if(isCompleted)
        {
            status = "[COMPLETED]";
        }
        else
        {
            status = "[IN PROGRESS]";
        }
        return $"{status} {questName}\n{description}";
    }
}

// Main class to 
public class QuestSystem : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
