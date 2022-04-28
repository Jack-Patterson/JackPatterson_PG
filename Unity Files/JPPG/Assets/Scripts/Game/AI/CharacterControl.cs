using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterControl : MonoBehaviour
{

    float characterInventory;
    float characterInventoryMax = 10;

    ResourceManager.Resource resourceType/* = ResourcesManager.Resources.Stone*/;
    HarvestableObject harvest;

    StorageItem storage;
    
    IInteractable interactableObject;

    public enum CharacterState {walkingToJob, idle, DoingJob, ReturningFromJob, ReturningToRestPosition, mining, practice, reading, storing, resting, returnToIdlePos};
    CharacterState currentState = CharacterState.idle;
    CharacterState lastState = CharacterState.idle;

    public enum Job { None, Fighter, Miner, Lumberjack, Farmer }
    [SerializeField]
    Job job = Job.None;

    // Getting Elements from this gameobject or others
    NavMeshAgent agent;

    Animator animator;
    Rigidbody rigidBody;
    CapsuleCollider capsule;

    GameObject standPos;

    // Obtained from Capsule Collider
    float capsuleHeight;
    Vector3 capsuleCenter;

    // Mining Related
    GameObject pickaxe;

    // Weapons Related
    GameObject sword;
    GameObject shield;

    bool resting;

    string name;
    bool isGenderMale;

    Vector3 target;

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

        characterInventory = 0;
        resting = false;

        isGenderMale = setGender();
        name = Manager.instance.getName(isGenderMale);
    }

    void Update()
    {
        CharacterStates();
    }

    public void CharacterStates()
    {
        CheckLastState();

        switch (currentState)
        {
            case CharacterState.idle:

                if (job != Job.None)
                {
                    GameObject target = findNearestHarvestableObject();
                    setTarget(target.transform.position);
                    currentState = CharacterState.walkingToJob;
                }
                break;

            case CharacterState.walkingToJob:
                if (agent.pathPending)
                {
                    return;
                }
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    Collider[] cols = Physics.OverlapSphere(agent.destination, 3);

                    foreach (Collider c in cols)
                    {
                        interactableObject = c.GetComponent<IInteractable>();

                        if (interactableObject != null)
                        {
                            interactableObject.interact(this);

                            transform.LookAt(c.transform);

                            currentState = CharacterState.DoingJob;

                            switch (resourceType)
                            {
                                case ResourceManager.Resource.Stone:
                                    animator.SetBool("isMining", true);
                                    pickaxe.SetActive(true);
                                    break;
                                case ResourceManager.Resource.Wood:
                                    animator.SetBool("isMining", true);
                                    pickaxe.SetActive(true);
                                    break;
                                case ResourceManager.Resource.Food:
                                    animator.SetBool("isMining", true);
                                    pickaxe.SetActive(true);
                                    break;
                                case ResourceManager.Resource.MeleeSkill:
                                    animator.SetBool("isMining", true);
                                    sword.SetActive(true);
                                    shield.SetActive(true);
                                    break;
                            }
                        }
                    }
                }
                break;

            case CharacterState.DoingJob:

                if (characterInventory >= characterInventoryMax)
                {
                    currentState = CharacterState.ReturningFromJob;
                    switch (resourceType)
                    {
                        case ResourceManager.Resource.Stone:
                            animator.SetBool("isMining", false);
                            pickaxe.SetActive(false);
                            setTarget(findNearestStorageItemObject().transform.position);
                            break;
                        case ResourceManager.Resource.Wood:
                            animator.SetBool("isMining", false);
                            pickaxe.SetActive(false);
                            setTarget(findNearestStorageItemObject().transform.position);
                            break;
                        case ResourceManager.Resource.Food:
                            animator.SetBool("isMining", false);
                            pickaxe.SetActive(false);
                            setTarget(findNearestStorageItemObject().transform.position);
                            break;
                        case ResourceManager.Resource.MeleeSkill:
                            animator.SetBool("isMining", false);
                            sword.SetActive(false);
                            shield.SetActive(false);
                            setTarget(findNearestStorageItemObject().transform.position);
                            break;
                    }

                }
                break;


            case CharacterState.ReturningFromJob:
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    Collider[] cols = Physics.OverlapSphere(agent.destination, 3);

                    foreach (Collider c in cols)
                    {
                        interactableObject = c.GetComponent<IInteractable>();

                        if (interactableObject != null)
                        {
                            interactableObject.interact(this);

                            transform.LookAt(c.transform);

                            currentState = CharacterState.storing;
                        }
                    }
                }

                break;

            case CharacterState.ReturningToRestPosition:
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    currentState = CharacterState.resting;
                }
                break;

            case CharacterState.storing:
                storage.giveItem(resourceType, characterInventory);
                // temp anim as is unused and will show it working
                animator.SetBool("isWalkingBackward", true);
                if (characterInventory == 0)
                {
                    currentState = CharacterState.ReturningToRestPosition;
                    setTarget(new Vector3(-31, 0, -12));
                }
                break;

            case CharacterState.resting:
                if (!resting)
                {
                    rest(5);
                    resting = true;
                }

                break;

            case CharacterState.returnToIdlePos:
                if (agent.pathPending)
                {
                    return;
                }
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    currentState = CharacterState.idle;
                }
                break;
        }
    }

    public void OnMouseDown()
    {
        CameraControl.instance.focusTrans = transform;
        Manager.instance.setCharUIActive(true);
        CharacterUI.instance.setCharacterFocus(this);
    }

    // Sets the agent target
    public void setTarget(Vector3 position)
    {
        animator.SetBool("isWalkingForward", true);
        agent.SetDestination(position);
        target = position;
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

            case CharacterState.walkingToJob:
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

            case CharacterState.ReturningToRestPosition:
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

            case CharacterState.resting:
                
                break;

            case CharacterState.ReturningFromJob:
                animator.SetBool("isWalkingForward", false);
                pickaxe.SetActive(false);
                break;
        }

        lastState = currentState;
    }

    internal void IAmHarvestable(HarvestableObject harvestableObject)
    {
        harvest = harvestableObject;
        resourceType = harvest.getResourceType();
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

    internal ResourceManager.Resource getResourceType()
    {
        return resourceType;
    }

    internal float getInventory()
    {
        return characterInventory;
    }

    internal float getMaxInventory()
    {
        return characterInventoryMax;
    }

    internal void setHarvestObjectNull()
    {
        if (harvest != null)
        {
            harvest = null;
        }
    }

    internal void setStorageObjectNull()
    {
        if (storage != null)
        {
            storage = null;
        }
    }

    internal void removeFromInventory(float rate)
    {
        characterInventory -= rate;
        Debug.Log("Inventory amount: " + characterInventory);
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
            HarvestableObject ho = g.GetComponent<HarvestableObject>();
            if (nearest == null)
            {
                nearest = g;
            }
            if (Vector3.Distance(transform.position, g.transform.position) < Vector3.Distance(transform.position, nearest.transform.position) && ResourceManager.instance.getJobResource(job) == ho.GetResource())
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

    private void rest(float time)
    {
        StartCoroutine(restIE(time));
    }

    private IEnumerator restIE(float time)
    {
        yield return new WaitForSeconds(time);

        Debug.Log("Rested");
        currentState = CharacterState.idle;
        resting = false;
    }

    private bool setGender()
    {
        float gender = (float)new System.Random().NextDouble();

        if (gender <= 0.5f)
        {
            return true;
        }
        return false;
    }

    internal string getName()
    {
        return name;
    }

    internal void setJob(Job job)
    {
        this.job = job;
        if (job == Job.None)
        {
            setTarget(new Vector3(-31, 0, -12));
            currentState = CharacterState.returnToIdlePos;
            animator.SetBool("isMining", false);
        }
    }

    internal string currentStateEngl()
    {
        float dist = Vector3.Distance(agent.destination, target);
        
        switch (currentState)
        {
            case CharacterState.idle:
                return "idling.";
            case CharacterState.walkingToJob:
                if (agent.pathPending)
                {
                    return "heading to job.";
                }
                return "heading to job. (" + dist.ToString("0.00") + "m)";
            case CharacterState.DoingJob:
                return "doing their job.";
            case CharacterState.ReturningFromJob:
                if (agent.pathPending)
                {
                    return "returning from job.";
                }
                return "returning from job. (" + dist.ToString("0.00") + "m)";
            case CharacterState.resting:
                return "resting.";
            case CharacterState.ReturningToRestPosition:
                if (agent.pathPending)
                {
                    return "heading to rest.";
                }
                return "heading to rest.(" + dist.ToString("0.00") + "m)";
            case CharacterState.storing:
                return "storing items.";
            default:
                return "idling.";
        }
    }

}
