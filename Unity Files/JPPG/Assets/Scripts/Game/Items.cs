using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    private string itemName;
    private int amount;
    private int cost;
     
    public Items(string itemName, int amount, int cost)
    {
        this.itemName = itemName;
        this.amount = amount;
        this.cost = cost;
    }
    

    void Start()
    {

    }

    void Update()
    {
        
    }

    public string getName()
    {
        return name;
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

    public void setCost(int cost)
    {
        this.cost = cost;
    }

    public void buyItem(int quantity, int totalGold)
    {
        int buyCost = getCost();
        int totalAmount = getAmount();
        int totalCost;

        Debug.Log("Amount " + getAmount());
        Debug.Log("Cost " + getCost());

        totalCost = buyCost * quantity;
        
        if (totalCost > totalGold)
        {
            Debug.LogError("Not enough gold");
            return;
        }

        totalGold -= totalCost;
        Debug.Log(totalGold);

        setAmount(totalAmount + quantity);
        Debug.Log(getAmount());
    }

}
