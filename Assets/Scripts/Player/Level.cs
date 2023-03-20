using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStats;

public class Level : MonoBehaviour
{
    [SerializeField]
    ExprienceBar exprienceBar;
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    public List<LevelRange> levelRanges;
    void Start()
    {
        //Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), FindObjectOfType<EnemyStats>().GetComponent<BoxCollider2D>());
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
        if (experience >= experienceCap)
        {
            level++;
            exprienceBar.SetLevelText(level);
            experience -= experienceCap;
            int experienceCapIncrease = 0;
            foreach (var levelRange in levelRanges)
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
}
