using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HarvestableObject : MonoBehaviour, IInteractable
{
    CharacterControl character;
    [SerializeField]
    private ResourceManager.Resource resource;
    
    float maxResourceAmount;
    float resourceAmount;
    float collectRate = 1;

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
                case ResourceManager.Resource.Food: return CharacterControl.CharacterState.mining;
                case ResourceManager.Resource.Stone: return CharacterControl.CharacterState.mining;
                case ResourceManager.Resource.MeleeSkill: return CharacterControl.CharacterState.practice;
                default: return CharacterControl.CharacterState.mining;
            }
        }
    }

    public ResourceManager.Resource interact(CharacterControl NPC)
    {
        character = NPC;
        character.IAmHarvestable(this);
        return resource;
    }

    internal void collectResource()
    {
        StartCoroutine(collectResourceIE(ResourceManager.instance.getResourceTime(resource)));
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

    public ResourceManager.Resource getResourceType()
    {
        return resource;
    }

    private void noResources()
    {
        StartCoroutine(hideObjectIE(ResourceManager.instance.getResourceObjectTime(resource)));
    }

    internal ResourceManager.Resource GetResource()
    {
        return resource;
    }

}
