using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip walkSound, attackSound, hitSound, enemyDeathSound;

    static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        walkSound = Resources.Load<AudioClip>("Audio/walk");
        attackSound = Resources.Load<AudioClip>("Audio/attack");
        hitSound = Resources.Load<AudioClip>("Audio/hit");
        enemyDeathSound = Resources.Load<AudioClip>("Audio/enemyDeath");
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlayEffect(string action)
    {
        switch (action)
        {
            case "walk":
                audioSource.PlayOneShot(walkSound);
                break;
            case "attack":
                audioSource.PlayOneShot(attackSound);
                break;
            case "hit":
                audioSource.PlayOneShot(hitSound);
                break;
            case "enemyDeath":
                audioSource.PlayOneShot(enemyDeathSound);
                break;

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
