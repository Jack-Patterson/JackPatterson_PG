using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacterMove : MonoBehaviour
{

    public Camera cam;
    NavMeshAgent agent;

    Animator animator;
    Rigidbody rigidBody;
    CapsuleCollider capsule;

    // temp
    public GameObject posToGet;

    float capsuleHeight;
    Vector3 capsuleCenter;

    // Mining Related
    bool isMining;
    GameObject pickaxe;

    // Weapons Related
    GameObject sword;
    GameObject shield;

    void Start()
    {
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
    }

    void Update()
    {
        if (!Manager.instance.getBuildMode())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    agent.SetDestination(hit.point);
                }
            }

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

        if (Input.GetKeyDown(KeyCode.B))
        {
            setTarget(posToGet.transform.position);
        }
    }

    public void Move(Vector3 move)
    {
       if (move.magnitude > 1)
       {
            move.Normalize();
       }

        transform.InverseTransformDirection(move);

    }

    public void setTarget(Vector3 position)
    {
        agent.SetDestination(position);
        //Move(agent.desiredVelocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MineTarget"))
        {
            setTarget(Vector3.zero);
            toggleMining();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("MineTarget"))
        {
            setTarget(transform.position);
            transform.LookAt(posToGet.transform);
        }
    }

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

}
