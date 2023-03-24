using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IDamageable
{
    [Header("Enemy Stats")]
    public EnemyScriptableObject enemyData;
    Animator animator;
    public float currentMoveSpeed;
    float currentHealth;
    float currentDamage;
    public bool isDead = false;
    SpriteRenderer spriteRenderer;
    public float timeToColor = 0.3f;
    bool isTarget = false;
    Color defaultColor;
    EnemyMover enemyMover;
    Collider2D myCollider;
    Rigidbody2D myRb2d;
    StageTime stageTime;
    float furyDuration = 30f;
    List<float> furyTime = new List<float>() { 20f, 140f, 200f };
    bool isOnFuryStage = false;
    bool isFuryStage = false;
    bool isOutFuryStage = false;
    void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        enemyMover = GetComponent<EnemyMover>();
        myCollider = GetComponent<Collider2D>();
        myRb2d = GetComponent<Rigidbody2D>();
        stageTime = FindObjectOfType<StageTime>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isOnFuryStage)
        {
            CheckFuryStage();
        }
        if (isFuryStage)
        {
            OnFuryState();
            isOnFuryStage = true;
            isFuryStage = false;
        }
        if (isOnFuryStage)
        {
            CheckOutFuryStage();
        }
        if (isOutFuryStage)
        {
            OutFuryState();
            isOutFuryStage = false;
            isOnFuryStage=false;
        }

    }

    void CheckOutFuryStage()
    {
        foreach(float x in furyTime)
        {
            if(x + furyDuration <= stageTime.time)
            {
                isOutFuryStage = true;
                break;
            }
        }
    }

    void CheckFuryStage()
    {
        foreach(float x in furyTime)
        {
            if(x <= stageTime.time)
            {
                isFuryStage = true;
                break;
            }
        }
    }
    public void TakeDamage(float damage, bool isCrit)
    {
        Debug.Log("Take " + damage);
        if (!isCrit)
        {
           GameManager.DoFloatingText(new Vector3(transform.position.x, transform.position.y + 0.5f), damage.ToString(), Color.red);
        }
        else
        {
            GameManager.DoFloatingText(new Vector3(transform.position.x, transform.position.y + 0.5f),"Crit:"+ damage.ToString(), Color.magenta);
        }
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
    

    public void OnFuryState()
    {
        currentHealth *= 2;
        currentDamage *= 2;
        currentMoveSpeed *= 2;
    }

    public void OutFuryState()
    {
        currentHealth /= 2;
        currentDamage /= 2;
        currentMoveSpeed /= 2;
    }

    public void Kill()
    {
        
        animator.SetBool("Dead", true);
        SoundManager.PlayEffect("enemyDeath");
        isDead = true;
        Destroy(myCollider);
        Destroy(myRb2d);
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
        if (collision.collider.CompareTag("Player") && !isDead)
        {
            IDamageable damageable = collision.collider.GetComponent<IDamageable>();
            damageable.TakeDamage(currentDamage);
        }
    }

}
