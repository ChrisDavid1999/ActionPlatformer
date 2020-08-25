using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{

    private Rigidbody2D player;
    public float speed = 1f;
    public float jumpAcc = 10f;
    public float health = 100;
    public int jumps = 2;
    public float direction = 1f;
    public LayerMask ground;
    public float groundCheckRadius;
    public Transform groundChecker;
    public Image hpBar;
    public Image slowTimerBack;

    [Range(0.01f,1.0f)]
    public float timeScale;
    public float slowCoolDown;

    private float fixedTime;
    private float slowTimer;
    private bool slowOnCooldown = false;
    private Color iconColor;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        slowTimer = slowCoolDown;
        iconColor = slowTimerBack.color;
    }

    // Update is called once per frame
    void Update()
    {
        MoveX();
        GroundCheck();
        Jump();
        UpdateHealthBar();
        SlowDown();
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
            player.velocity = new Vector2(player.velocity.x, jumpAcc);
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

    void UpdateHealthBar()
    {
        Debug.Log(hpBar.fillAmount);
        hpBar.fillAmount = health / 100;
    }

    void SlowDown()
    {
        if (Input.GetButton("Fire2") && slowTimer > 0 && !slowOnCooldown)
        {
            Time.timeScale = timeScale;
            slowTimer -= Time.deltaTime / timeScale;

            if (slowTimer < 0)
            {
                slowTimer = 0;
                slowOnCooldown = true;
                slowTimerBack.color = Color.blue;
            }
        }
        else
        {
            Time.timeScale = 1.0f;

            if (slowTimer < slowCoolDown)
                slowTimer += Time.deltaTime;
            else if (slowTimer >= slowCoolDown)
            {
                slowOnCooldown = false;
                slowTimerBack.color = iconColor;
            }
                
        }

        slowTimerBack.fillAmount = slowTimer / slowCoolDown;
        Time.fixedDeltaTime = fixedTime * Time.timeScale;
    }
}
