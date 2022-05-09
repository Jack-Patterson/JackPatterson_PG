using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest2 : MonoBehaviour, IQuestable
{
    public static Quest2 instance;
    
    private string questName;
    private string questDescription;
    private bool questCompleted;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError($"More than one instance of this quest.");
            return;
        }
        instance = this;

        QuestsManager.instance.questsList.Add(this);
    }

    private void Start()
    {
        setQuestCompleted(false);
        setQuestName("Mine a resource.");
        setQuestDescription("Allow the villager to mine a resource.");
    }

    public void interact()
    {
        setQuestCompleted(true);
    }

    internal string getQuestName()
    {
        return questName;
    }

    internal string getQuestDescription()
    {
        return questDescription;
    }

    internal bool getQuestCompleted()
    {
        return questCompleted;
    }

    internal void setQuestName(string questName)
    {
        this.questName = questName;
    }

    internal void setQuestDescription(string questDescription)
    {
        this.questDescription = questDescription;
    }

    internal void setQuestCompleted(bool questCompleted)
    {
        this.questCompleted = questCompleted;
    }


}
