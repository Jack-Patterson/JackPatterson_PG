using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HarvestableObject : MonoBehaviour, IInteractable
{
    public enum Resources { Rock, Gold, Wood, Food, Melee_Skill}
    AICharacterMove myNPC;
    public Resources this_has;
    float amount_of_resource = 1000;
    float rate_of_collection = 2;
    void Start()
    {

    }

    void Update()
    {
        myNPC.give(rate_of_collection * Time.deltaTime/*, this_has*/);
    }

    AICharacterMove.CharacterState AnimationFor
    {
        get
        {
            switch(this_has)
            {

                case Resources.Food:

                    return AICharacterMove.CharacterState.mining;



                case Resources.Rock:

                    return AICharacterMove.CharacterState.mining;



                case Resources.Melee_Skill:

                    return AICharacterMove.CharacterState.practice;


                default:

                    return AICharacterMove.CharacterState.mining;



            }
        }

      
    }

    public AICharacterMove.CharacterState interact(AICharacterMove NPC)
    {
        myNPC = NPC;
        myNPC.Iam(this);
        return AnimationFor; ;
    }
}
