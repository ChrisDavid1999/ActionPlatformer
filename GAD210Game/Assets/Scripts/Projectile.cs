using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float time;
    public float distance;
    public LayerMask hittable;
    public float damage;
    public bool rocket = false;
    public bool shotgun = false;
    public bool enemy = false;
    private RaycastHit2D checkShot;
    private float shotTime;

    // Start is called before the first frame update
    void Start()
    {
        shotTime = time;
        Destroy(gameObject, time);
    }

    // Update is called once per frame
    void Update()
    {
        shotTime -= Time.deltaTime;
        if (shotTime - Time.deltaTime < 0)
            shotTime = 0;

        CheckCollision();

        transform.Translate(Vector2.right * speed * Time.deltaTime);//moves the object
    }

    void CheckCollision()
    {
        checkShot = Physics2D.Raycast(transform.position, Vector2.right, distance, hittable);
        if(checkShot.collider != null)
        {
            if(!enemy)
            {
                if (checkShot.collider.tag == "Enemy")
                {
                    Enemy hitEnemy = checkShot.collider.GetComponent<Enemy>();

                    if(shotgun)
                        hitEnemy.TakeDamage((float)damage * shotTime);
                    else
                        hitEnemy.TakeDamage(damage);

                    Destroy(gameObject);
                }
                else if (checkShot.collider.tag == "Breakable")
                {
                    Breakable breakIt = checkShot.collider.GetComponent<Breakable>();
                    breakIt.DestroyBreakable();
                }
                else
                    Destroy(gameObject);
            }
            else
            {
                if (checkShot.collider.tag == "Player")
                {
                    Controller player = checkShot.collider.GetComponent<Controller>();
                    player.TakeDamage((int)damage);
                }
                Destroy(gameObject);
            }
            
        }


    }

    public RaycastHit2D GetCheckShot()
    {
        return checkShot;
    }
}
