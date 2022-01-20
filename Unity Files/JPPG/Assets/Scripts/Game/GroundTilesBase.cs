using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTilesBase : MonoBehaviour
{
    private Renderer rend;
    private Color startColour;
    private GameObject _object;
    private bool mode;

    public Color hoverColor;
    public Vector3 offset;

    // Start is called before the first frame update
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
        Debug.Log(offset);
        GameObject objectToBuild = BuildManager.instance.GetGroundObjectToBuild();
        _object = (GameObject)Instantiate(objectToBuild, transform.position + offset, transform.rotation);

    }

    private void OnMouseEnter()
    {
        if (mode == false)
        {
            return;
        }
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        if (mode == false)
        {
            return;
        }
        rend.material.color = startColour;
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
