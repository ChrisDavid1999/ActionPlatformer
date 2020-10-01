using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    public Text timeText;
    public Text timeText2;
    public Text kills;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Updates the timer for the level (will be moved to game manager at somepoint, this is just what i did first)
        if(!Manager.GetPaused() && !Manager.GetFinished())
        {
            time += Time.deltaTime / Time.timeScale;
            timeText.text = time +  " ";
            timeText2.text = "Time: " + time;
            kills.text = "Kills: " + (Manager.GetTotalEnemies() - Manager.GetEnemies()) + "/" + Manager.GetTotalEnemies();
        }
    }
}
