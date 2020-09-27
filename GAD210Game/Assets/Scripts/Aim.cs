using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public GameObject weapon;
    private SpriteRenderer w_sprite;
    public bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        w_sprite = weapon.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the player is alive, move the weapon to face the mouse
        if(Manager.GetAlive() && !Manager.GetPaused())
        {
            Vector3 mouseWeaponDifference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - weapon.transform.position;
            float rotation = Mathf.Atan2(mouseWeaponDifference.y, mouseWeaponDifference.x) * Mathf.Rad2Deg;

            weapon.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
            LookingDirection(rotation);
        }
            
    }

    void LookingDirection(float rot)
    {
        if (rot > -90 && rot < 90)
        {
            w_sprite.flipY = false;
            facingRight = true;
        }
        else
        {
            w_sprite.flipY = true;
            facingRight = true;
        }
            
    }

    public void FlipWeaponY()
    {
            w_sprite.flipY = false;
    }
}
