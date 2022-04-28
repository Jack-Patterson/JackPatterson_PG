using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsManager : MonoBehaviour
{
    public static QuestsManager instance;

    Quest assignJob;
    Quest mineResource;
    Quest storeItem;

    List<Quest> quests = new List<Quest>();


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of Manager");
            return;
        }
        instance = this;
    }

    void Start()
    {
        assignJob = new Quest("Assign a villager a job", ".");
        mineResource = new Quest("Mine a resource", ".");
        storeItem = new Quest("Store an item", ".");
    }

    void Update()
    {
        
    }

    internal void addToQuestsList(Quest quest)
    {
        quests.Add(quest);
    }

    internal List<Quest> getQuestsList()
    {
        return quests;
    }
}
