using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoamingEnemy : MonoBehaviour
{
    public int health;
    public float range;
    public Transform player;
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
        foundPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Manager.GetAlive() && !Manager.GetPaused() && !Manager.GetFinished())
        {
            CheckHealth();

            if (!foundPlayer)
                FindPlayer();
            else
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
        if (Vector3.Distance(transform.position, player.position) < range && Manager.GetAlive())
        {
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion shootAngle = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector3 rayDirection = shootAngle * Vector3.right;
            RaycastHit2D checkForPlayer = Physics2D.Raycast(transform.position, rayDirection, range, playerLayer);

            if (checkForPlayer.collider != null)
            {
                if (checkForPlayer.collider.tag == "Player")
                {
                    foundPlayer = true;
                    Debug.Log("Player found");
                }
            }
        }
    }

    void MoveToPlayer()
    {
        rb.velocity = Vector3.right * speed * Time.deltaTime;
        Vector3 target = player.position - transform.position;
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion shootAngle = Quaternion.AngleAxis(angle, Vector3.forward);
        rb.angularVelocity = angle * rotSpeed * Time.deltaTime;
    }

}
