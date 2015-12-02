using UnityEngine;
using System.Collections;

public class FightEnemyController : MonoBehaviour
{
    public delegate void OnEnemyDie();

    private GameObject player;
    private Transform cachedTransform;
    private Rigidbody2D selfRB;

    private float Health;

    [Range(1, 10)]
    public float MaxHealth;

    public float Speed = 1;

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
        selfRB = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var toPlayer = player.transform.position - cachedTransform.position;
        toPlayer.Normalize();
        cachedTransform.right = toPlayer;
        selfRB.AddForce(toPlayer * Time.deltaTime * Speed, ForceMode2D.Impulse);
    }
}
