using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float range;
    public float shotTimer;
    public Projectile projectile;
    public Transform player;
    private float storeShotTimer;
    private SpriteRenderer sprite;
    public Color[] damageIndicator;
    private int fullHealth;
    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        storeShotTimer = shotTimer;
        sprite = GetComponent<SpriteRenderer>();
        fullHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if(Manager.GetAlive() && !Manager.GetPaused())
        {
            CheckHealth();
            FindPlayer();
        }
    }

    //Checks the health of this object
    void CheckHealth()
    {
        float scaledHealth = (float)health / (float)fullHealth;
        if (scaledHealth == 1)
            sprite.color = damageIndicator[3];
        else if(scaledHealth <= 0.75 && scaledHealth > 0.5)
            sprite.color = damageIndicator[2];
        else if(scaledHealth <= 0.50 && scaledHealth > 0.25)
            sprite.color = damageIndicator[1];
        else if (scaledHealth <= 0.25)
            sprite.color = damageIndicator[0];

        if (health <= 0)
        {
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
        if (Vector3.Distance(transform.position, player.position) < range && Manager.GetAlive())
        {
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion shootAngle = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector3 rayDirection = shootAngle * Vector3.right;
            RaycastHit2D checkForPlayer = Physics2D.Raycast(transform.position, rayDirection, range, playerLayer);

            if(checkForPlayer.collider != null)
            {
                if(checkForPlayer.collider.tag == "Player")
                {
                    transform.rotation = shootAngle;
                    Shoot();
                } 
            }
        }
    }

    public void Shoot()
    {
        if (shotTimer <= 0)
        {
            Instantiate(projectile, transform.position, transform.rotation);
            shotTimer = storeShotTimer;
        }
        else
            shotTimer -= Time.deltaTime;
        
    }
}
