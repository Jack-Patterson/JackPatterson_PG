using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class CharacterUI : MonoBehaviour
{
    public static CharacterUI instance;

    private CharacterControl character;
    string name;
    TMP_Dropdown jobDropdown;

    string currentValue;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of Character UI");
            return;
        }
        instance = this;

        Manager.instance.setCharUI(gameObject);
    }

    void Start()
    {
        createDropdown();
        currentValue = jobDropdown.options[jobDropdown.value].ToString();
    }


    void Update()
    {
        if (character != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                character = null;
                Manager.instance.setCharUIActive(false);
                return;
            }

            setName();
            currentlyDoing();
        }
    }

    private void createDropdown()
    {
        jobDropdown = GetComponentInChildren<TMP_Dropdown>();

        List<CharacterControl.Job> jobs = ((CharacterControl.Job[])Enum.GetValues(typeof(CharacterControl.Job))).ToList();
        List<TMP_Dropdown.OptionData> optionDatas = new List<TMP_Dropdown.OptionData>();

        foreach (CharacterControl.Job j in jobs)
        {
            TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData();
            data.text = j.ToString();
            optionDatas.Add(data);
        }

        jobDropdown.AddOptions(optionDatas);
    }

    private void setName()
    {
        TextMeshProUGUI nameGui = GameObject.Find("Name").GetComponent<TextMeshProUGUI>();
        nameGui.text = character.getName();
        name = character.getName();
    }

    internal void setCharacterFocus(CharacterControl character)
    {
        this.character = character;
    }

    public void checkValueChanged()
    {
        string temp = jobDropdown.options[jobDropdown.value].text;
        if (temp != currentValue)
        {
            currentValue = temp;
        }

        switch (currentValue)
        {
            case "Fighter":
                character.setJob(CharacterControl.Job.Fighter);
                break;
            case "Miner":
                character.setJob(CharacterControl.Job.Miner);
                break;
            case "Lumberjack":
                character.setJob(CharacterControl.Job.Lumberjack);
                break;
            case "Farmer":
                character.setJob(CharacterControl.Job.Farmer);
                break;
            case "None":
                character.setJob(CharacterControl.Job.None);
                break;
            default:
                character.setJob(CharacterControl.Job.None);
                break;
        }
    }

    internal void currentlyDoing()
    {
        TextMeshProUGUI doingText = GameObject.Find("DoingDesc").GetComponent<TextMeshProUGUI>();
        doingText.text = "Is currently " + character.currentStateEngl();
    }
}
