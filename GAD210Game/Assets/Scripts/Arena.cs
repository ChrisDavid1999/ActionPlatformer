using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject barrier;

    private int count;
    // Start is called before the first frame update
    void Start()
    {
        count = enemies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        int counter = 0;

        for(int i = 0; i < count; i++)
        {
            if(enemies[i] == null)
            {
                counter++;
            }
        }

        if(counter == count)
        {
            Destroy(gameObject);
        }    
    }
}
