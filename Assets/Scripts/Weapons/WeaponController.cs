using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    public List<WeaponScriptableObject> listWeaponData;
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    PlayerStats stats;
    public Vector2 PointerPosition { get; set; }
    public Animator animator;
    public float delay = 0.3f;
    public GameObject arrowPrefab;
    private bool attackBlocked;
    public float currentCoolDown;
    public float currentDamage;
    public float currentSpeed;
    public float radius;
    public Transform circleOrigin;
    public bool IsAttacking { get; private set; }
    SpriteRenderer weaponRenderer, characterRenderer;
    PlayerStats playerStats;
    private void Start()
    {
        panel.active = true;

    }
    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }
    private void Update()
    {
        if (panel.active == false)
        {
            if (!GetComponentInParent<PlayerStats>().isDead)
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

        }
    }
    public void Attack()
    {
        if (attackBlocked)
            return;
        if (animator != null && animator.isActiveAndEnabled)
        {
            animator.SetTrigger("Attack");
        }
        
        if(weaponData.Type == WeaponScriptableObject.TypeWeapon.Projectile)
        {
            ProjectileAttack();
        }
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

    private void ProjectileAttack()
    {
        GameObject arrow = Instantiate(arrowPrefab, circleOrigin.position, Quaternion.identity);
        Vector2 shootDir = (PointerPosition - (Vector2)circleOrigin.position).normalized;
        arrow.GetComponent<Arrow>().Setup(currentSpeed, currentDamage);
        arrow.GetComponent<Arrow>().Initialize(shootDir);
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

    public void ChooseWeapon(int pressedButtonId)
    {
        weaponData = listWeaponData[pressedButtonId];
        animator = weaponData.Prefab.GetComponent<Animator>();
        currentCoolDown = weaponData.CooldownDuration;
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        weaponRenderer = weaponData.Prefab.GetComponent<SpriteRenderer>();
        playerStats = GetComponentInParent<PlayerStats>();
        characterRenderer = playerStats.playerData.Character.GetComponent<SpriteRenderer>();
        if (pressedButtonId == 0)
        {
            transform.FindChild("Bone").gameObject.SetActive(true);
        }
        else
        {
            transform.FindChild("Machete").gameObject.SetActive(true);
        }
        panel.active = false;
    }
}
