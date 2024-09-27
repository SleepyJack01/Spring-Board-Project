using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    private CharacterController characterController;
    [SerializeField] private Transform headTransform;
    [SerializeField] private Transform cameraTransform;
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

    [Header("Headbob Variables")]
    [SerializeField] private bool enableHeadbob = true;
    private float headBobIntensity = 0.15f;
    private Vector2 headBobVector = Vector2.zero;
    private float headBobIndex = 0.0f;
    private float headBobCurrentIntensity = 0.0f;
    private Vector3 headInitialPosition;

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

    }

    private void LateUpdate() 
    {
        if (enableHeadbob)
        {
            HandleHeadbob();
        }
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

    private void HandleHeadbob()
    {
        bool isPlayerMoving = Mathf.Abs(movementInput.y) > 0;
        bool isPlayerRotating = Mathf.Abs(movementInput.x) > 0; 

        if (isGrounded)
        {
            if (isPlayerMoving)
            {
                headBobCurrentIntensity = headBobIntensity;
                headBobIndex += (characterController.velocity.magnitude * 2.5f) * Time.deltaTime;

                headBobVector.y = Mathf.Sin(headBobIndex);
                headBobVector.x = Mathf.Sin(headBobIndex / 2);

                cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, headInitialPosition + new Vector3(headBobVector.x * headBobCurrentIntensity, headBobVector.y * headBobCurrentIntensity, 0), Time.deltaTime * movementLerpSpeed);
            }
            else if (isPlayerRotating )
            {
                headBobCurrentIntensity = headBobIntensity;
                headBobIndex += Time.deltaTime * 11f;

                headBobVector.y = Mathf.Sin(headBobIndex);
                headBobVector.x = Mathf.Sin(headBobIndex / 2);

                cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, headInitialPosition + new Vector3(headBobVector.x * headBobCurrentIntensity, headBobVector.y * headBobCurrentIntensity, 0), Time.deltaTime * movementLerpSpeed);
            }
            else
            {
                cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, headInitialPosition, Time.deltaTime * movementLerpSpeed);

                if (Vector3.Distance(cameraTransform.localPosition, headInitialPosition) <= 0.001f)
                {
                    cameraTransform.localPosition = headInitialPosition + Vector3.zero;
                }
            }
        }  
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

    public Transform GetTransform()
    {
        return headTransform; 
    }
}
