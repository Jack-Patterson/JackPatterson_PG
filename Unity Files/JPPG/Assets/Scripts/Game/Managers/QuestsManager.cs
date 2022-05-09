using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuestsManager : MonoBehaviour
{
    public static QuestsManager instance;

    internal List<IQuestable> questsList = new List<IQuestable>();

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

    }

    void Update()
    {

    }
}
