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
    [SerializeField] UpgradePanelManager upgradePanel;
    [SerializeField] List<UpgradeData> upgrades;
    List<UpgradeData> selectedUpgrades;
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
            List<UpgradeData> list = GetUpgrades(2);
            selectedUpgrades= list;
            upgradePanel.OpenPanel(list);
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

    public List<UpgradeData> GetUpgrades(int count)
    {
        List<UpgradeData> listUpgrade = new List<UpgradeData>();
        if (count > upgrades.Count)
        {
            count = upgrades.Count;
        }

        for (int i = 0; i < count; i++)
        {
            listUpgrade.Add(upgrades[Random.Range(0, upgrades.Count)]);
        }
        return listUpgrade;
    }

    public void Upgrade(int selectedUpgradeId)
    {
        UpgradeData upgradeData = selectedUpgrades[selectedUpgradeId];

        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUpgrade:
                
                break;
            case UpgradeType.PlayerUpgrade:
                float currentSpeed = GetComponent<PlayerStats>().currentMoveSpeed;
                Debug.Log("Current: " + currentSpeed);
                GetComponent<PlayerStats>().currentMoveSpeed += 2;
                Debug.Log("After: " + GetComponent<PlayerStats>().currentMoveSpeed);
                break;
            case UpgradeType.WeaponUnlock:
                break;
        }
    }
}
