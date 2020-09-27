using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
    public LayerMask hittable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollision();
    }

    void CheckCollision()
    {
        RaycastHit2D check = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, hittable);
        if (check.collider != null)
        {
            if (check.collider.tag == "Breakable")
            {
                Breakable breakIt = check.collider.GetComponent<Breakable>();
                breakIt.DestroyBreakable();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Breakable")
        {
            GameObject breakObject = collision.gameObject;
            Breakable breakIt = breakObject.GetComponent<Breakable>();
            breakIt.DestroyBreakable();
        }
    }
}
