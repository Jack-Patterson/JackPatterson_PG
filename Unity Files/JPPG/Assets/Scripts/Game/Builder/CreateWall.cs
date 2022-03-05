using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWall : MonoBehaviour
{
    bool buildingFence;
    
    public GameObject wallPolePrefab;
    public GameObject wallPrefab;
    GameObject lastPos;

    public GameObject mousePointer;
    public Camera cam;

    private GameObject[] wallsList;

    void Start()
    {
        
    }

    void Update()
    {
        if (!Manager.instance.getBuildModeWall())
        {
            mousePointer.transform.position = new Vector3(0, -40, 0);
            return;
        }
        mousePointer.transform.position = snapPos(getWorldPoint());
        getInput();
    }

    void getInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            startWall();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            setWall();
        }
        else
        {
            if (buildingFence)
            {
                updateWall();
            }
        }
    }

    public Vector3 getWorldPoint()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    public Vector3 snapPos(Vector3 original)
    {
        Vector3 snapped;
        snapped.x = Mathf.Floor(original.x + 0.5f);
        snapped.y = Mathf.Floor(original.y + 0.5f);
        snapped.z = Mathf.Floor(original.z + 0.5f);

        /*float y = 0;

        if (rot.y > 55  rot.y < 65)
            y = 60;
        if (rot.y > 115  rot.y < 125)
            y = 120;
        if (rot.y > 175  rot.y < 185)
            y = 180;
        if (rot.y > 235  rot.y < 245)
            y = 240;
        if (rot.y > 295  rot.y < 305)
            y = 300;
        if (rot.y > 355  rot.y < 5)
            y = 0;*/



        return snapped;

    }

    void startWall()
    {
        buildingFence = true;
        
        Vector3 startPos = getWorldPoint();
        startPos = snapPos(startPos);
        
        GameObject startWallPos = Instantiate(wallPolePrefab, startPos, Quaternion.identity);
        startWallPos.transform.position = new Vector3(startPos.x, startPos.y + 1.1f, startPos.z);
        lastPos = startWallPos;
    }

    void setWall()
    {
        buildingFence = false;
    }

    void updateWall()
    {
        Vector3 current = getWorldPoint();
        current = snapPos(current);
        current = new Vector3(current.x, current.y + 1.1f, current.z);
        if (!current.Equals(lastPos.transform.position))
        {
            createWallSegment(current);
        }
    }

    void createWallSegment(Vector3 current)
    {
        GameObject newPos = Instantiate(wallPolePrefab, current, Quaternion.identity);
        Vector3 middle = Vector3.Lerp(newPos.transform.position, lastPos.transform.position, 0.5f);
        GameObject newWall = Instantiate(wallPrefab, middle, Quaternion.identity);
        newWall.transform.LookAt(lastPos.transform);
        lastPos = newPos;
    }
}
