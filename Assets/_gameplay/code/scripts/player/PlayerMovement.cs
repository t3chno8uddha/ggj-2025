using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float mouseSensitivity = 2.0f;

    CharacterController characterController;
    Vector3 moveDirection;
    float rotationX = 0;

    [SerializeField] int statIndex = 0; 
    [SerializeField] List<PlayerStats> stats = new List<PlayerStats>();

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Handle player movement
        HandleMovementInput();

        // Handle player rotation
        HandleMouseLook();

        // Move the character
        moveDirection.y += stats[statIndex].gravity;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void HandleMovementInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * horizontal;
        Vector3 moveVertical = transform.forward * vertical;

        // Final movement direction
        Vector3 desiredMoveDirection = (moveHorizontal + moveVertical).normalized;

        // Apply speed multiplier when running and there is stamina
        float currentSpeed = stats[statIndex].movementSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) { currentSpeed *= stats[statIndex].sprintMultiplier; }

        // Makes the player jump
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            moveDirection.y += Mathf.Sqrt(stats[statIndex].playerJumpHeight * -2.0f * stats[statIndex].gravity);
        }

        moveDirection = desiredMoveDirection * currentSpeed;
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the character horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
}

[Serializable]
public class PlayerStats
{
    public int _health;
    public float movementSpeed;
    public float sprintMultiplier = 2.0f; // Speed multiplier when running
    public float playerJumpHeight;
    public float gravity = 9.8f;
}