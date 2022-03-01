using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private string itemName;
    private int amount;
    private int cost;
    private ResourcesManager.Resources resourceType;
     
    public Item(string itemName, int amount, int cost, ResourcesManager.Resources resourceType)
    {
        this.itemName = itemName;
        this.amount = amount;
        this.cost = cost;
        this.resourceType = resourceType;
    }
    

    void Start()
    {

    }

    void Update()
    {
        
    }

    public string getName()
    {
        return itemName;
    }

    public int getAmount()
    {
        return amount;
    }

    public int getCost()
    {
        return cost;
    }

    public void setName(string itemName)
    {
        this.itemName = itemName;
    }

    public void setAmount(int amount)
    {
        this.amount = amount;
    }

    public void addAmount (int amount)
    {
        this.amount += amount;
    }

    public void setCost(int cost)
    {
        this.cost = cost;
    }

}
