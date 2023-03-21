using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ExperienceGem : MonoBehaviour, ICollectible
{
    public bool isSuck = false;
    public int experienceGranted;
    Rigidbody2D rb2d;
    public float speed = 30.0f;
    //Distance to start moving
    public float minDistance = 0.09f;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    public void Collect()
    {
        Level level = FindObjectOfType<Level>();
        level.IncreaseExperience(experienceGranted);
    }

    void FixedUpdate()
    {
        if (isSuck)
        {
            PlayerMover target = FindObjectOfType<PlayerMover>();
            Vector3 dir = (target.transform.position - rb2d.transform.position).normalized;
            //Check if we need to follow object then do so 
            if (Vector3.Distance(target.transform.position, rb2d.transform.position) > minDistance)
            {
                rb2d.MovePosition(rb2d.transform.position + dir * speed * Time.fixedDeltaTime);
            }
        }
    }
}
