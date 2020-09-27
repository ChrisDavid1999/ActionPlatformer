using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private static bool alive;
    private static Manager instance;
    private static bool paused;


    public float maxHealth;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance != this)
                Destroy(gameObject);
        }
    }

    public static Manager GetInstance
    {
        get
        {
            return instance;
        }
    }

    void Start()
    {
        SetAlive(true);
        paused = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
            Time.timeScale = 0;
        if(Input.GetButtonDown("Cancel") && alive)
        {
            if (paused == false)
                paused = true;
            else if(paused == true)
                paused = false;
        }
    }

    //Sets the status of alive
    public static void SetAlive(bool life)
    {
        alive = life;
    }

    //Gets the status of alive
    public static bool GetAlive()
    {
        return alive;
    }

    //Sets the status paused
    public static void SetPaused(bool p)
    {
        paused = p;
    }

    //Gets the status of paused
    public static bool GetPaused()
    {
        return paused;
    }

    //Reloads the level (This does not work once the package is exported, i am not sure why)
    public static void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SetAlive(true);
    }


    //Loads the player to the menu area (also does not work once exported and the menu is not completed)
    public static void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }    
}
