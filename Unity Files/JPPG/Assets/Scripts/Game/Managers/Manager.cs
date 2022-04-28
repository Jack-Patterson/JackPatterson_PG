using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.IO;
using System.Linq;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    bool buildModeNormal;
    bool buildModeWall;

    private GameObject charUI;

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
        setCharUIActive(false);
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

    internal string getName(bool isGenderMale)
    {
        string readFromFilePath;
        if (isGenderMale)
        {
            readFromFilePath = Application.streamingAssetsPath + "/Names/" + "/GenericNamesMale.txt";
        }
        else
        {
            readFromFilePath = Application.streamingAssetsPath + "/Names/" + "GenericNamesFemale.txt";
        }

        List<string> fileLines = File.ReadAllLines(readFromFilePath).ToList();

        int pos = Random.Range(0, fileLines.Count);

        return fileLines[pos];
    }

    internal Sprite retrieveSprite(string job)
    {
        switch (job)
        {
            case "Fighter":
                Resources.Load<Sprite>("Sprites/icon_sword/");
                break;
            case "Miner":
                Resources.Load<Sprite>("Sprites/icon_pickaxe/");
                break;
            case "Lumberjack":
                Resources.Load<Sprite>("Sprites/icon_axe/");
                break;
            case "Farmer":
                Resources.Load<Sprite>("Sprites/icon_pitchfork/");
                break;
            case "None":
                return null;
            default:
                return null;
        }
        return null;
    }

    internal void setCharUI(GameObject charUI)
    {
        this.charUI = charUI;
    }

    internal void setCharUIActive(bool active)
    {
        charUI.SetActive(active);
    }

}
