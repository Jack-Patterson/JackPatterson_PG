using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI itemText;

    List<Item> itemsList;
    
    void Start()
    {
        itemsList = ResourceManager.instance.getItemList();
    }

    void Update()
    {
        assignItemsToUI();
    }

    private void setText(string text)
    {
        itemText.text = text;
    }

    private void assignItemsToUI()
    {
        foreach(Item i in itemsList)
        {
            GameObject g = GameObject.Find(i.getName());
            
            if (g != null)
            {
                itemText = g.GetComponentInChildren<TextMeshProUGUI>();
                setText(i.getAmount().ToString());
            }
        }
    }
}
