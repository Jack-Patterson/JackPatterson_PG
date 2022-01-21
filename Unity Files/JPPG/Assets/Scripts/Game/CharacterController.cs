using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    private float currentSpeed;

    void Start()
    {
        currentSpeed = 5;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveForward();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveBackwards();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveRight();
        }
    }

    void moveForward()
    {
        transform.position += currentSpeed * transform.forward * Time.deltaTime;
    }

    void moveBackwards()
    {
        transform.position += currentSpeed * -transform.forward * Time.deltaTime;
    }

    void moveLeft()
    {
        transform.position += currentSpeed * -transform.right * Time.deltaTime;
    }

    void moveRight()
    {
        transform.position += currentSpeed * transform.right * Time.deltaTime;
    }
}
