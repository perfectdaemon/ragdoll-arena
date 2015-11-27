using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public int KilledCount;

    public Text KilledText;
    public GameObject EnemyPrefab;

    public List<GameObject> enemies = new List<GameObject>();

    public void AddEnemy()
    {
        var enemy = Instantiate<GameObject>(EnemyPrefab);
        enemy.transform.position = GetRandomPosition();
        var controller = enemy.GetComponent<FightEnemyController>();
        controller.OnDie += OnEnemyDie;
        enemies.Add(enemy);
    }

    Vector2 GetRandomPosition()
    {
        return Random.insideUnitCircle * 4;
    }

    void OnEnemyDie()
    {
        ++KilledCount;
        KilledText.text = "Убито: " + KilledCount.ToString();
    }

    // Use this for initialization
    void Start()
    {
        KilledCount = 0;
        InvokeRepeating("AddEnemy", 1.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
