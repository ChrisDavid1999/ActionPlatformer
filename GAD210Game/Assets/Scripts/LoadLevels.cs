using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevels : MonoBehaviour
{
    public GameObject escape;
    public GameObject skyhigh;
    public GameObject zoom;

    // Update is called once per frame
    void Update()
    {
        GameData d = Manager.GetInstance.GetGameData();

        if (d.Escape)
            escape.SetActive(true);
        else
            escape.SetActive(false);

        if (d.Skyhigh)
            skyhigh.SetActive(true);
        else
            skyhigh.SetActive(false);

        if (d.Zoom)
            zoom.SetActive(true);
        else
            zoom.SetActive(false);
    }
}
