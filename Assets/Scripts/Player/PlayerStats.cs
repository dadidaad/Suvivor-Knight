using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class PlayerStats : MonoBehaviour
{
    public PlayerScriptableObject playerData;
    public GameObject currentCharacter;
    public float currentHealth;
    public float currentRecovery;
    public float currentMoveSpeed;
    PlayerAnimator animator;
    bool isDead = false;
    [SerializeField]
    StatusBar statusBar;
    // Start is called before the first frame update

    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvinciable;

    void Awake()
    {
        currentHealth = playerData.MaxHealth;
        currentRecovery = playerData.Recovery;
        currentMoveSpeed = playerData.MoveSpeed;
        currentCharacter = playerData.Character;
        animator = GetComponentInChildren<PlayerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else if (isInvinciable)
        {
            isInvinciable = false;
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isInvinciable)
        {
            Debug.Log("Player take " + damage);
            currentHealth -= damage;

            statusBar.SetState(currentHealth, playerData.MaxHealth);
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

        StartCoroutine(TimeManager.PasueGame(2));
    }
}
