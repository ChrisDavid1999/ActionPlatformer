using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string level;

    public bool Escape;
    public bool Skyhigh;
    public bool Zoom;
    // Start is called before the first frame update
    public GameData()
    {
        level = "Escape";
        Escape = true;
        Skyhigh = false;
        Zoom = false;
    }
}
