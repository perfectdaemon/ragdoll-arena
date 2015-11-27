using UnityEngine;
using System.Collections;

public class MaceController : WeaponController
{
    private GameObject collidedWith;

    public MaceController()
    {
        WeaponTypeName = "Mace";
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collidedWith = collision.gameObject;
        if (collidedWith.tag == "Enemy")
        {
            base.OnCollisionWithEnemy(collidedWith);   
        }
    }
}
