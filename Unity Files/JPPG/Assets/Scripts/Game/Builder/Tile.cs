using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Renderer rend;
    private Color startColour;
    private GameObject _object;    
    private GameObject[] interiorTags;
    private GameObject[] exteriorTags;

    public Color hoverColor;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
        exteriorTags = GameObject.FindGameObjectsWithTag("Exterior Ground");
    }

    private void OnMouseDown()
    {
        if (_object != null)
        {
            Debug.LogError("Can't build there -  already built on");
            return;
        }

        GameObject objectToBuild = BuildManager.instance.GetGroundObjectToBuild();

        if (objectToBuild == _object)
        {
            Debug.LogError("Can't build there - build on itsself");
            return;
        }        

        for(int i = 0; i < exteriorTags.Length; i++)
        {
            if (exteriorTags[0] == null)
            {
                Debug.LogError("Can't build there - no tiles to build on");
                return;
            }

            if (_object == exteriorTags[i])
            {
                Debug.LogError("Can't build there - building on itself");
                return;
            }

        }

        _object = (GameObject)Instantiate(objectToBuild, transform.position, transform.rotation);

    }

    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColour;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}