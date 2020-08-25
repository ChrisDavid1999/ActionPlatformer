using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float time;
    public float distance;
    public LayerMask hittable;
    public int damage;
    public bool rocket = false;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, time);
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollision();

        transform.Translate(Vector2.right * speed * Time.deltaTime);//moves the object
    }

    void CheckCollision()
    {
        RaycastHit2D checkShot = Physics2D.Raycast(transform.position, Vector2.right, distance, hittable);

        if(checkShot.collider != null)
        {
            if(checkShot.collider.tag == "Enemy")
            {
                Enemy hitEnemy = checkShot.collider.GetComponent<Enemy>();
                hitEnemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
