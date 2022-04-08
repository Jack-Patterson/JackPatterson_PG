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

    bool coroutineStarted = false;
    bool breakCoroutine = false;

    Vector3 previousPos;

    void Start()
    {
        ResourceManager.instance.addToHarvestableItems(gameObject);

        maxResourceAmount = 10;
        resourceAmount = maxResourceAmount;
    }

    void Update()
    {
        if (character == null)
        {
            return;
        }

        if (character.getInventory() >= character.getMaxInventory())
        {
            //character.setHarvestObjectNull();
            wait(5);
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
        ResourceManager.instance.removeFromHarvestableItems(gameObject);
        transform.position = new Vector3(9999, -1000, 9999);

        yield return new WaitForSeconds(time);

        ResourceManager.instance.addToHarvestableItems(gameObject);
        transform.position = previousPos;
        resourceAmount = maxResourceAmount;
    }

    internal void BreakCoroutine()
    {
        breakCoroutine = true;
    }

    internal ResourceManager.Resource getResourceType()
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

    private void wait(float time)
    {
        StartCoroutine(waitIE(time));
    }

    private IEnumerator waitIE(float time)
    {
        yield return new WaitForSeconds(time);

        coroutineStarted = false;
        breakCoroutine = false;
    }

}
