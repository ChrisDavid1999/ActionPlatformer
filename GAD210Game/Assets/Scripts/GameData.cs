using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public int lives;
    public float health;
    public int level;
    // Start is called before the first frame update
    public GameData()
    {
        level = 1;
        health = 100.0f;
        lives = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
