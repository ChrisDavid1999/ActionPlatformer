using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healAmount;

    //When there is a collision with the object that hosts the players controller, attempt to heal then
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
