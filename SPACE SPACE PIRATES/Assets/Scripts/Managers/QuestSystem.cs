using UnityEngine;
using System.Collections.Generic;

// This script handles the quests that the player will
// encounter throughout the game.
// Specifically, this script will:
//      be accessible as a string for UI
//      add player quest
//      check if quest has been completed
//      give the player the reward upon completion


// This is meant to represent each individual quest.
// Assumes the quest will have a name, a further description of what it is,
// whether it's done or not, and the associated reward to it.
public class Quest
{
    public string questName;
    public string description;
    public bool isCompleted;
    public GameObject reward;

    // Quest Information = its name, what it is, is it done, and reward
    public Quest(string name, string desc, GameObject rewardObj = null)
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

// Main class to handle all quests of the game.
public class QuestSystem : MonoBehaviour
{
    // Player will be working on multiple quests at a time.
    // So, need to keep track of what quests are working on currently
    // and the quests they have successfully completed.
    public List<Quest> activeQuests = new List<Quest>();
    public List<Quest> completedQuests = new List<Quest>();

    // Add the quest to the player's to-do list to help them keep track
    // of how to progress through the game.
    public void addQuest(string questName, string description, GameObject reward = null)
    {
        Quest newQuest = new Quest(questName, description, reward);
        activeQuests.Add(newQuest);        
    }

    // This checks a specific quest if it is done yet or not.
    public bool isQuestCompleted(string questName)
    {
        foreach(Quest quest in completedQuests)
        {
            if(quest.questName == questName)
            {
                return true;
            }
        }
        return false;
    }

    // Once the player has finished the quest,
    // mark it complete and reward the player.
    public void completeQuest(string questName)
    {
        Quest questToComplete = null;

        foreach(Quest quest in activeQuests)
        {
            if(quest.questName == questName)
            {
                questToComplete = quest;
                break;
            }
        }

        if(questToComplete != null)
        {
            questToComplete.isCompleted = true;

            // Reward the player
            if(questToComplete.reward != null)
            {
                rewardPlayer(questToComplete.reward);
            }

            // Now need to remove it from the to-do list,
            // and instead mark it as complete.
            activeQuests.Remove(questToComplete);
            completedQuests.Add(questToComplete);
        }
        else
        {
            //might need add debug statements ?
        }
    }

    // Give the player the reward 
    private void rewardPlayer(GameObject reward)
    {
        // Each quest may be different, that is,
        // not all quests might be a physical reward given.
        // For now will just assume it is an object.
        Instantiate(reward, transform.position, transform.rotation);
    }

    // Need to get the currently active quests that the player is working on.
    // Meant for UI purposes.
    public string getActiveQuests()
    {
        if(activeQuests.Count == 0)
        {
            return "No current active quests.";
        }

        string questList = "Active Quests:\n\n";
        foreach(Quest quest in activeQuests)
        {
            questList += quest.getQuestInfo() + "\n\n";
        }
        return questList;
    }

    // Need to get the currently completed quests that the player has finished.
    // Meant for UI purposes.
    public string getCompleteQuests()
    {
        if(completedQuests.Count == 0)
        {
            return "No current completed quests.";
        }

        string questList = "Completed Quests:\n\n";
        foreach(Quest quest in completedQuests)
        {
            questList += quest.getQuestInfo() + "\n\n";
        }
        return questList;
    }
}
