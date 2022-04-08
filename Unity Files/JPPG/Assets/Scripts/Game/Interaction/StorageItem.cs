using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageItem : MonoBehaviour, IInteractable
{
    CharacterControl character;
    
    float currentInventory;
    float maxInventory = 20;
    ResourceManager.Resource resource;

    bool coroutineStarted = false;
    bool breakCoroutine;
    float collectRate = 1;


    void Start()
    {
        currentInventory = 0;

        ResourceManager.instance.addToStorageItems(gameObject);
    }

    void Update()
    {
        /*if (!coroutineStarted && currentInventory < maxInventory)
        {
            coroutineStarted = true;
            collectResource();
        }*/
    }

    public ResourceManager.Resource interact(CharacterControl characterControl)
    {
        this.character = characterControl;
        this.character.IAmStorage(this);
        return resource;
    }

    internal void giveItem(ResourceManager.Resource resource, float amount)
    {
        this.resource = resource;
        currentInventory += amount;

        character.removeFromInventory(amount);
        Debug.Log("Storage Item Amount: " + currentInventory);
    }

    internal void collectResource()
    {
        StartCoroutine(collectResourceIE(ResourceManager.instance.getResourceTime(resource)));
    }

    private IEnumerator collectResourceIE(float time)
    {
        yield return new WaitForSeconds(time);

        character.give(collectRate);
        currentInventory += collectRate;

        if (currentInventory <= 0 || breakCoroutine)
        {
            yield break;
        }

        coroutineStarted = false;
    }

    internal void BreakCoroutine()
    {
        breakCoroutine = true;
    }

}
