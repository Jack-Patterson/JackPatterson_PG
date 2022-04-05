using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    [SerializeField]
    private ResourceManager.Resource resource;
    private Item resourceItem;

    [SerializeField]
    private TextMeshProUGUI itemText;

    private float time = 2;
    
    void Start()
    {
     
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            resourceItem = ResourceManager.instance.getResourceItem(resource);

        time -= Time.deltaTime;
        if (time <= 0)
        {
            setText(resourceItem.getAmount().ToString());
            time = 2;
        }
    }

    private void setText(string text)
    {
        itemText.text = text;
    }
}
