using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform playerPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If the player is alive, the camera will follow them
        if(Manager.GetAlive())
            this.transform.position = new Vector3(playerPos.position.x, playerPos.position.y, this.transform.position.z);
    }
}
