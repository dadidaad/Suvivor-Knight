using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    private PlayerAnimator playerAnimator;
    private PlayerMover playerMover;

    private WeaponController weaponController;

    [SerializeField]
    private InputActionReference movement, attack, pointerPosition;

    private Vector2 pointerInput, movementInput;

    private void Update()
    {
        pointerInput = GetPointerInput();
        movementInput = movement.action.ReadValue<Vector2>().normalized;

        playerMover.MovementInput = movementInput;
        weaponController.PointerPosition = pointerInput;
        AnimateCharacter();
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

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    //private void PerformAttack(InputAction.CallbackContext obj)
    //{
    //    weaponController.Attack();
    //}

    private void Awake()
    {
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
        weaponController = GetComponentInChildren<WeaponController>();
        playerMover = GetComponent<PlayerMover>();
    }

    private void AnimateCharacter()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
        playerAnimator.RotateToPointer(lookDirection);
        playerAnimator.PlayAnimation(movementInput);
    }

}
