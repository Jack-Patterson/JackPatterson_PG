using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageItem : MonoBehaviour, IInteractable
{
    CharacterControl characterControl;
    
    float currentInventory = 0;
    float maxInventory = 20;
    ResourceManager.Resource resources;


    void Start()
    {
        ResourceManager.instance.addToStorageItems(gameObject);
    }

    void Update()
    {
        
    }

    public CharacterControl.CharacterState interact(CharacterControl characterControl)
    {
        this.characterControl = characterControl;
        this.characterControl.IAmStorage(this);
        return CharacterControl.CharacterState.storing;
    }

    internal void giveItem(ResourceManager.Resource resources, float amount)
    {
        this.resources = resources;
        currentInventory += amount;

        characterControl.removeFromInventory(amount);
        Debug.Log("Storage Item Amount: " + currentInventory);
    }


}
