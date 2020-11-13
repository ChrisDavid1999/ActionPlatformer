using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    public void LoadLevel(string name)
    {
        Manager.GetInstance.LoadLevel(name);
    }

    public void LoadNew()
    {
        Manager.GetInstance.LoadNew();
    }
}
