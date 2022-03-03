using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterControl : MonoBehaviour
{
    // Setting Character State

    float characterInventory = 6;
    float characterInventoryMax = 10;

    ResourcesManager.Resources resourceType/* = ResourcesManager.Resources.Stone*/;
    HarvestableObject harvest;

    Inventory inventory;

    IInteractable interactableObject;

    public enum CharacterState {walkingTo, idle, mining, practice, reading, storing};
    CharacterState currentState = CharacterState.idle;
    CharacterState lastState = CharacterState.idle;

    // Getting Elements from this gameobject or others
    public Camera cam;
    NavMeshAgent agent;

    Animator animator;
    Rigidbody rigidBody;
    CapsuleCollider capsule;

    // temp
    public GameObject objToGet;
    public GameObject objToGet2;
    public GameObject objToGet3;
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

        agent.stoppingDistance = 12;

        findObject();
        setObjectStateOff();

        currentTarget = null;
        standPos = GetChildWithName(objToGet2, "StandPosition");
    }

    void Update()
    {
        CharacterStates();

        if (Input.GetKeyDown(KeyCode.F))
        {
            characterInventory = (int)UnityEngine.Random.Range(0, 10);
        }

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
                    setTarget(objToGet.transform.position);
                    currentTarget = objToGet;
                    currentState = CharacterState.walkingTo;
                }
                else if (Input.GetKeyDown(KeyCode.V))
                {
                    setTarget(standPos.transform.position);
                    currentTarget = standPos;
                    currentState = CharacterState.walkingTo;
                }
                else if (Input.GetKeyDown(KeyCode.C))
                {
                    setTarget(objToGet3.transform.position);
                    currentTarget = standPos;
                    currentState = CharacterState.walkingTo;
                }
                break;

            case CharacterState.walkingTo:
                CheckLastState();

                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    Collider[] cols = Physics.OverlapSphere(agent.destination, 5);

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
                break;

            case CharacterState.practice:
                CheckLastState();

                animator.SetBool("isMining", true);
                sword.SetActive(true);
                shield.SetActive(true);

                break;

            case CharacterState.storing:
                CheckLastState();
                
                // temp anim as is unused and will show it working
                animator.SetBool("isWalkingBackward", true);

                break;
        }
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
                //animator.SetBool("isStoring", false);
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

    public ResourcesManager.Resources getResourceType()
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

}
