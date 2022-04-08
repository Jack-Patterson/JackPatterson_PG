using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private string itemName;
    private int amount;
    private int cost;
    private ResourceManager.Resource resourceType;
     
    public Item(string itemName, int amount, int cost, ResourceManager.Resource resourceType)
    {
        setName(itemName);
        setAmount(amount);
        setCost(cost);
        setResource(resourceType);
    }
    
    public Item(string itemName, int amount, int cost)
    {
        setName(itemName);
        setAmount(amount);
        setCost(cost);
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

    public ResourceManager.Resource GetResource()
    {
        return resourceType;
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

    public void setResource(ResourceManager.Resource resource)
    {
        this.resourceType = resource;
    }

}
