using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterControl : MonoBehaviour
{

    float characterInventory = 8;
    float characterInventoryMax = 10;

    ResourceManager.Resource resourceType/* = ResourcesManager.Resources.Stone*/;
    HarvestableObject harvest;

    StorageItem storage;
    Inventory inventory;

    IInteractable interactableObject;

    public enum CharacterState {walkingTo, idle, mining, practice, reading, storing};
    CharacterState currentState = CharacterState.idle;
    CharacterState lastState = CharacterState.idle;

    public enum Job { None, Swordsman, Archer, Miner, Lumberjack }
    [SerializeField]
    Job job = Job.None;

    // Getting Elements from this gameobject or others
    NavMeshAgent agent;

    Animator animator;
    Rigidbody rigidBody;
    CapsuleCollider capsule;

    /*// temp
    public GameObject objToGet;
    public GameObject objToGet2;
    public GameObject objToGet3;*/
    private GameObject currentTarget;

    GameObject standPos;

    // Obtained from Capsule Collider
    float capsuleHeight;
    Vector3 capsuleCenter;

    // Mining Related
    GameObject pickaxe;

    // Weapons Related
    GameObject sword;
    GameObject shield;

    void Start()
    {
        
        // Getting Objects and setting certain constraints
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        rigidBody = GetComponentInChildren<Rigidbody>();
        capsule = GetComponentInChildren<CapsuleCollider>();

        capsuleHeight = capsule.height;
        capsuleCenter = capsule.center;

        agent.stoppingDistance = 4;

        findObject();
        setObjectStateOff();

        currentTarget = null;
        //standPos = GetChildWithName(objToGet2, "StandPosition");
    }

    void Update()
    {
        CharacterStates();

        /*// Will only check these if the game is not in "Build Mode"
        // if the mode is not setting to is currently building check the following
        if (Manager.instance.getBuildMode())
        {
            return;
        }

        // Temp allows char to move to the rock

        if (currentTarget != null)
        {
            // attempt to fix character not stopping move animation once reaching the dummy
            
        }*/
    }

    public void CharacterStates()
    {
        switch (currentState)
        {
            case CharacterState.idle:
                CheckLastState();

                if (Input.GetKeyDown(KeyCode.B))
                {
                    GameObject target = findNearestHarvestableObject();
                    setTarget(target.transform.position);
                    currentState = CharacterState.walkingTo;
                }

                if (Input.GetKeyDown(KeyCode.C))
                {
                    GameObject target = findNearestStorageItemObject();
                    setTarget(target.transform.position);
                    currentState = CharacterState.walkingTo;
                }
                break;

            case CharacterState.walkingTo:
                CheckLastState();

                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    Collider[] cols = Physics.OverlapSphere(agent.destination, 3);

                    foreach (Collider c in cols)
                    {
                        interactableObject = c.GetComponent<IInteractable>();

                        if (interactableObject != null)
                        {
                            currentState = interactableObject.interact(this);

                            transform.LookAt(c.transform);
                        }
                    }
                }
                break;

            case CharacterState.mining:
                CheckLastState();

                animator.SetBool("isMining", true);
                pickaxe.SetActive(true);

                if (characterInventory == characterInventoryMax)
                {
                    currentState = CharacterState.idle;
                }
                break;

            case CharacterState.practice:
                CheckLastState();

                animator.SetBool("isMining", true);
                sword.SetActive(true);
                shield.SetActive(true);

                break;

            case CharacterState.storing:
                CheckLastState();

                storage.giveItem(resourceType, characterInventory);
                // temp anim as is unused and will show it working
                animator.SetBool("isWalkingBackward", true);
                if (characterInventory == 0)
                {
                    currentState = CharacterState.idle;
                }

                break;
        }
    }

    // Allows Camera to focus and follow this character
    public void OnMouseDown()
    {
        CameraControl.instance.focusTrans = transform;
    }

    // Sets the agent target
    public void setTarget(Vector3 position)
    {
        animator.SetBool("isWalkingForward", true);
        agent.SetDestination(position);
    }

    internal CharacterState getState()
    {
        return currentState;
    }

    internal void CheckLastState()
    {
        if (lastState == currentState)
        {
            return;
        }

        switch (lastState)
        {
            case CharacterState.idle:
                // Not currently interacting with anything
                if (interactableObject != null)
                {
                    interactableObject = null;
                }

                // Disables all tools/weapons
                setObjectStateOff();

                // Selecting a new task now that they're idle
                //SelectTask();

                break;

            case CharacterState.walkingTo:
                // Clears any previous interactable objects
                if (interactableObject != null)
                {
                    interactableObject = null;
                }

                // Disables all tools/weapons
                setObjectStateOff();

                // Disables any animations it shouldn't have
                animator.SetBool("isWalkingForward", false);
                break;

            case CharacterState.mining:
                animator.SetBool("isMining", false);
                break;

            case CharacterState.practice:
                //animator.SetBool("isAttackingMelee", false);
                animator.SetBool("isMining", false);
                break;

            case CharacterState.storing:
                animator.SetBool("isWalkingBackward", false);
                break;

            case CharacterState.reading:
                //animator.SetBool("isLearning", false);
                break;
        }

        lastState = currentState;
    }

    internal void IAmHarvestable(HarvestableObject harvestableObject)
    {
        harvest = harvestableObject;
        resourceType = harvest.getResourceType();
    }

    internal void IAmInventory(Inventory inventory)
    {
        this.inventory = inventory;
    }

    internal void IAmStorage(StorageItem storage)
    {
        this.storage = storage;
    }

    internal void give(float amount)
    {
        if (characterInventory >= characterInventoryMax)
        {
            harvest.BreakCoroutine();
            return;
        }

        characterInventory += amount;
        Debug.Log(characterInventory);
    }

    public ResourceManager.Resource getResourceType()
    {
        return resourceType;
    }

    public float getInventory()
    {
        return characterInventory;
    }

    public void removeFromInventory(float rate)
    {
        characterInventory -= rate;
        Debug.Log("Inventory amount: " + characterInventory);
    }

    // Gets the child of an object other than the one this script is attached to, in this case looking for the training dummy
    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }

    private void findObject()
    {
        pickaxe = GameObject.Find("Pickaxe");
        sword = GameObject.Find("Sword");
        shield = GameObject.Find("Shield");
    }

    private void setObjectStateOff()
    {
        pickaxe.SetActive(false);
        sword.SetActive(false);
        shield.SetActive(false);
    }

    private GameObject findNearestHarvestableObject()
    {
        GameObject nearest = null; 
        foreach(GameObject g in ResourceManager.instance.getHarvestableObjectsList())
        {
            if (nearest == null)
            {
                nearest = g;
            }
            if (Vector3.Distance(transform.position, g.transform.position) < Vector3.Distance(transform.position, nearest.transform.position))
            {
                nearest = g;
            }
        }
        return nearest;
    }

    private GameObject findNearestStorageItemObject()
    {
        GameObject nearest = null;
        foreach (GameObject g in ResourceManager.instance.getStorageItemsList())
        {
            if (nearest == null)
            {
                nearest = g;
            }
            if (Vector3.Distance(transform.position, g.transform.position) < Vector3.Distance(transform.position, nearest.transform.position))
            {
                nearest = g;
            }
        }

        return nearest;
    }

}
