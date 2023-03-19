using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Rigidbody2D rb2d;

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
    }

    private void FixedUpdate()
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


}
