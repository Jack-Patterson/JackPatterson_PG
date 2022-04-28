using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private string itemName;
    private float amount;
    private int cost;
    private ResourceManager.Resource resourceType;
     
    public Item(string itemName, float amount, int cost, ResourceManager.Resource resourceType)
    {
        setName(itemName);
        setAmount(amount);
        setCost(cost);
        setResource(resourceType);
        ResourceManager.instance.addItemToList(this);
    }
    
    public Item(string itemName, float amount, int cost)
    {
        setName(itemName);
        setAmount(amount);
        setCost(cost);
        ResourceManager.instance.addItemToList(this);
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

    public float getAmount()
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

    public void setAmount(float amount)
    {
        this.amount = amount;
    }

    public void addAmount (float amount)
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
