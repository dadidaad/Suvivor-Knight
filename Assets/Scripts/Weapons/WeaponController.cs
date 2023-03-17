using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    PlayerStats stats;
    public Vector2 PointerPosition { get; set; }

    public Animator animator;
    public float delay = 0.3f;
    private bool attackBlocked;
    float currentCoolDown;
    float currentDamage;
    public float radius;
    public Transform circleOrigin;
    public bool IsAttacking { get; private set; }
    SpriteRenderer weaponRenderer, characterRenderer;
    private void Start()
    {
        currentCoolDown = weaponData.CooldownDuration;
        currentDamage = weaponData.Damage;
        weaponRenderer = weaponData.Prefab.GetComponent<SpriteRenderer>();
        
        characterRenderer = gameObject.GetComponentInParent<PlayerStats>().playerData.Character.GetComponent<SpriteRenderer>();

    }
    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }
    private void Update()
    {
        currentCoolDown -= Time.deltaTime;
        if (currentCoolDown <= 0f)
        {
            Attack();
        }
        if (IsAttacking)
            return;
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            scale.y = -1;
        }
        else if (direction.x > 0)
        {
            scale.y = 1;
        }
        transform.localScale = scale;

        if (transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        }
        else
        {
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        }

    }
    public void Attack()
    {
        if (attackBlocked)
            return;
        animator.SetTrigger("Attack");
        IsAttacking = true;
        attackBlocked = true;
        StartCoroutine(DelayAttack());
        currentCoolDown = weaponData.CooldownDuration;
    }
    private IEnumerator DelayAttack() 
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
        ResetIsAttacking();
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {
            if (collider.CompareTag("Enemy"))
            {
                EnemyStats enemyStats = collider.GetComponent<EnemyStats>();
                enemyStats.TakeDamage(currentDamage);
            }
        }
    }

}
