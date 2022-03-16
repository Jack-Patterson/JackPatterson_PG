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

    GameObject thisGO;

    bool coroutineStarted = false;
    bool breakCoroutine = false;

    Vector3 previousPos;

    void Start()
    {
        thisGO = this.gameObject;

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

        if (resourceAmount <= 0)
        {
            noResources();
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
        StartCoroutine(collectResourceIE(ResourcesManager.instance.getResourceTime(resource)));
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

    private IEnumerator hideObjectIE(float time)
    {
        previousPos = transform.position;

        transform.position = new Vector3(9999, -1000, 9999);
        thisGO.SetActive(false);

        yield return new WaitForSeconds(time);

        thisGO.SetActive(true);
        transform.position = previousPos;
        resourceAmount = maxResourceAmount;
    }

    public void BreakCoroutine()
    {
        breakCoroutine = true;
    }

    public ResourcesManager.Resources getResourceType()
    {
        return resource;
    }

    private void noResources()
    {
        StartCoroutine(hideObjectIE(ResourcesManager.instance.getResourceObjectTime(resource)));
    }

}
