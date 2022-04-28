using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    private static int questID = 0;
    private string questName;
    private string questDescription;
    private bool questCompleted;

    public Quest(string questName, string questDescription)
    {
        questID++;
        setQuestName(questName);
        setQuestDescription(questDescription);
        setQuestCompleted(false);
        QuestsManager.instance.addToQuestsList(this);
    }

    internal int getQuestID()
    {
        return questID;
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
