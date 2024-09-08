using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugMovement : MonoBehaviour
{   
    CharacterController characterController;
    private Vector2 movementInput;
    private Vector3 movement;
    private float movementSpeed = 5f;
    private float movementLerpTime = 6f;
    private Vector3 verticalMove;
    private float verticalVelocity;
    private float gravity = -9.81f;

    [Header("Ground Check Settings")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 1.1f;
    private bool isGrounded
    {
        get
        {
            Ray ray = new Ray(transform.position, -transform.up);
            return Physics.SphereCast(ray, characterController.radius, groundDistance, groundMask);
        }
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        ApplyGravity();
        MovementHandler();
    }

    private void ApplyGravity()
    {
        if (!isGrounded)
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        verticalMove = new Vector3(0, verticalVelocity, 0);
        characterController.Move(verticalMove * Time.deltaTime);
    }

    private void MovementHandler()
    {
        if (isGrounded)
        {
            movement = Vector3.Lerp(movement,(transform.forward * movementInput.y + transform.right * movementInput.x), Time.deltaTime * movementLerpTime);
        }

        if (movement.magnitude >= 0.1f)
        {
            characterController.Move(movement * movementSpeed * Time.deltaTime);
        }
        else
        {
            characterController.Move(Vector3.MoveTowards(movement, Vector3.zero, Time.deltaTime * movementLerpTime) * movementSpeed * Time.deltaTime);
        }
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}
