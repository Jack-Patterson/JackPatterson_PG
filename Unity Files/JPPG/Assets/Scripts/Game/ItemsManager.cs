using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public static ItemsManager instance;

    

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
        // initialising variables
        /*goldAmount = 50;
        woodAmount = 4;
        woodCost = 5;*/
        /*stoneAmount = 0;
        
        stoneCost = 0;*/
    }

    void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.Q)){
            Items.payForItem(Items.woodCost, Items.woodAmount, 3);
        }*/
    }

    /*void addItemAmount (int item, int amount)
    {
        item += amount;
    }*/

    
}
