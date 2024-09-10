using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    private CharacterController characterController;
    // private Animator animator;

    [Header("Input Variables")]
    private Vector2 movementInput;
    private Vector3 currentMovement;
    private float verticalVelocity;
    private Vector3 verticalMovement;

    [Header("Movement Variables")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float movementLerpSpeed = 6f;
    [SerializeField] float rotationFactorPerFrame = 100f;

    [Header("Gravity Variables")]
    private float gravity = 9.81f;

    [Header("Ground Check Variables")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance = 1.1f;
    private bool isGrounded
    {
        get
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            return Physics.Raycast(ray, groundDistance, groundMask);
        }
    }
    
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>();
    
    }

    void Update()
    {
        ApplyGravity();
        HandleMovement();
        HandleRotation();

        Debug.Log(isGrounded);

    }

    private void ApplyGravity()
    {
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += -gravity * Time.deltaTime;
        }

        verticalMovement = new Vector3(0, verticalVelocity, 0);
        characterController.Move(verticalMovement * Time.deltaTime);
    }

    private void HandleMovement()
    {
        if (isGrounded)
        {
            currentMovement = Vector3.Lerp(currentMovement, (transform.forward * movementInput.y).normalized, movementLerpSpeed * Time.deltaTime);
        }
        
        if (movementInput.magnitude > 0.1f)
        {
            characterController.Move(currentMovement * movementSpeed * Time.deltaTime);
        }
        else
        {
            characterController.Move(Vector3.MoveTowards(currentMovement, Vector3.zero, Time.deltaTime * movementLerpSpeed) * movementSpeed * Time.deltaTime);
        }
    }

    private void HandleRotation()
    {
        transform.Rotate(Vector3.up * movementInput.x * rotationFactorPerFrame * Time.deltaTime);
    }


    public void onMovementInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}
