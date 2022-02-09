using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacterMove : MonoBehaviour
{

    // Getting Elements from this gameobject or others
    public Camera cam;
    NavMeshAgent agent;

    Animator animator;
    Rigidbody rigidBody;
    CapsuleCollider capsule;

    // temp
    public GameObject objToGet;
    public GameObject objToGet2;

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
        // Getting Objects and setting certain constraints
        h = objToGet.GetComponent<HarvestableObject>();

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        rigidBody = GetComponentInChildren<Rigidbody>();
        capsule = GetComponentInChildren<CapsuleCollider>();

        capsuleHeight = capsule.height;
        capsuleCenter = capsule.center;

        rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        pickaxe = GameObject.Find("Pickaxe");
        pickaxe.SetActive(false);
        isMining = false;

        sword = GameObject.Find("Sword");
        sword.SetActive(false);
        shield = GameObject.Find("Shield");
        shield.SetActive(false);

        standPos = GetChildWithName(objToGet2, "StandPosition");
    }

    void Update()
    {
        // if the mode is not setting to is currently building check the following
        if (!Manager.instance.getBuildMode())
        {
            // test raycast for moving
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    agent.SetDestination(hit.point);
                }
            }

            // Normalises movement and controls animations
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                Move(agent.desiredVelocity);
                animator.SetBool("isWalkingForward", true);
            }
            else
            {
                Move(Vector3.zero);
                animator.SetBool("isWalkingForward", false);
            }
        }

        // Temp allows char to move to the rock
        if (Input.GetKeyDown(KeyCode.B))
        {
            setTarget(objToGet.transform.position);
        }
        // temp allows char to move to training dummy
        else if (Input.GetKeyDown(KeyCode.V))
        {
            setTarget(standPos.transform.position);
        }

        // attempt to fix character not stopping move animation once reaching the dummy
        if (Vector3.Distance(transform.position, standPos.transform.position) <= 5)
        {
            
            if (!arrived)
            {
                Debug.Log("Arrived");
                arrived = true;
                
                
                
            }
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                setTarget(transform.position);
                
            }

        }

        // Allows char to mine
        if (Vector3.Distance(transform.position, objToGet.transform.position) > 5 && isMining == true)
        {
            toggleMining();
        }
    }

    // Normalises movement
    public void Move(Vector3 move)
    {
       if (move.magnitude > 1)
       {
            move.Normalize();
       }

        transform.InverseTransformDirection(move);

    }

    // Sets the agent target
    public void setTarget(Vector3 position)
    {
        agent.SetDestination(position);
    }

    // Checks for a collision enter and if the tag is correct begin mining object, somewhat incomplete yet
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MineTarget"))
        {
            setTarget(Vector3.zero);
            toggleMining();
            
            InvokeRepeating("testMineStone", 0, 4);
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
}
