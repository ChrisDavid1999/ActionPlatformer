using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject instructions;
    public GameObject loadLevels;

    private bool menuUi;
    private bool instructionsUi;
    private bool levelsUi;
    // Start is called before the first frame update
    void Start()
    {
        menuUi = true;
        instructionsUi = false;
        levelsUi = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(menuUi)
        {
            menu.SetActive(true);
            instructions.SetActive(false);
            loadLevels.SetActive(false);
        }
        else if(instructionsUi)
        {
            menu.SetActive(false);
            instructions.SetActive(true);
            loadLevels.SetActive(false);
        }
        else if(levelsUi)
        {
            menu.SetActive(false);
            instructions.SetActive(false);
            loadLevels.SetActive(true);
        }
    }

    public void Back()
    {
        menuUi = true;
        instructionsUi = false;
        levelsUi = false;
    }

    public void LoadLevelsUi()
    {
        Manager.GetInstance.LoadGame();
        menuUi = false;
        instructionsUi = false;
        levelsUi = true;
    }

    public void LoadInstructions()
    {
        menuUi = false;
        instructionsUi = true;
        levelsUi = false;
    }
}
