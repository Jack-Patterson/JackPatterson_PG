using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public static ItemsManager instance;

    Items gold;
    Items wood;
    Items stone;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of ItemsManager");
            return;
        }
        instance = this;
    }

    void Start()
    {
        gold = new Items("Gold", 50, 1);
        wood = new Items("Wood", 10, 2);
        stone = new Items("Stone", 10, 3);
    }

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Z))
        {
            wood.buyItem(12, gold.getAmount());
        }
    }
}
