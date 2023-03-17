using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class PlayerStats : MonoBehaviour
{
    public PlayerScriptableObject playerData;
    public GameObject currentCharacter;
    float currentHealth;
    float currentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    PlayerAnimator animator;
    bool isDead = false;
    [SerializeField]
    StatusBar statusBar;
    [SerializeField]
    ExprienceBar exprienceBar;
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;
    // Start is called before the first frame update

    [System.Serializable]
    public class LevelRange {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    public List<LevelRange> levelRanges;

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

    void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;
        exprienceBar.UpdateExprienceSlider(experience, experienceCap);
        exprienceBar.SetLevelText(level);

    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        exprienceBar.UpdateExprienceSlider(experience, experienceCap);
        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if(experience >= experienceCap)
        {
            level++;
            exprienceBar.SetLevelText(level);
            experience -= experienceCap;
            int experienceCapIncrease = 0;
            foreach(var levelRange in levelRanges)
            {
                if (level >= levelRange.startLevel && level <= levelRange.endLevel)
                {
                    experienceCapIncrease = levelRange.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
        }
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
