using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    bool buildModeNormal;
    bool buildModeWall;

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
        buildModeNormal = false;
        buildModeWall = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            buildModeWall = false;
            if (getBuildModeNormal())
            {
                setBuildModeNormal(false);
            }
            else if (!getBuildModeNormal())
            {
                setBuildModeNormal(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            buildModeNormal = false;
            if (getBuildModeWall())
            {
                setBuildModeWall(false);
            }
            else if (!getBuildModeWall())
            {
                setBuildModeWall(true);
            }
        }
    }

    public void setBuildModeNormal(bool buildMode)
    {
        this.buildModeNormal = buildMode;
    }

    public bool getBuildModeNormal()
    {
        return buildModeNormal;
    }

    public void setBuildModeWall(bool buildMode)
    {
        this.buildModeWall = buildMode;
    }

    public bool getBuildModeWall()
    {
        return buildModeWall;
    }

}
