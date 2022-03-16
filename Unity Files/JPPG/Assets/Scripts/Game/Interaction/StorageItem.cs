using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageItem : MonoBehaviour, IInteractable
{
    CharacterControl characterControl;
    
    float currentInventory = 0;
    float maxInventory = 20;
    ResourcesManager.Resources resources;


    void Start()
    {
        ResourcesManager.instance.addToStorageItems(this.gameObject);
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

    internal void giveItem(ResourcesManager.Resources resources, float amount)
    {
        this.resources = resources;
        currentInventory += amount;

        characterControl.removeFromInventory(amount);
        Debug.Log("Storage Item Amount: " + currentInventory);
    }


}
