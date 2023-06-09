using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [Header("Enemy stats")]
    public EnemyScriptableObject enemyData;
    Transform player;
    float currentMoveSpeed;
    Rigidbody2D rb2d;
    Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMover>().transform;
        rb2d = GetComponent<Rigidbody2D>();
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        EnemyStats enemyStats = GetComponent<EnemyStats>();
        if (!enemyStats.isDead)
        {
            currentMoveSpeed += enemyData.MoveSpeed * Time.deltaTime;
            currentMoveSpeed = Mathf.Clamp(currentMoveSpeed, 0, enemyData.MoveSpeed);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position,
            currentMoveSpeed * Time.deltaTime
            );
            rb2d.velocity = new Vector2(0f, 0f) * currentMoveSpeed;
        }
        else
        {
            lastPosition = transform.position;
        }
    }
    public Vector3 GetLastPosition()
    {
        return lastPosition;
    }
    
}
