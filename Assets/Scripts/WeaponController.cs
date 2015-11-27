using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour
{
    protected Rigidbody2D selfRigidBody2D;
    protected Transform selfTransform, enemyTransform;
    protected GameObject newBloodParticle;
    protected FightEnemyController enemyController;
    protected Vector2 hitVelocity;
    protected float hitVelocityMagnitude;

    protected string WeaponTypeName;

    protected void Start()
    {
        selfTransform = this.GetComponent<Transform>();
        selfRigidBody2D = this.GetComponent<Rigidbody2D>();
    }

    protected void OnCollisionWithEnemy(GameObject enemy)
    {
        enemyTransform = enemy.transform;
        enemyController = enemy.GetComponent<FightEnemyController>();
        hitVelocity = selfRigidBody2D.GetRelativePointVelocity(enemyTransform.position);
        hitVelocityMagnitude = hitVelocity.magnitude;
        
        if (hitVelocityMagnitude < MinForceToHit)
            return;

        // Blood
        newBloodParticle = Instantiate<GameObject>(BloodParticles);
        newBloodParticle.transform.position = enemyTransform.position;
        newBloodParticle.transform.up = (enemyTransform.position - selfTransform.position).normalized;
        Destroy(newBloodParticle, 1);

        // Do damage                      
        print(WeaponTypeName + ": " + hitVelocityMagnitude.ToString());
                
        enemyController.DoDamage(Mathf.Clamp(hitVelocityMagnitude, MinForceToHit, MaxForceToHit));
    }

    public GameObject BloodParticles;

    public float MinForceToHit;
    public float MaxForceToHit;
}
