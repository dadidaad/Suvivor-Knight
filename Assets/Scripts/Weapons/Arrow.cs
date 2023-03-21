using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    float speed;
    float damage;

    private Rigidbody2D myRb2d;

    // Start is called before the first frame update
    void Awake()
    {
        myRb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //conquaredDistance = Vector2.Distance(transform.position, startPosition);
        //if(conquaredDistance >= maxDistance)
        //{
        //    DisableObject();
        //}
    }

    private void DisableObject()
    {
        myRb2d  .velocity = Vector2.zero;
        gameObject.SetActive(false);
    }

    public void Initialize(Vector2 shootPosition)
    {
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(new Vector2(shootPosition.x * speed, shootPosition.y * speed), ForceMode2D.Impulse);
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(shootPosition));
        Destroy(gameObject, 5f);
    }

    public void Setup(float speed, float damage)
    {
        this.speed = speed;
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy") || collider.CompareTag("Drops"))
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            damageable.TakeDamage(damage);
        }
        if (collider.CompareTag("Boss"))
        {
            BossHealth bossHealth = collider.GetComponent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
                Destroy(gameObject);

            }

        }
        if (collider.CompareTag("SolidObject"))
        {
            Destroy(gameObject);
        }
    }

}
