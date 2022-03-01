using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HarvestableObject : MonoBehaviour, IInteractable
{
    CharacterControl character;
    public ResourcesManager.Resources resource;
    float maxResourceAmount;
    float resourceAmount;
    float collectRate = 1;
    float time;

    bool coroutineStarted = false;
    bool breakCoroutine = false;


    void Start()
    {
        maxResourceAmount = 10;
        resourceAmount = maxResourceAmount;
    }

    void Update()
    {
        if (character == null)
        {
            return;
        }

        if (!coroutineStarted && resourceAmount > 0)
        {
            coroutineStarted = true;
            collectResource();
        }
    }

    CharacterControl.CharacterState AnimationFor
    {
        get
        {
            switch (resource)
            {
                case ResourcesManager.Resources.Food: return CharacterControl.CharacterState.mining;
                case ResourcesManager.Resources.Stone: return CharacterControl.CharacterState.mining;
                case ResourcesManager.Resources.MeleeSkill: return CharacterControl.CharacterState.practice;
                default: return CharacterControl.CharacterState.mining;
            }
        }
    }

    public CharacterControl.CharacterState interact(CharacterControl NPC)
    {
        character = NPC;
        character.IAmHarvestable(this);
        return AnimationFor;
    }

    internal void collectResource()
    {
        StartCoroutine(collectResourceIE(ResourcesManager.instance.getTime(resource)));
    }

    private IEnumerator collectResourceIE(float time)
    {
        yield return new WaitForSeconds(time);

        character.give(collectRate);
        resourceAmount -= collectRate;

        if (resourceAmount <= 0 || breakCoroutine)
        {
            yield break;
        }

        coroutineStarted = false;
    }

    public void BreakCoroutine()
    {
        breakCoroutine = true;
    }

    public ResourcesManager.Resources getResourceType()
    {
        return resource;
    }

}
