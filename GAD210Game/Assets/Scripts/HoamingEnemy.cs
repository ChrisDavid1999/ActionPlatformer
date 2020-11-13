using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoamingEnemy : MonoBehaviour
{
    public int health;
    public float range;
    public GameObject target;
    private SpriteRenderer sprite;
    public Color[] damageIndicator;
    private int fullHealth;
    public LayerMask playerLayer;
    private Rigidbody2D rb;
    public float speed;
    public float rotSpeed;
    private bool foundPlayer;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        fullHealth = health;
        Manager.AddEnemy();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Manager.GetAlive() && !Manager.GetPaused() && !Manager.GetFinished())
        {
            CheckHealth();
            MoveToPlayer();
            
        }
    }

    //Checks the health of this object
    void CheckHealth()
    {
        float scaledHealth = (float)health / (float)fullHealth;
        if (scaledHealth <= 1 && scaledHealth > 0.50)
            sprite.color = damageIndicator[0];
        else if (scaledHealth <= 0.50)
            sprite.color = damageIndicator[1];

        if (health <= 0)
        {
            Manager.TakeEnemy();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= (int)dmg;
    }

    //Finds the player to attempt to shoot at them
    void FindPlayer()
    {
       
    }

    void MoveToPlayer()
    {
        Vector2 point = (Vector2)transform.position - (Vector2)target.transform.position;
        point.Normalize();
        float crossVal = Vector3.Cross(point, transform.right).z;

        if(crossVal > 0)
        {
            rb.angularVelocity = -rotSpeed;
        }
        else if(crossVal < 0)
        {
            rb.angularVelocity = rotSpeed;
        }

        rb.velocity = transform.right * speed;
    }

}
