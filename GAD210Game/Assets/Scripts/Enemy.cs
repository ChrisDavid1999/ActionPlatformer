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

    // Start is called before the first frame update
    void Start()
    {
        storeShotTimer = shotTimer;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        FindPlayer();
    }

    void CheckHealth()
    {
        if (health == 100)
            sprite.color = damageIndicator[3];
        else if(health < 76 && health > 49)
            sprite.color = damageIndicator[2];
        else if(health < 50 && health > 25)
            sprite.color = damageIndicator[1];
        else if (health <= 25)
            sprite.color = damageIndicator[0];

        if (health <= 0)
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
