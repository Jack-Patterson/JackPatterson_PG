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
    [SerializeField]
    private GameObject gameMenuUI;

    private List<CharacterControl> charList;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of Manager");
            return;
        }
        instance = this;
        
        charList = new List<CharacterControl>();
    }

    void Start()
    {
        // Ensures build mode is off on start
        buildModeNormal = false;
        buildModeWall = false;
        // Ensures menu information UI is off
        setCharUIActive(false);
        setMenuUIActive(false);
    }

    void Update()
    {
        // Toggles Build Mode - WIP
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

        // Toggles Build Wall mode - WIP
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

        if (Input.GetKeyDown(KeyCode.Escape) && !CameraControl.instance.hasFocus())
        {
            if (gameMenuUI.activeSelf)
            {
                setMenuUIActive(false);
            }
            else
            {
                setMenuUIActive(true);
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

    // Reads a list of names from a file and returns a random one based on gender
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

    // retrieves and loads a sprite for the UI
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

    // Sets and Toggles the character UI
    internal void setCharUI(GameObject charUI)
    {
        this.charUI = charUI;
    }

    internal void setCharUIActive(bool active)
    {
        charUI.SetActive(active);
    }

    // Sets and Toggles the Menu UI
    internal void setMenuUI(GameObject gameMenuUI)
    {
        this.gameMenuUI = gameMenuUI;
        //this.gameMenuUI = GameMenuUI.instance.gameObject;
    }

    internal void setMenuUIActive(bool active)
    {
        gameMenuUI.SetActive(active);
        if (active)
        {
            setTime(0);
        }
        else
        {
            setTime(1);
        }
    }

    internal void addCharList(CharacterControl character)
    {
        charList.Add(character);
    }

    internal void setTime(int level)
    {
        switch (level)
        {
            case 0:
                Time.timeScale = 0;
                break;
            case 1:
                Time.timeScale = 1;
                break;
            case 2:
                Time.timeScale = 2;
                break;
            default:
                Time.timeScale = 1;
                break;
        }
    }

}
