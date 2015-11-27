using UnityEngine;
using System.Collections;

public class SwordController : WeaponController
{
    private GameObject collidedWith;

    public SwordController()
    {
        WeaponTypeName = "Mace";
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        collidedWith = collider.gameObject;
        if (collidedWith.tag == "Enemy")
        {
            base.OnCollisionWithEnemy(collidedWith);
        }
    }
}
