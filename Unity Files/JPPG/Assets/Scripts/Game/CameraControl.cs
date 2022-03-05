using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl instance;
    
    public Transform followTrans;
    public Transform cameraTrans;
    
    public float normalSpeed;
    public float fastSpeed;
    public float movementSpeed;
    public float movementTime;
    public float rotAmount;
    public Vector3 zoomAmount;

    public Vector2 panLimit = new Vector2(40f, 35f);
    public float maxY = 30f;
    public float minY = 5f;

    public Vector3 newPos;
    public Quaternion newRot;
    public Vector3 newZoom;

    public Vector3 dragStartPos;
    public Vector3 dragCurrentPos;
    public Vector3 rotStartPos;
    public Vector3 rotCurrentPos;


    void Start()
    {
        instance = this;
        
        newPos = transform.position;
        newRot = transform.rotation;
        newZoom = cameraTrans.localPosition;
    }

    void Update()
    {
        if (followTrans != null)
        {
            transform.position = followTrans.position;
        }
        else
        {
            HandleMouseInput();
            HandleMovementInput();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            followTrans = null;
        }
    }

    void HandleMouseInput()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragStartPos = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPos = ray.GetPoint(entry);

                newPos = transform.position + dragStartPos - dragCurrentPos;
            }
        }

        if (Input.GetMouseButtonDown(2))
        {
            rotStartPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            rotCurrentPos = Input.mousePosition;

            Vector3 difference = rotStartPos - rotCurrentPos;

            rotStartPos = rotCurrentPos;

            newRot *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }
    }

    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPos += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPos += (transform.forward * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPos += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPos += (transform.right * -movementSpeed);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            newRot *= Quaternion.Euler(Vector3.up * rotAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRot *= Quaternion.Euler(Vector3.up * -rotAmount);
        }

        if (Input.GetKey(KeyCode.R))
        {
            newZoom += zoomAmount;
        }
        if (Input.GetKey(KeyCode.F))
        {
            newZoom -= zoomAmount;
        }

        newPos.x = Mathf.Clamp(newPos.x, -panLimit.x, panLimit.x);
        newPos.z = Mathf.Clamp(newPos.z, -panLimit.y, panLimit.y);
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * movementTime);

        

        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * movementTime);

        newZoom.x = Mathf.Clamp(newZoom.x, -panLimit.x, panLimit.x);
        newZoom.z = Mathf.Clamp(newZoom.z, -panLimit.y, panLimit.y);

        newZoom.y = Mathf.Clamp(newZoom.y, minY, maxY);
        cameraTrans.localPosition = Vector3.Lerp(cameraTrans.localPosition, newZoom, Time.deltaTime * movementTime);

        
    }
}
