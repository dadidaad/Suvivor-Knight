using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public PlayerAnimator playerAnimator;
    private PlayerMover playerMover;
    private PlayerStats playerStats;
    private WeaponController weaponController;

    PlayerManager playerManager;
    [SerializeField]
    private InputActionReference movement, attack, pointerPosition;

    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 lastMovedVector;

    private Vector2 pointerInput, movementInput;

    
    private void Update()
    {
        if (playerManager.isSetup)
        {
            pointerInput = GetPointerInput();
            movementInput = movement.action.ReadValue<Vector2>().normalized;
            if (movementInput.x != 0)
            {
                lastHorizontalVector = movementInput.x;
                lastMovedVector = new Vector2(lastHorizontalVector, 0f);
            }
            if (movementInput.y != 0)
            {
                lastVerticalVector = movementInput.y;
                lastMovedVector = new Vector2(0f, lastVerticalVector);  //Last moved Y
            }

            if (movementInput.x != 0 && movementInput.y != 0)
            {
                lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);    //While moving
            }
            playerMover.MovementInput = movementInput;
            weaponController.PointerPosition = pointerInput;
            AnimateCharacter();
        }
        
    }
    public Vector2 GetMoveDir()
    {
        return movementInput;
    }
    //private void OnEnable()
    //{
    //    attack.action.performed += PerformAttack;
    //}

    //private void OnDisable()
    //{
    //    attack.action.performed -= PerformAttack;
    //}

    public Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    public Vector2 GetMovementInput()
    {
        return movementInput;
    }
    //private void PerformAttack(InputAction.CallbackContext obj)
    //{
    //    weaponController.Attack();
    //}

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        weaponController = GetComponentInChildren<WeaponController>();
        playerMover = GetComponent<PlayerMover>();
        playerStats = GetComponent<PlayerStats>();    
        lastMovedVector = new Vector2(1.0f, 0f);
    }

    private void AnimateCharacter()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
        playerAnimator.RotateToPointer(lookDirection);
        playerAnimator.PlayAnimation(movementInput);
    }

}
