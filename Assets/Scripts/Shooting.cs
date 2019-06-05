using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Weapon currentWeapon;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // There is a current Weapon
        if (currentWeapon)
        {
            // Is the fire button down?
            if (Input.GetButton("Fire1"))
            {
                if (currentWeapon.canShoot)
                {
                    currentWeapon.Shoot();
                }
            }
        }
    }
}
