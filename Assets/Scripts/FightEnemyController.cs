using UnityEngine;
using System.Collections;

public class FightEnemyController : MonoBehaviour
{
    public delegate void OnEnemyDie();

    private GameObject player;
    private Transform cachedTransform;

    [Range(1, 10)]
    public float MaxHealth;
    public float Health;

    public event OnEnemyDie OnDie;

    public void DoDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
            Die();
    }

    public void Die()
    {
        OnDie();
        Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        OnDie += () => { };
        Health = MaxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        cachedTransform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        cachedTransform.right = (player.transform.position - cachedTransform.position).normalized;
    }
}
