using UnityEngine;
using System.Collections;

public class SwordController : WeaponController
{
    private GameObject collidedWith;    
    private Vector2 hitVelocity;
    private float hitVelocityMagnitude;

    public SwordController()
    {
        WeaponTypeName = "Mace";
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        collidedWith = collider.gameObject;        
        if (collidedWith.tag == "Enemy")
        {

            hitVelocity = selfRigidBody2D.GetRelativePointVelocity(collider.attachedRigidbody.position) 
                - collider.attachedRigidbody.velocity;
                //
            hitVelocityMagnitude = hitVelocity.magnitude;
            base.OnCollisionWithEnemy(collidedWith, hitVelocityMagnitude);
        }
    }
}
