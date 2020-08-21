using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    private Rigidbody2D player;
    public float speed = 1f;
    public float jump = 10f;
    private int jumps = 2;
    public float direction = 1f;
    public LayerMask ground;
    public float groundCheckRadius;
    public Transform groundChecker;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveX();
        GroundCheck();
        Jump();
    }

    void MoveX()
    {
        float input = Input.GetAxis("Horizontal");
        player.velocity = new Vector2(input * speed, player.velocity.y);

        if (input < 0)
            direction = -1f;
        else 
            direction = 1f;
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && jumps != 0)
        {
            player.velocity = new Vector2(player.velocity.x, jump);
            jumps--;
        }
    }
    
    void GroundCheck()
    {
        Collider2D groundCol = Physics2D.OverlapCircle(groundChecker.position, groundCheckRadius, ground);

        if(groundCol != null)
        {
            jumps = 2;
        }
    }
}
