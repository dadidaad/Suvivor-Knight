using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesctructableObject : MonoBehaviour, IDamageable
{
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(float damage)
    {
        animator.SetTrigger("Break");
        Destroy(gameObject);
    }
}
