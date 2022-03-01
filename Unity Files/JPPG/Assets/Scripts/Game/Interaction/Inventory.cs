using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IInteractable
{
    CharacterControl character;
    ResourcesManager.Resources resource;
    float storageRate = 1;

    bool breakCoroutine = false;
    bool coroutineStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (character == null)
        {
            return;
        }

        resource = character.getResourceType();

        if (character.getState() == CharacterControl.CharacterState.storing)
        {
            if (!coroutineStarted)
            {
                coroutineStarted = true;
                storeResource();
            }

            //addResources(resource);
            
        }
        
    }

    public CharacterControl.CharacterState interact(CharacterControl NPC)
    {
        character = NPC;
        character.IAmInventory(this);
        return CharacterControl.CharacterState.storing;
    }

    internal void storeResource()
    {
        StartCoroutine(depositResourceIE(ResourcesManager.instance.getTime(resource), resource));
    }

    private IEnumerator depositResourceIE(float time, ResourcesManager.Resources resource)
    {
        float inventoryAmount = character.getInventory();
        Item item = ResourcesManager.instance.getResourceItem(resource);

        if (inventoryAmount <= 0 || breakCoroutine)
        {
            yield break;
        }

        yield return new WaitForSeconds(time);

        item.addAmount((int)storageRate);
        character.removeFromInventory(storageRate);
        Debug.Log("Stone amount: " + item.getAmount());

        

        coroutineStarted = false;
    }

    public void BreakCoroutine()
    {
        breakCoroutine = true;
    }
}
