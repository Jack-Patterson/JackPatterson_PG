using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacterMove : MonoBehaviour
{
    // Setting Character State
    int characterState;

    // Getting Elements from this gameobject or others
    public Camera cam;
    NavMeshAgent agent;

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

    // Also temp, for testing purposes
    HarvestableObject h;
    bool arrived = false;

    void Start()
    {
        characterState = 0;
        
        // Getting Objects and setting certain constraints
        h = objToGet.GetComponent<HarvestableObject>();

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        rigidBody = GetComponentInChildren<Rigidbody>();
        capsule = GetComponentInChildren<CapsuleCollider>();

        capsuleHeight = capsule.height;
        capsuleCenter = capsule.center;

        agent.stoppingDistance = 1;

        setObjectState();
        isMining = false;

        currentTarget = null;
        standPos = GetChildWithName(objToGet2, "StandPosition");
    }

    void Update()
    {
        CharacterStates();

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
        if (Input.GetKeyDown(KeyCode.B))
        {
            setTarget(objToGet.transform.position);
            currentTarget = objToGet;
        }
        // temp allows char to move to training dummy
        else if (Input.GetKeyDown(KeyCode.V))
        {
            setTarget(standPos.transform.position);
            currentTarget = standPos;
        }

        if (currentTarget != null)
        {
            // attempt to fix character not stopping move animation once reaching the dummy
            if (Vector3.Distance(transform.position, currentTarget.transform.position) <= 1)
            {
                if (agent.remainingDistance < agent.stoppingDistance)
                {
                    setTarget(transform.position);

                }
                if (!arrived)
                {
                    arrived = true;
                    transform.LookAt(objToGet2.transform);
                    characterState = 2;
                }
            }
        }
    }

    public void CharacterStates()
    {
        // 0 = Idle
        // 1 = Walking
        // 2 = Attacking (Melee) / Mining
        // 3 = Attacking (Ranged)

        switch (characterState)
        {
            case 0:
                break;
            case 1:
                if (animator.GetBool("isWalkingForward"))
                {
                    if (agent.remainingDistance > agent.stoppingDistance)
                    {
                        animator.SetBool("isWalkingForward", true);
                    }
                    else
                    {
                        animator.SetBool("isWalkingForward", false);
                        animator.StopPlayback();
                        characterState = 0;
                    }
                }
                break;
            case 2:
                /*if (animator.GetBool("isMining"))
                {
                    toggleMining();
                }*/
                sword.SetActive(true);
                shield.SetActive(true);
                animator.SetBool("isMining", true);
                break;
            case 3:
                break;
            default:
                Debug.LogError("Invalid character state. Setting it to idle.");
                characterState = 0;
                break;
        }
    }

    public int getCharacterState()
    {
        return characterState;
    }

    // Sets the agent target
    public void setTarget(Vector3 position)
    {
        animator.SetBool("isWalkingForward", true);
        agent.SetDestination(position);
        characterState = 1;
    }

    // Checks for a collision enter and if the tag is correct begin mining object, somewhat incomplete yet
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MineTarget"))
        {
            setTarget(Vector3.zero);
            characterState = 2;
            
            
            //InvokeRepeating("testMineStone", 0, 4);
        }
    }

    // Attempt to fix movement issues in regards to not stopping moving into or away from rock once they reach it
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("MineTarget"))
        {
            setTarget(transform.position);
            transform.LookAt(objToGet.transform);
        }
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
        h.mineStone();
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
