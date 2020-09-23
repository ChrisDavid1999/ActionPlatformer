using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Controller hit = collision.GetComponent<Controller>();
        
        if (hit != null)
        {
            if (hit.Heal(healAmount))
                Destroy(gameObject);         
        }
    }
}
