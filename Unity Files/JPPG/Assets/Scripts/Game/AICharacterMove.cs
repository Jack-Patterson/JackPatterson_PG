using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacterMove : MonoBehaviour
{
    // Setting Character State

    float bucket_contents, max_bucket_contents = 10;

    HarvestableObject.Resources am_currently_collecting;
    HarvestableObject currentlyHarvesting;
    public enum CharacterState { walking_to, idle, mining, practice, reading};
    CharacterState isCurrently = CharacterState.idle;

    // Getting Elements from this gameobject or others
    public Camera cam;
    NavMeshAgent agent;

    internal void give(float amount)
    {
        bucket_contents += amount;
 
        if (bucket_contents > max_bucket_contents)
        {

        }


    }

    Animator animator;
    Rigidbody rigidBody;
    CapsuleCollider capsule;

    // temp
    public GameObject objToGet;
    public GameObject objToGet2;
    private GameObject currentTarget;

    GameObject standPos;

    // Obtained from Capsule Collider
    float capsuleHeight;
    Vector3 capsuleCenter;

    // Mining Related
    bool isMining;
    GameObject pickaxe;

    // Weapons Related
    GameObject sword;
    GameObject shield;

    internal void Iam(HarvestableObject harvestableObject)
    {
        currentlyHarvesting = harvestableObject;
        am_currently_collecting = currentlyHarvesting.this_has;
    }

    // Also temp, for testing purposes
    HarvestableObject h;
    bool arrived = false;

    void Start()
    {
        
        // Getting Objects and setting certain constraints
        h = objToGet.GetComponent<HarvestableObject>();

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        rigidBody = GetComponentInChildren<Rigidbody>();
        capsule = GetComponentInChildren<CapsuleCollider>();

        capsuleHeight = capsule.height;
        capsuleCenter = capsule.center;

        agent.stoppingDistance = 12;

        setObjectState();
        isMining = false;

        currentTarget = null;
        standPos = GetChildWithName(objToGet2, "StandPosition");
    }

    void Update()
    {
        switch (isCurrently)
        {
            case CharacterState.idle:
                if (Input.GetKeyDown(KeyCode.B))
                {
                    setTarget(objToGet.transform.position);
                    currentTarget = objToGet;
                    isCurrently = CharacterState.walking_to;
                }
                else if (Input.GetKeyDown(KeyCode.V))
                {
                    setTarget(standPos.transform.position);
                    currentTarget = standPos;
                    isCurrently = CharacterState.walking_to;
                }


                break;
            case CharacterState.walking_to:
                if (agent.remainingDistance < agent.stoppingDistance)
                    {
                    Collider[] cols = Physics.OverlapSphere(agent.destination, 3);

                    foreach (Collider c in cols)
                    {
                       IInteractable interactable_object =  c.GetComponent<IInteractable>();

                      if (interactable_object != null)
                        {
                           isCurrently = interactable_object.interact(this);

                            
                           transform.LookAt(c.transform);
                        }
                    }

                      

                    }



                break;


            case CharacterState.mining:

                animator.SetBool("isMining", true);
                pickaxe.SetActive(true);
             

                break;



            




        }
        
        
        
        
        
        
        
        
        /*if (characterState != 2)
        {
            shield.SetActive(false);
            sword.SetActive(false);
            animator.SetBool("isMining", false);
        }*/

        // Will only check these if the game is not in "Build Mode"
        // if the mode is not setting to is currently building check the following
        if (Manager.instance.getBuildMode())
        {
            return;
        }
            
        // test raycast for moving
        /*if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }*/

        // Temp allows char to move to the rock
        

        if (currentTarget != null)
        {
            // attempt to fix character not stopping move animation once reaching the dummy
            
        }
    }

    // Sets the agent target
    public void setTarget(Vector3 position)
    {
        animator.SetBool("isWalkingForward", true);
        agent.SetDestination(position);
    }



    // Plays mining animation and gets char to hold pickaxe
    private void toggleMining()
    {
        if (!isMining)
        {
            isMining = true;
            animator.SetBool("isMining", true);
            pickaxe.SetActive(true);
        }
        else if (isMining)
        {
            isMining = false;
            animator.SetBool("isMining", true);
            pickaxe.SetActive(false);
        }
    }

    // Used for the repeating as does not work if calling a method from another script directly
    void testMineStone()
    {
     
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

    private void setObjectState()
    {
        
        pickaxe = GameObject.Find("Pickaxe");
        pickaxe.SetActive(false);

        sword = GameObject.Find("Sword");
        sword.SetActive(false);

        shield = GameObject.Find("Shield");
        shield.SetActive(false);
    }

}
