using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{

    private Rigidbody2D player;
    public float speed = 1f;
    public float jumpAcc = 10f;
    public int jumps = 2;
    public float direction = 1f;
    public LayerMask ground;
    public float groundCheckRadius;
    public Transform[] groundChecker;
    public Image hpBar;
    public Image slowTimerBack;
    public Image gravityBack;
    public Image gravityIcon;

    [Range(0.01f,1.0f)]
    public float timeScale;
    public float slowCoolDown;

    private float health;
    private int jumpCount;
    private float fixedTime;
    private float slowTimer;
    private bool slowOnCooldown = false;
    private Color iconColor;
    private Transform currentSpawnPoint;
    private bool gravChange;
    private int currentGround;
    private Aim aimer;
    private float initGravscale;
    private float initJumpAcc;

    // Start is called before the first frame update
    void Start()
    {
        health = Manager.GetInstance.maxHealth;
        player = GetComponent<Rigidbody2D>();
        slowTimer = slowCoolDown;
        iconColor = slowTimerBack.color;
        fixedTime = Time.fixedDeltaTime;
        jumpCount = jumps;
        gravChange = false;
        currentGround = 0;
        aimer = GetComponent<Aim>();
        initGravscale = player.gravityScale;
        initJumpAcc = jumpAcc;
    }

    // Update is called once per frame
    void Update()
    {
        if(Manager.GetAlive() && !Manager.GetPaused())
        {
            MoveX();
            GroundCheck();
            Jump();
            ChangeGravity();
            UpdateHealthBar();
            SlowDown();
            Respawn();
        }
    }

    //Returns the current gravity status of the player
    public bool GetGravStatus()
    {
        return gravChange;
    }

    //Moves the player on the X axis
    void MoveX()
    {
        float input = Input.GetAxis("Horizontal");
        player.velocity = new Vector2(input * speed, player.velocity.y);

        if (input < 0)
            direction = -1f;
        else 
            direction = 1f;
    }

    //Makes the player jump
    void Jump()
    {
        if(Input.GetButtonDown("Jump") && jumps != 0)
        {
            player.velocity = new Vector2(player.velocity.x, jumpAcc);
            jumps--;
        }
    }
    
    //Checks to see if the player is on the ground
    void GroundCheck()
    {
        Collider2D groundCol = Physics2D.OverlapCircle(groundChecker[currentGround].position, groundCheckRadius, ground);

        if(groundCol != null)
        {
            jumps = jumpCount;
        }
    }

    //Updates the fill ammount of the health bar
    void UpdateHealthBar()
    {
        hpBar.fillAmount = health / Manager.GetInstance.maxHealth;
    }

    //Slows down time
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

    //Changes the gravity of the player, going between 2 and -2
    void ChangeGravity()
    {
        if(Input.GetButtonDown("Fire3") && gravChange)
        {
            player.gravityScale *= -1;
            jumpAcc *= -1;
            aimer.FlipWeaponY();

            if (currentGround == 0)
            {
                Vector3 scale = new Vector3(1, 1, 1);
                gravityIcon.transform.localScale = scale;
                currentGround = 1;
            }
            else
            {
                Vector3 scale = new Vector3(1, -1, 1);
                gravityIcon.transform.localScale = scale;
                currentGround = 0;
            }
                
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject hit = collision.gameObject;
        //Checks if the player has hit a checkpoint
        if (hit.tag == "Checkpoint")
        {
            Transform tempTrans = hit.transform;
            tempTrans.position = new Vector3(tempTrans.position.x, tempTrans.position.y, 0);
            currentSpawnPoint = tempTrans;
        }
        //Checks if the player is in a gravity zone
        else if(hit.tag == "Gravityzone")
        {
            gravityBack.color = iconColor;
            gravChange = true;
        }
        else if(hit.tag == "End")
        {
            Manager.SetFinished(true);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject hit = collision.gameObject;
        //Checks if the player has left a gravity zone
        if (hit.tag == "Gravityzone")
        {
            gravityBack.color = Color.white;
            player.gravityScale = initGravscale;
            currentGround = 0;
            jumpAcc = initJumpAcc;
            gravChange = false;
        }
    }

    //Puts the player onto the most recent checkpoint hit
    void Respawn()
    {
        if(player.position.y < -50)
        {
            health -= 10;
            CheckHealth();
            this.transform.position = currentSpawnPoint.position;
        }
    }
    
    //Takes damage from the player
    public void TakeDamage(int damage)
    {
        health -= damage;
        CheckHealth();
    }

    //Checks the players health
    void CheckHealth()
    {
        if (health > Manager.GetInstance.maxHealth)
            health = Manager.GetInstance.maxHealth;
        else if (health <= 0)
            Die();
    }

    //Sets the player to not being alive
    void Die()
    {
        Manager.SetAlive(false);
    }

    //Heals the player
    public bool Heal(float healAmount)
    {
        if (health < Manager.GetInstance.maxHealth)
        {
            health += healAmount;
            CheckHealth();
            return true;
        }
        return false;
    }
}
