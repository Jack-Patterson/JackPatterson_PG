using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    bool buildMode;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of Manager");
            return;
        }
        instance = this;
    }

    void Start()
    {
        buildMode = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (getBuildMode())
            {
                setBuildMode(false);
            }
            else if (!getBuildMode())
            {
                setBuildMode(true);
            }
        }
    }

    public void setBuildMode(bool buildMode)
    {
        this.buildMode = buildMode;
    }

    public bool getBuildMode()
    {
        return buildMode;
    }

}
