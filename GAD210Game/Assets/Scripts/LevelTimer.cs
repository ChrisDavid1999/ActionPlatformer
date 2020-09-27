using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    public Text timeText;
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
        if(!Manager.GetPaused())
        {
            time += Time.deltaTime / Time.timeScale;
            timeText.text = time +  " ";
        }
    }
}
