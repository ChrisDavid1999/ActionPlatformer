using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public ParticleSystem shatter;
    public SpriteRenderer wall;
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        color.a = 0.25f;
    }

    public void DestroyBreakable()
    {
        if(wall != null)
        {
            wall.color = color;
        }    
        Destroy(gameObject);
    }
}
