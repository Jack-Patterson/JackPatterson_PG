using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    private Renderer rend;
    private Color startColour;
    private GameObject _object;
    private GameObject _tempObject;
    private bool mode;

    public Color hoverColor;
    public Vector3 offset;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
        mode = false;
    }

    private void OnMouseDown()
    {
        if (mode == false)
        {
            return;
        }
        
        if (_object != null)
        {
            Debug.LogError("Can't build there -  already built on");
            return;
        }

        offset = BuildManager.instance.getOffset();
        GameObject objectToBuild = BuildManager.instance.GetGroundObjectToBuild();
        if (gameObject.tag == "PlacedGroundTile" && objectToBuild.tag != "PlacedObject")
        {
            Debug.LogError("Object not placeable on this ground tile.");
        }
        else if (gameObject.tag == "BaseGround" && objectToBuild.tag == "BaseGround")
        {
            Debug.LogError("Object not placeable on same ground tile object 1.");
        }
        else if (gameObject.tag == "PlacedGroundTile" && objectToBuild.tag == "PlacedGroundTile")
        {
            Debug.LogError("Object not placeable on same ground tile object 2.");
        }
        else
        {
            _object = (GameObject)Instantiate(objectToBuild, transform.position + offset, transform.rotation);
        }
        
        

    }

    private void OnMouseEnter()
    {
        if (mode == false)
        {
            return;
        }
        rend.material.color = hoverColor;

        GameObject objectToBuild = BuildManager.instance.GetGroundObjectToBuild();
        _tempObject = (GameObject)Instantiate(objectToBuild, transform.position + offset, transform.rotation);
    }

    private void OnMouseExit()
    {
        if (mode == false)
        {
            return;
        }
        rend.material.color = startColour;

        if (_tempObject == null)
        {
            return;
        }
        Destroy(_tempObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && mode == false)
        {
            mode = true;
            return;
        }
        else if (Input.GetKeyDown(KeyCode.P) && mode == true)
        {
            mode = false;
            rend.material.color = startColour;
            return;
        }


    }
}
