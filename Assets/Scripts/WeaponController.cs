using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponController : MonoBehaviour
{    
    private Transform enemyTransform;
    private GameObject newBloodParticle, newDamageText;
    private FightEnemyController enemyController;    
    private Text newDamageTextText;

    protected Rigidbody2D selfRigidBody2D;
    protected Transform selfTransform;
    protected string WeaponTypeName;

    protected void Start()
    {
        selfTransform = this.GetComponent<Transform>();
        selfRigidBody2D = this.GetComponent<Rigidbody2D>();
    }

    protected void OnCollisionWithEnemy(GameObject enemy, float power)
    {
        power *= WeaponPowerMiltiplier;
        enemyTransform = enemy.transform;
        enemyController = enemy.GetComponent<FightEnemyController>();

        if (power < MinForceToHit)
            return;

        // Blood
        newBloodParticle = Instantiate<GameObject>(BloodParticles);
        newBloodParticle.transform.position = enemyTransform.position;
        newBloodParticle.transform.up = (enemyTransform.position - selfTransform.position).normalized;
        Destroy(newBloodParticle, 1);

        // Do damage                      
        enemyController.DoDamage(Mathf.Clamp(power, MinForceToHit, MaxForceToHit));
        //print(WeaponTypeName + ": " + hitVelocityMagnitude.ToString());

        // Damage Text
        newDamageText = Instantiate<GameObject>(DamageText);
        newDamageTextText = newDamageText.GetComponent<Text>();
        newDamageTextText.rectTransform.SetParent(WorldCanvas, false);
        newDamageTextText.rectTransform.position = enemyTransform.position + new Vector3(0f, 0.5f, 0f); ;
        newDamageTextText.text = string.Format("{0}", power.ToString("F0"));
    }

    public GameObject BloodParticles;

    public GameObject DamageText;
    public Transform WorldCanvas;

    public float MinForceToHit;
    public float MaxForceToHit;

    public float WeaponPowerMiltiplier = 1;
}
