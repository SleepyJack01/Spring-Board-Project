using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugMovement : MonoBehaviour
{   
    CharacterController characterController;
    private float speed = 5f;
    private Vector2 movementInput;
    private Vector3 movement;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        movement = new Vector3(movementInput.x, 0.0f, movementInput.y);
        characterController.Move(movement * speed * Time.deltaTime);
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}
