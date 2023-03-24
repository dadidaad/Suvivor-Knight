using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [HideInInspector]
    public WeaponScriptableObject weaponData;
    PlayerStats stats;
    public Vector2 PointerPosition { get; set; }
    [HideInInspector]
    public Animator animator;
    public float delay = 0.1f;
    [HideInInspector]
    public GameObject arrowPrefab;
    private bool attackBlocked;
    public float currentCoolDown;
    public float currentDamage;
    public float currentSpeed;
    public float currentCritChance;
    public bool isCrit = false;
    public float radius;
    [HideInInspector]
    public Transform circleOrigin;
    public bool IsAttacking { get; private set; }
    public WeaponManager weaponManager;
    SpriteRenderer weaponRenderer, characterRenderer;
    PlayerStats playerStats;
    private void Awake()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
    }
    private void Start()
    {
        IsAttacking = false;
        playerStats = GetComponentInParent<PlayerStats>();
        characterRenderer = playerStats.playerData.Character.GetComponent<SpriteRenderer>();
    }
    public void ResetIsAttacking()
    {
        IsAttacking = false;
        
    }
    private void Update()
    {
        if (weaponManager.isSetup && !playerStats.isDead)
        {
            currentCoolDown -= Time.deltaTime;
            if (currentCoolDown <= 0f)
            {
                checkCritChance();
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

    }
    public void Attack()
    {
        if (attackBlocked)
            return;
        animator.SetTrigger("Attack");
        SoundManager.PlayEffect("attack");
        if (weaponData.Type == WeaponScriptableObject.TypeWeapon.Projectile)
        {
            ProjectileAttack();
        }
        IsAttacking = true;
        attackBlocked = true;
        StartCoroutine(DelayAttack());
        currentCoolDown = weaponData.CooldownDuration;
        if (isCrit)
        {
            currentDamage /= 2;
            isCrit = false;
        }
    }
    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attackBlocked = false;
        ResetIsAttacking();
    }

    private void ProjectileAttack()
    {
        GameObject arrow = Instantiate(weaponData.ProjectileWeapon, circleOrigin.position, Quaternion.identity);
        Vector2 shootDir = (PointerPosition - (Vector2)circleOrigin.position).normalized;
        arrow.GetComponent<Arrow>().Setup(currentSpeed, currentDamage, isCrit);
        arrow.GetComponent<Arrow>().Initialize(shootDir);
    }
    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, radius))
        {
            if (collider.CompareTag("Enemy") || collider.CompareTag("Drops") || collider.CompareTag("Boss"))
            {
                IDamageable damageable = collider.GetComponent<IDamageable>();
                SoundManager.PlayEffect("hit");
                damageable.TakeDamage(currentDamage, isCrit);
            }
        }
    }

    public void ChooseWeapon(WeaponScriptableObject weaponChoose)
    {
        //weaponData = listWeaponData[pressedButtonId];
        weaponData = weaponChoose;
        currentCoolDown = weaponData.CooldownDuration;
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCritChance = weaponData.CritChance;
        weaponRenderer = weaponData.Prefab.GetComponent<SpriteRenderer>();
        if(weaponData.ProjectileWeapon != null)
        {
            arrowPrefab = weaponData.ProjectileWeapon;
        }
        //if (pressedButtonId == 0)
        //{
        //    transform.Find("Bone").gameObject.SetActive(true);
        //}
        //else
        //{
        //    transform.Find("Machete").gameObject.SetActive(true);
        //}
    }

    void checkCritChance()
    {
        float currentChance = Random.value;
        if(currentCritChance < currentChance)
        {
            currentDamage *= 2;
            isCrit = true;
        }
    }
}
