using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("General Stats/Info")]
    private int MHP;
    private int CHP;
    private string job;
    private bool isAdventurer;
    private bool isGuildWorker;
    private int level;
    private int maxLevel;
    private int XP;

    [Header("Adventurer Info")]
    private int prowess_combat;
    private GameObject currentWeapon;
    private int[] QuestsCompleted;
    private string rank;

    [Header("Guild Worker Stats")]
    private int prowess_guildStaff;

    void Start()
    {
        maxLevel = 10;
        
        if (level < 1)
        {
            level = 1;
        }
        else if (level !>= 1 && level !<= maxLevel){
            level = 1;
        }

        
    }

    
    void Update()
    {
        if(isAdventurer == true)
           {
               isGuildWorker = false;
           }
        if (isGuildWorker == true)
           {
               isAdventurer = false;
           }

        advanceLevel(level, XP);
    }

    int setHP(int damage, int CHP)
    {
        CHP = CHP - damage;
        return CHP;
    }

    string setJob(string job)
    {
        bool isValidJob = false;
        
        this.job = job;

        if (isValidJob == false)
        {
            return "No assigned job";
        }
        else
        {
            return job;
        }
    }

    void advanceLevel(int level, int xp)
    {
        if (xp < 100)
        {
            return;
        }
        
        if (level >= 1 && level <= 5 && xp >= 100)
        {
            level++;
            setMHP(level, MHP);
        }
        else if (level >= 6 && level <= 8 && xp >= 150)
        {
            level++;
            setMHP(level, MHP);
        }
        else if (level == 9 && xp >= 200)
        {
            level++;
            setMHP(level, MHP);
        }

        if (level > maxLevel)
        {
            level = maxLevel;
        }
    }

    void setMHP(int level, int MHP)
    {
        float levelMod;
        float prowessMod;

        levelMod = level * 0.1f;
        prowessMod = getProwess() * 0.05f;

        if (prowessMod == 0)
        {
            MHP = MHP + (int)levelMod;
        }
        else
        {
            MHP = MHP + (int)levelMod + (int)prowessMod;
        }
    }

    int getProwess()
    {
        int prowess;

        prowess = 0;
        
        if (isAdventurer == true)
        {
            prowess = prowess_combat;
        }
        else if (isGuildWorker == true)
        {
            prowess = prowess_guildStaff;
        }
        
        return prowess;
    }
}
