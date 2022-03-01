using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public static ResourcesManager instance;
    public enum Resources {Stone, Gold, Wood, Food, MeleeSkill}

    internal Item stone = new Item("Stone", 0, 20, Resources.Stone);
    internal Item gold = new Item("Gold", 0, 1, Resources.Gold);
    internal Item wood = new Item("Wood", 0, 20, Resources.Wood);
    internal Item food = new Item("Food", 0, 20, Resources.Food);
    internal Item meleeSkill = new Item("Melee Skill", 0, 20, Resources.MeleeSkill);

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of ResourcesManager");
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

    public Item getResourceItem(Resources resource)
    {
        switch (resource)
        {
            case Resources.Stone: return stone;
            case Resources.Wood: return wood;
            case Resources.Gold: return gold;
            case Resources.Food: return food;
            case Resources.MeleeSkill: return meleeSkill;
            default: return null;
        }
    }

    public float getTime(Resources resource)
    {
        switch (resource)
        {
            case Resources.Food: return 2f;
            case Resources.Stone: return 5f;
            case Resources.MeleeSkill: return 2f;
            case Resources.Wood: return 3f;
            case Resources.Gold: return 1f;
            default: return 3f;
        }
    }
}
