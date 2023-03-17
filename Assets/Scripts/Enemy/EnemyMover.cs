using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [Header("Enemy stats")]
    public EnemyScriptableObject enemyData;
    Transform player;
    float currentMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMover>().transform;
        currentMoveSpeed = enemyData.MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyStats enemyStats = GetComponent<EnemyStats>();
        if (!enemyStats.isDead)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position,
            currentMoveSpeed * Time.deltaTime
            );
        }
    }
}
