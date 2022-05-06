using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuUI : MonoBehaviour
{
    public static GameMenuUI instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of Character UI");
            return;
        }
        instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
    }

    public void resumeButton()
    {
        Manager.instance.setMenuUIActive(false);
    }

    public void exitMenuButton()
    {

    }

    public void exitDesktopButton()
    {
        Application.Quit(0);

        UnityEditor.EditorApplication.isPlaying = false;
    }
}
