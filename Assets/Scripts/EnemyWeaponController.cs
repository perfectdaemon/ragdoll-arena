using UnityEngine;
using System.Collections;

public class EnemyWeaponController : MonoBehaviour
{
    private Rigidbody2D selfRigidBody2D;
    //private EnemyManager manager;

    // Use this for initialization
    void Start()
    {
        selfRigidBody2D = this.GetComponent<Rigidbody2D>();
        //manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemyManager>();
    }

    //void OnTriggerEnter2D(Collider2D collider)
    void OnCollisionEnter2D(Collision2D collision)
    {
        var collidedWith = collision.collider;
        if (collidedWith.gameObject.tag == "Player")
        {
            var hitVelocity = selfRigidBody2D.GetRelativePointVelocity(collidedWith.attachedRigidbody.position)
                - collidedWith.attachedRigidbody.velocity;
            var hitVelocityMagnitude = hitVelocity.magnitude;
            var playerController = collidedWith.GetComponent<FightPlayerController>();
            playerController.TakeDamage(hitVelocityMagnitude);
        }
    }
}
