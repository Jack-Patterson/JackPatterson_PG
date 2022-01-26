using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [Header("Item Amounts")]
    public int goldAmount;
    public int woodAmount;
    private int stoneAmount;

    [Header("Items Costs")]
    public int woodCost;
    private int stoneCost;

    // Start is called before the first frame update
    void Start()
    {
        goldAmount = 50;
        woodAmount = 4;
        woodCost = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void payForItem(int itemCost, int itemAmount, int quantity)
    {
        int totalCost;
        int goldCount = getGoldCount();

        totalCost = itemCost * quantity;

        if (goldCount >= totalCost)
        {
            goldCount -= totalCost;
            itemAmount += quantity;
            Debug.Log(goldCount + " " + itemAmount);
        }
        else
        {
            Debug.LogWarning("Not enough to buy this");
        }
    }

    int getGoldCount()
    {
        return this.goldAmount;
    }
}
