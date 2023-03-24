using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int attackDamage = 20;
    public int enragedAttackDamage = 40;
    public Vector3 attackOffset;
    public float attackRange = 1;
    [SerializeField]
    public LayerMask attackMask;
    public void Attack()
    {
        DetectColliders(CalcutePositionAttack(), attackDamage);
    }

    public void EnragedAttack()
    {
        DetectColliders(CalcutePositionAttack(), enragedAttackDamage);
    }

    Vector3 CalcutePositionAttack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
        return pos;
    }
    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }

    public void DetectColliders(Vector3 position, float damage)
    {
        Collider2D collider = Physics2D.OverlapCircle(position, attackRange, attackMask);
        if(collider != null){
            IDamageable damageable = collider.GetComponent<IDamageable>();
            damageable.TakeDamage(damage);
        }

    }
}
