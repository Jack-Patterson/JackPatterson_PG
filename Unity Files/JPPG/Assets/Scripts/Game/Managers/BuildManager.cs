using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    
    private Vector3 potOffset;

    [Header("Test1")]
    public GameObject groundBuildPrefab;

    [Header("Test2")]
    public GameObject interiorBuildPrefab;

    [Header("Test3")]
    public GameObject otherBuildPrefab;

    [Header("testArray")]
    private GameObject[] ItemsToBuild;

    private GameObject objectToBuildGround;
    private bool changeSelectedObject;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of BuildManager");
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        objectToBuildGround = groundBuildPrefab;
        changeSelectedObject = true;
        potOffset.y = 0;
        potOffset.x = 0;
        potOffset.z = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && changeSelectedObject == true)
        {
            changeSelectedObject = false;
            potOffset.y = 0;
            objectToBuildGround = interiorBuildPrefab;
            return;
        }
        else if (Input.GetKeyDown(KeyCode.O) && changeSelectedObject == false)
        {
            changeSelectedObject = true;
            //potOffset.y = 10;
            objectToBuildGround = groundBuildPrefab;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            objectToBuildGround = groundBuildPrefab;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            objectToBuildGround = interiorBuildPrefab;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            objectToBuildGround = otherBuildPrefab;
        }
    }

    public GameObject GetGroundObjectToBuild()
    {
        return objectToBuildGround;
    }

    public Vector3 getOffset()
    {
        return potOffset;
    }
}
