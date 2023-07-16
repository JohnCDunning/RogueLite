using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, GameInput.IMechInputActions
{
    public InputManager inputManager;
    public CharacterController characterController;

    public float moveSpeed = 2;

    private Vector2 movement;
    private void Start()
    {
        inputManager.ConfigurePlayerBindings(this);
    }

    private void FixedUpdate()
    {
        characterController.SimpleMove(new Vector3(movement.x, 0, movement.y) * Time.deltaTime * moveSpeed);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>().normalized;
    }
}
