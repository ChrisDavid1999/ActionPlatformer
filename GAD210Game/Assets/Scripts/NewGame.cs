using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public void LoadEscapeLevel()
    {
        Debug.Log("New game clicked");
        SceneManager.LoadScene("Escape");
    }
}
