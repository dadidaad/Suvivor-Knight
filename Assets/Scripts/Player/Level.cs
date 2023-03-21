using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    WeaponController weaponController;
    PlayerStats playerStats;
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
        weaponController = GetComponentInChildren<WeaponController>();
        playerStats = GetComponent<PlayerStats>();
        foreach (UpgradeData upgradeData in upgrades)
        {
            if(upgradeData.upgradeType == UpgradeType.WeaponUpgrade
                && upgradeData.star == Star.Speed 
                && weaponController.weaponData.Type != WeaponScriptableObject.TypeWeapon.Projectile)
            {
                upgrades.Remove(upgradeData);
            }
        }

    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
        exprienceBar.UpdateExprienceSlider(experience, experienceCap);
    }

    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            List<UpgradeData> list = GetUpgrades(3);
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
                WeaponUpgrade(upgradeData);
                break;
            case UpgradeType.PlayerUpgrade:
                PlayerUpgrade(upgradeData);
                break;
            case UpgradeType.WeaponUnlock:
                
                break;
        }
    }

    private void PlayerUpgrade(UpgradeData upgradeData)
    {
        switch (upgradeData.star)
        {
            case Star.MoveSpeed:
                playerStats.currentMoveSpeed += (upgradeData.value * playerStats.currentMoveSpeed);
                break;
            case Star.Health:
                playerStats.currentHealth += (upgradeData.value * playerStats.currentHealth);
                playerStats.health +=  (upgradeData.value * playerStats.currentHealth);
                ;
                break;
            case Star.Recover:
                if(playerStats.currentRecovery == 0)
                {
                    playerStats.currentRecovery = 2;
                }
                playerStats.currentRecovery += (upgradeData.value * playerStats.currentRecovery);
                break;
            default:
                return;
        }
    }

    private void WeaponUpgrade(UpgradeData upgradeData)
    {
        switch (upgradeData.star)
        {
            case Star.Speed:
                weaponController.currentSpeed += (upgradeData.value * weaponController.currentSpeed);
                break;
            case Star.Damage:
                weaponController.currentDamage += (upgradeData.value * weaponController.currentDamage);
                break;
            case Star.Cooldown:
                weaponController.currentCoolDown += (upgradeData.value * weaponController.currentCoolDown);
                break;
            default:
                return;
        }
    }
}
