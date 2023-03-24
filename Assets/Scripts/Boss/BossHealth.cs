using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealth : MonoBehaviour, IDamageable
{
    public float health; 
    public float maxHealth = 300f;
    public bool isEarn = false;
    [SerializeField]
    public UIhealth healthBar;

    void Start()
    {
        health = maxHealth -1;
        healthBar.SetHealth(health, maxHealth);
    }

    public void TakeDamage(float damage, bool isCrit)
    {
        health -= damage;
        if (health <= maxHealth/2)
        {
            GetComponent<Animator>().SetBool("IsEarn", true);
            isEarn = true;
        }

        if (health <= 0)
        {
            GetComponent<Animator>().SetBool("Die", true);
            //PlayerStats player = otherGameObject.GetComponent<PlayerStats>();
            //player.currentHealth = player.playerData.MaxHealth;
            //player.statusBar.SetState(player.currentHealth, player.playerData.MaxHealth);
            Notification notification = GameManager.Instance.notificationPanel.GetComponent<Notification>();
            StartCoroutine(notification.ShowMessage("Win!!", 2f));
            Time.timeScale = 0f;
            //if (player.currentHealth + maxHealth < player.playerData.MaxHealth)
            //{            
            //    player.currentHealth += maxHealth;
            //} else
            //{
            //    player.currentHealth = player.playerData.MaxHealth;
            //}
            Destroy(gameObject);
        }
    }

}
