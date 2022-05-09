using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestsUI : MonoBehaviour
{
    public static QuestsUI instance;

    [SerializeField] private GameObject questObject1;
    [SerializeField] private GameObject questObject2;
    [SerializeField] private GameObject questObject3;

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
        
    }

    private void assignQuests()
    {

    }
}
