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

    // Start is called before the first frame update
    void Start()
    {
        storeShotTimer = shotTimer;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        FindPlayer();
    }

    void CheckHealth()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
    }

    void FindPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < range)
        {
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Shoot();
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
