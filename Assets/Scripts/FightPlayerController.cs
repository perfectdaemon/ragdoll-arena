using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class FightPlayerController : MonoBehaviour
{
    private bool isDead;
    private Vector2 touchPos;
    private Vector2 controlVector, forceAtPos;
    private Transform selfTransform;
    private Rigidbody2D selfRigidBody2d;

    private float Health;

    public float MaxHealth;

    public Vector2 ForcePosOffset;

    public float ControlPower;

    public GameObject DamageTakenText;
    public Transform WorldCanvas;
    public GameObject DeadText;
    public GameObject HPText;

    public void TakeDamage(float damage)
    {
        if (damage < 1)
            return;

        // Damage Text
        var damageTaken = Instantiate<GameObject>(DamageTakenText);
        var damageTakenText = damageTaken.GetComponent<Text>();
        damageTakenText.rectTransform.SetParent(WorldCanvas, false);
        damageTakenText.rectTransform.position = selfTransform.position + new Vector3(0f, -0.5f, 1f); ;
        damageTakenText.text = string.Format("-{0}", damage.ToString("F0"));

        Health -= damage;
        UpdateHPText();
        if (Health <= 0)
            Die();
    }

    public void Reload()
    {
        Time.timeScale = 1;
        Application.LoadLevel(Application.loadedLevelName);
    }

    void UpdateHPText()
    {
        HPText.GetComponent<Text>().text = string.Format("HP: {0}", Health.ToString("F0"));
    }

    void Die()
    {
        isDead = true;
        Time.timeScale = 0;
        DeadText.SetActive(true);
        DeadText.GetComponent<Text>().text += "\r\n" + GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemyManager>().Scores;
    }

    // Use this for initialization
    void Start()
    {
        this.selfTransform = this.GetComponent<Transform>();
        this.selfRigidBody2d = this.GetComponent<Rigidbody2D>();
        Health = MaxHealth;
        UpdateHPText();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;
        if (Input.GetMouseButton(0))
        {
            touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            forceAtPos = selfTransform.TransformPoint(ForcePosOffset);
            controlVector = (touchPos - forceAtPos);
            controlVector.Normalize();

            selfRigidBody2d.AddForceAtPosition(controlVector * ControlPower * Time.deltaTime, forceAtPos, ForceMode2D.Impulse);
        }
    }
}
