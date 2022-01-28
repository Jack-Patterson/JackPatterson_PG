using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterMove : MonoBehaviour
{
    // Script is based on a script from the Youtuber "Brackeys" https://www.youtube.com/watch?v=FkLJ45Pt-mY

    Animator animator;
    Rigidbody rigidBody;
    CapsuleCollider capsule;

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
        
    }

    public void Move(Vector3 move)
    {
       if (move.magnitude > 1)
       {
            move.Normalize();
       }

        transform.InverseTransformDirection(move);

        animator.SetBool("isWalkingForward", true);
    }
}
