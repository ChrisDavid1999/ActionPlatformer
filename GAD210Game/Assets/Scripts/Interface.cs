using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface : MonoBehaviour
{

    public GameObject gameUi;
    public GameObject gameOver;
    public GameObject gameMenu;

    private bool gameUiShowing;
    private bool gameOverShowing;
    private bool gameMenuShowing;
    // Start is called before the first frame update
    void Start()
    {
        gameUiShowing = true;
        gameOverShowing = false;
        gameMenuShowing = Manager.GetPaused();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPaused();
        CheckPlayerAlive();
        ManageInterface();
    }

    //Manages what UI, game over page or menu is being shown
    void ManageInterface()
    {
        if (gameUiShowing)
        {
            gameUi.SetActive(true);
            gameMenu.SetActive(false);
            gameOver.SetActive(false);
        }
        else if (gameMenuShowing)
        {
            gameMenu.SetActive(true);
            gameOver.SetActive(false);
            gameUi.SetActive(false);
        }
        else if (gameOverShowing)
        {
            gameOver.SetActive(true);
            gameMenu.SetActive(false);
            gameUi.SetActive(false);
        }
    }


    //Checks the player is alive for the game over screen
    void CheckPlayerAlive()
    {
        if (!Manager.GetAlive())
        {
            gameUiShowing = false;
            gameOverShowing = true;
        }
    }

    void CheckPaused()
    {
        gameMenuShowing = Manager.GetPaused();
        if(gameMenuShowing)
        {
            gameUiShowing = false;
            gameOverShowing = false;
        }
    }

    //Reloads the level
    public void Reload()
    {
        Manager.ReloadLevel();
    }

    //Exits the level
    public void GoToStartMenu()
    {
        Manager.ExitToMenu();
    }
    
    public void Unpause()
    {
        Manager.SetPaused(false);
        gameMenuShowing = Manager.GetPaused();
        gameUiShowing = true;
    }
}
