using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    private float currentSpeed;
    private float turningSpeed;

    Animator charAnimations;

    void Start()
    {
        currentSpeed = 10;
        turningSpeed = 90;

        charAnimations = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveForward();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            charAnimations.SetBool("isWalkingForward", false);
            charAnimations.SetBool("isWalkingBackward", false);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveBackwards();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            charAnimations.SetBool("isWalkingForward", false);
            charAnimations.SetBool("isWalkingBackward", false);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            turnLeft();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            charAnimations.SetBool("isWalkingForward", false);
            charAnimations.SetBool("isWalkingBackward", false);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            turnRight();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            charAnimations.SetBool("isWalkingForward", false);
            charAnimations.SetBool("isWalkingBackward", false);
        }
    }

    void moveForward()
    {
        transform.position += currentSpeed * transform.forward * Time.deltaTime;
        charAnimations.SetBool("isWalkingForward", true);
        charAnimations.SetBool("isWalkingBackward", false);

    }

    void moveBackwards()
    {
        transform.position += currentSpeed * -transform.forward * Time.deltaTime;
        charAnimations.SetBool("isWalkingForward", false);
        charAnimations.SetBool("isWalkingBackward", true);
    }

    void turnLeft()
    {
        transform.Rotate(Vector3.up, -turningSpeed * Time.deltaTime);
        charAnimations.SetBool("isWalkingForward", true);
        charAnimations.SetBool("isWalkingBackward", false);
    }

    void turnRight()
    {
        transform.Rotate(Vector3.up, turningSpeed * Time.deltaTime);
        charAnimations.SetBool("isWalkingForward", true);
        charAnimations.SetBool("isWalkingBackward", false);
    }
}
