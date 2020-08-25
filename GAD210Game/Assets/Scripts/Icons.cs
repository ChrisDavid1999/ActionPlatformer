using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Icons : MonoBehaviour
{
    public Weapons positions;
    public int iconPosition;
    public Color selected;
    public Color notSelected;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        CheckWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        CheckWeapon();
    }

    void CheckWeapon()
    {
        if (positions.GetWeaponPosition() == iconPosition)
            image.color = selected;
        else
            image.color = notSelected;
    }
}
