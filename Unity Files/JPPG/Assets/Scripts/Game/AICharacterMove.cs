using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICharacterMove : MonoBehaviour
{

    public Camera cam;
    public NavMeshAgent agent;

    Animator animator;
    Rigidbody rigidBody;
    CapsuleCollider capsule;

    // temp
    public GameObject posToGet;

    float capsuleHeight;
    Vector3 capsuleCenter;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidBody = GetComponentInChildren<Rigidbody>();
        capsule = GetComponentInChildren<CapsuleCollider>();

        capsuleHeight = capsule.height;
        capsuleCenter = capsule.center;

        rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
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
            setTarget();
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

    public void setTarget()
    {
        agent.SetDestination(posToGet.transform.position);
    }
}
