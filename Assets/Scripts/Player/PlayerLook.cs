using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Camera playerCamera;

    [Header("Settings")]
    [Range(0.01f, 0.2f)]
    [SerializeField] private float mouseSensitivity = 0.1f;
    [Range(80f, 200f)]
    [SerializeField] private float controllerSensitivity = 100f;
    private float sensitivity;
    [Range(0f, 90f)]
    [SerializeField] private float maxVerticalLookAngle = 60f;
    [Range(0f, 360f)]
    [SerializeField] private float maxHorizontalLookAngle = 120f;

    [Header("Inputs")]
    private Vector2 lookInput;

    [Header("Rotation")]
    private float xRotation = 0f;
    private float yRotation = 0f;
    private float lookX;
    private float lookY;


    private void Awake() 
    {
        playerInput = GetComponentInParent<PlayerInput>();
    }

    private void Start()
    {
        SetSensitivity();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void SetSensitivity()
    {
        
        if (playerInput.currentControlScheme == "Gamepad")
        {
            sensitivity = controllerSensitivity;
        }
        else
        {
            sensitivity = mouseSensitivity;
        }

        Debug.Log("Sensitivity set to: " + sensitivity);
        
    }

    private void Update()
    {
        ApplyCameraRotation();
    }

    private void ApplyCameraRotation()
    {
        if (playerInput.currentControlScheme == "Gamepad")
        {
            lookX = lookInput.x * sensitivity * Time.deltaTime;
            lookY = lookInput.y * sensitivity * Time.deltaTime;
        }
        else
        {
            lookX = lookInput.x * sensitivity;
            lookY = lookInput.y * sensitivity;
        }

        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -maxVerticalLookAngle, maxVerticalLookAngle);

        yRotation += lookX;
        yRotation = Mathf.Clamp(yRotation, -maxHorizontalLookAngle, maxHorizontalLookAngle);
        
        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }


    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
}
