using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class PlayerStats : MonoBehaviour, IDamageable
{
    public PlayerScriptableObject playerData;
    public GameObject currentCharacter;
    public float currentHealth, currentRecovery, currentMoveSpeed;

    PlayerAnimator animator;
    public float health;
    [HideInInspector]
    public bool isDead = false;
    [SerializeField]
    StatusBar statusBar;
    // Start is called before the first frame update

    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvinciable;
    float timeInterval = 0;
    void Awake()
    {
        health = playerData.MaxHealth;
        currentHealth = health;
        currentRecovery = playerData.Recovery;
        currentMoveSpeed = playerData.MoveSpeed;
        currentCharacter = playerData.Character;
        animator = GetComponentInChildren<PlayerAnimator>();
        statusBar.SetState(currentHealth, health);
    }

    // Update is called once per frame
    void Update()
    {
        timeInterval += Time.deltaTime;
        if(invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvinciable)
        {
            isInvinciable = false;
        }

        if(timeInterval >= 1f)
        {
            timeInterval = 0;
            currentHealth += currentRecovery;
            statusBar.SetState(currentHealth, health);
        }
        if (currentHealth > health)
        {
            currentHealth = health;
        }
        
    }

    public void TakeDamage(float damage)
    {
        if (!isInvinciable)
        {
            Debug.Log("Player take " + damage);
            currentHealth -= damage;

            statusBar.SetState(currentHealth, health);
            invincibilityTimer = invincibilityDuration;
            isInvinciable = true; 
            if (currentHealth <= 0)
            {
                Kill();
            }
        }
    }
    public void Kill()
    {

        animator.DeadState();
        isDead = true;
        //Destroy(gameObject, 2);

        StartCoroutine(TimeManager.PasueGame(1));
    }

    public void Heal(float amount)
    {
        currentHealth += health * amount;
        if(currentHealth > health)
        {
            currentHealth = health;
        }
        statusBar.SetState(currentHealth, health);

    }
}
