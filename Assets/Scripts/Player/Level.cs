using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    ExprienceBar exprienceBar;
    [SerializeField]
    GameObject weapon;
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
                GetComponent<PlayerStats>().currentMoveSpeed += (upgradeData.value * GetComponent<PlayerStats>().currentMoveSpeed);
                break;
            case Star.Health:
                GetComponent<PlayerStats>().currentHealth += (upgradeData.value * GetComponent<PlayerStats>().currentHealth);
                GetComponent<PlayerStats>().health += GetComponent<PlayerStats>().currentHealth *= (upgradeData.value * GetComponent<PlayerStats>().currentHealth);
                ;
                break;
            case Star.Recover:
                if(GetComponent<PlayerStats>().currentRecovery == 0)
                {
                    GetComponent<PlayerStats>().currentRecovery = 5;
                }
                GetComponent<PlayerStats>().currentRecovery += (upgradeData.value * GetComponent<PlayerStats>().currentRecovery);
                break;
            default:
                return;
        }
    }

    private void WeaponUpgrade(UpgradeData upgradeData)
    {
        GameObject weapon = GameObject.FindGameObjectWithTag("Weapon");
        switch (upgradeData.star)
        {
            case Star.Speed:
                weapon.GetComponent<WeaponController>().currentSpeed += (upgradeData.value * GetComponent<WeaponController>().currentSpeed);
                break;
            case Star.Damage:
                weapon.GetComponent<WeaponController>().currentDamage += (upgradeData.value * GetComponent<WeaponController>().currentDamage);
                break;
            case Star.Cooldown:
                weapon.GetComponent<WeaponController>().currentCoolDown += (upgradeData.value * GetComponent<WeaponController>().currentCoolDown);
                break;
            default:
                return;
        }
    }
}
