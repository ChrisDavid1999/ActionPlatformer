using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Manager : MonoBehaviour
{
    private static bool alive;
    private static Manager instance;
    private static bool paused;
    private static bool finished;
    private GameData gameData;
    private string saveFileName = "data.json";

    public float maxHealth;
    private static int enemyCount = 0;
    private static int enemyCountTotal = 0;

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

    public GameData GetGameData()
    {
        return gameData;
    }

    void Start()
    {
        gameData = new GameData();
        SetAlive(true);
        paused = false;
        finished = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (paused)
            Time.timeScale = 0;

        if(Input.GetButtonDown("Cancel") && alive && !finished)
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

    //Sets the status of alive
    public static void SetFinished(bool f)
    {
        finished = f;
    }

    //Gets the status of alive
    public static bool GetFinished()
    {
        return finished;
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

    public static void SetBothEnemies(int i)
    {
        enemyCount = i;
        enemyCountTotal = i;
    }

    public static int GetTotalEnemies()
    {
        return enemyCountTotal;
    }

    public static int GetEnemies()
    {
        return enemyCount;
    }

    public static void AddEnemy()
    {
        enemyCount++;
        enemyCountTotal++;
    }

    public static void TakeEnemy()
    {
        enemyCount--;
    }

    //Reloads the level (This does not work once the package is exported, i am not sure why)
    public static void ReloadLevel()
    {
        alive = true;
        paused = false;
        finished = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SetBothEnemies(0);
    }


    //Loads the player to the menu area (also does not work once exported and the menu is not completed)
    public static void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }    

    public void LoadLevel(string levelName)
    {
        alive = true;
        paused = false;
        finished = false;
        enemyCount = 0;
        enemyCountTotal = 0;

        if (levelName == "Escape")
            gameData.Escape = true;
        else if (levelName == "Skyhigh")
            gameData.Skyhigh = true;
        else if (levelName == "Zoom")
            gameData.Zoom = true;


        SceneManager.LoadScene(levelName);
        SaveGame(); 
    }

    public void LoadNew()
    {
        gameData = new GameData();
        alive = true;
        paused = false;
        finished = false;
        enemyCount = 0;
        enemyCountTotal = 0;
        SceneManager.LoadScene("Escape");
        SaveGame();
    }

    void SetGameData(GameData d)
    {
        gameData = d;
    }

    public void LoadGame()
    {
        string filePath = Application.streamingAssetsPath + "/" + saveFileName;

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);
            SetGameData(loadedData);
        }
    }

    public void SaveGame()
    {
        GameData gd = gameData;
        string jsonData = JsonUtility.ToJson(gd);
        //Debug.Log(jsonData);
        string filePath = Application.streamingAssetsPath + "/" + saveFileName;
        File.WriteAllText(filePath, jsonData);
    }
}
