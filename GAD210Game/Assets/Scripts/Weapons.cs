using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapons : MonoBehaviour
{
    public GameObject[] weapons;
    public Transform[] shotPos;
    public GameObject player;
    public Transform shotAngle;
    public Image weaponIcons;
    private int currentWeapon;
    private float setFireRate;
    private Gun gunComp;
    private SpriteRenderer gunSprite;
    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = 0;
        gunComp = weapons[currentWeapon].GetComponent<Gun>();
        setFireRate = gunComp.fireRate/2;
        gunSprite = weapons[currentWeapon].GetComponent<SpriteRenderer>();
        GetComponent<SpriteRenderer>().sprite = gunSprite.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (setFireRate <= 0)
        {
            if (Input.GetButtonDown("Fire1") && !gunComp.auto)
            {
                Shoot();
                setFireRate = gunComp.fireRate;
            }
            else if(Input.GetButton("Fire1") && gunComp.auto)//Auto weapons can be held down
            {
                Shoot();
                setFireRate = gunComp.fireRate;
            }
                
        }
        else
        {
            setFireRate -= Time.deltaTime;
        }

        if (ScrollWeapon())
            ChangeWeapon();

        if (KeyboardWeapon())
            ChangeWeapon();
        
    }

    public void Shoot()
    {
        Instantiate(gunComp.projectile, shotPos[currentWeapon].position, transform.rotation);
    }

    bool ScrollWeapon()
    {
        Debug.Log(currentWeapon);
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (currentWeapon == 0)
                currentWeapon = weapons.Length-1;
            else
                currentWeapon--;

            return true;
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (currentWeapon == weapons.Length-1)
                currentWeapon = 0;
            else
                currentWeapon++;

            return true;
        }

        return false;
    }


    bool KeyboardWeapon()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
            return true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentWeapon = 3;
            return true;
        }

        return false;
    }

    void ChangeWeapon()
    {
        gunComp = weapons[currentWeapon].GetComponent<Gun>();
        setFireRate = gunComp.fireRate/2;
        gunSprite = weapons[currentWeapon].GetComponent<SpriteRenderer>();
        GetComponent<SpriteRenderer>().sprite = gunSprite.sprite;
    }

    public int GetWeaponPosition()
    {
        return currentWeapon;
    }
}
