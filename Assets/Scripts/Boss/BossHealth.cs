using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealth : MonoBehaviour
{
    public float health; 
    public float maxHealth = 300f;
    [SerializeField]
    public UIhealth healthBar;
    GameObject otherGameObject;

    void Start()
    {
        health = maxHealth - 1;
        healthBar.SetHealth(health, maxHealth);
        otherGameObject = GameObject.FindGameObjectWithTag("Player");
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= maxHealth/2)
        {
            GetComponent<Animator>().SetBool("IsEarn", true);
        }

        if (health <= 0)
        {
            GetComponent<Animator>().SetBool("Die", true);
            PlayerStats player = otherGameObject.GetComponent<PlayerStats>();
            player.currentHealth = player.playerData.MaxHealth;
            player.statusBar.SetState(player.currentHealth, player.playerData.MaxHealth);
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
