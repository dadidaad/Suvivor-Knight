using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Collider2D collider2D;
    [SerializeField]
    private float acceleration = 50, deacceleration = 100;
    [SerializeField]
    private float currentSpeed = 0;
    private Vector2 oldMovementInput;
    public Vector2 MovementInput { get; set; }
    PlayerStats playerStats;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();  
        collider2D = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (!playerStats.isDead)
        {
            if (MovementInput.magnitude > 0 && currentSpeed >= 0)
            {
                oldMovementInput = MovementInput;
                currentSpeed += acceleration * playerStats.currentMoveSpeed * Time.deltaTime;
            }
            else
            {
                currentSpeed -= deacceleration * playerStats.currentMoveSpeed * Time.deltaTime;
            }
            currentSpeed = Mathf.Clamp(currentSpeed, 0, playerStats.currentMoveSpeed);
            rb2d.velocity = oldMovementInput * currentSpeed;
        }
        else
        {
            currentSpeed = 0;
            Destroy(rb2d);
            Destroy(collider2D);
        }

    }

    public float GetCurrentMoveSpeed()
    {
        return currentSpeed;
    }

}
