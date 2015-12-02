using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public float MaxComboPeriod;

    public int ScorePerKill;
    public int ScorePerComboKill;

    public Text KilledText;
    public GameObject EnemyPrefab;
    public Text ComboTextPrefab;

    private List<GameObject> enemies = new List<GameObject>();

    private int comboCounter;
    private float comboTimer;
    private int killedCount;
    public int Scores;

    public void AddEnemy()
    {
        var enemy = Instantiate<GameObject>(EnemyPrefab);
        enemy.transform.position = GetRandomPosition();
        var controller = enemy.GetComponent<FightEnemyController>();
        controller.OnDie += OnEnemyDie;
        enemy.SetActive(true);
        enemies.Add(enemy);
    }

    Vector2 GetRandomPosition()
    {
        return Random.insideUnitCircle * 7;
    }

    void UpdateScores()
    {
        KilledText.text = Scores.ToString();
    }

    void OnEnemyDie()
    {
        ++comboCounter;
        comboTimer = MaxComboPeriod;

        ++killedCount;
        Scores += ScorePerKill;
        UpdateScores();
        //KilledText.text = "Killed: " + killedCount.ToString();
    }

    // Use this for initialization
    void Start()
    {
        ComboTextPrefab.canvasRenderer.SetAlpha(0.0f);
        killedCount = 0;
        comboCounter = 0;
        Scores = 0;
        UpdateScores();
        InvokeRepeating("AddEnemy", 1.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (comboTimer > 0)
            comboTimer -= Time.deltaTime;
        // combo is over
        else if (comboCounter > 1)
        {
            // Add score
            Scores += ScorePerComboKill * comboCounter;
            UpdateScores();

            // Combo Text
            ComboTextPrefab.text = string.Format("{0}x Combo!", comboCounter);
            ComboTextPrefab.canvasRenderer.SetAlpha(1.0f);
            ComboTextPrefab.CrossFadeAlpha(0f, 2.5f, true);
            
            // Reset
            comboCounter = 0;
        }
    }
}
