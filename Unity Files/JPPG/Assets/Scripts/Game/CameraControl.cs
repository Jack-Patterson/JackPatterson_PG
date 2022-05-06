using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl instance;
    
    internal Transform focusTrans;
    private Transform cameraTrans;
    
    private float normalSpeed = .5f;
    private float fastSpeed = 3f;
    private float movementSpeed = 1f;
    private float movementTime = 5f;
    private float rotAmount = 1f;
    private Vector3 zoomAmount = new Vector3(0,-5,5);

    private Vector2 panLimit = new Vector2(40f, 35f);
    private float maxY = 30f;
    private float minY = 5f;

    private Vector3 newPos;
    private Quaternion newRot;
    private Vector3 newZoom;

    private Vector3 dragStartPos;
    private Vector3 dragCurrentPos;
    private Vector3 rotStartPos;
    private Vector3 rotCurrentPos;


    void Start()
    {
        instance = this;

        cameraTrans = GameObject.Find("Main Camera").transform;

        newPos = transform.position;
        newRot = transform.rotation;
        newZoom = cameraTrans.localPosition;
    }

    void Update()
    {
        if (focusTrans != null)
        {
            transform.position = focusTrans.position;
            HandleFocusTransInput();
        }
        else
        {
            HandleMouseInput();
            HandleMovementInput();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            focusTrans = null;
        }
    }

    void HandleFocusTransInput()
    {
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

        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * movementTime);
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

    internal bool hasFocus()
    {
        if (focusTrans != null)
        {
            return true;
        }
        return false;
    }
}
