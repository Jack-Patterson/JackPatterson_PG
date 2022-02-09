using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HarvestableObject : MonoBehaviour
{
    private int maxStone;
    private int currentStone;

    void Start()
    {
        maxStone = 5;
        currentStone = maxStone;
    }

    void Update()
    {
        
    }

    public void mineStone()
    {
        if (currentStone > 0)
        {
            currentStone--;
            Debug.Log("Stone Mined " + currentStone);
        }
    }
}
