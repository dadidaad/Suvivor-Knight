using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Enemy Stats")]
    public EnemyScriptableObject enemyData;
    Animator animator;
    float currentMoveSpeed;
    float currentHealth;
    float currentDamage;
    public bool isDead = false;
    SpriteRenderer spriteRenderer;
    public float timeToColor = 0.3f;
    bool isTarget = false;
    Color defaultColor;
    EnemyMover enemyMover;
    BoxCollider2D boxCollider;  
    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        enemyMover = GetComponent<EnemyMover>();
        boxCollider =  GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Take " + damage);
        GameManager.DoFloatingText(new Vector3(transform.position.x, transform.position.y + 0.5f), damage.ToString(), Color.red);
        currentHealth -= damage;
        if (!isTarget)
        {
            isTarget = true;
            StartCoroutine("SwitchColor");
        }
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        
        animator.SetBool("Dead", true);
        isDead = true;
        Destroy(boxCollider);
        Destroy(gameObject, 1);
    }

    IEnumerator SwitchColor()
    {
        spriteRenderer.color = new Color(1f, 0.30196078f, 0.30196078f);
        yield return new WaitForSeconds(timeToColor);
        spriteRenderer.color = defaultColor;
        isTarget = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            Attack(playerStats);
        }
    }

    private void OnDestroy()
    {
        EnemySpawn enemySpawn = FindObjectOfType<EnemySpawn>();
        if(enemySpawn != null)
        {
            enemySpawn.OnEnemyKilled();
        }
    }
    private void Attack(PlayerStats playerStats)
    {
        if(playerStats != null)
        {
            playerStats.TakeDamage(currentDamage);
        }
    }
}
