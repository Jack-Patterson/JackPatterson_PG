using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestsUI : MonoBehaviour
{
    public static QuestsUI instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of Quest UI");
            return;
        }
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        addQuests();
    }

    private void addQuests()
    {
        int i = 1;
        foreach (Quest q in QuestsManager.instance.getQuestsList())
        {
            TextMeshProUGUI questText = GameObject.Find("QuestText1").GetComponent<TextMeshProUGUI>();

        }
    }
}
