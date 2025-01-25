using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    CharacterController characterController;

    [Header("Camera and Mouse")]
    public float mouseSensitivity = 2.0f;
    float rotationX = 0;

    [Header("Movement")]
    [SerializeField] Vector3 moveDirection;

    [Header("Stats")]
    [SerializeField] List<PlayerStats> stats = new List<PlayerStats>();
    [SerializeField] int statIndex = 0;

    float verticalVelocity;
    bool isJumping;

    void Start()
    {
        if (!characterController) { characterController = GetComponent<CharacterController>(); }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Handle player rotation
        HandleMouseLook();

        // Move the character
        var moveDir = MoveDirection();
        verticalVelocity -= stats[statIndex].gravity * Time.deltaTime;

        // Handle gravity
        if (characterController.isGrounded)
        {
            if (isJumping) { isJumping = false; }
            if ( Input.GetKeyDown(KeyCode.Space)) { Jump(); }
        }
        else if (!isJumping)
        {
            verticalVelocity = 0;
            isJumping = true;
        }

        moveDir.y = verticalVelocity;

        // Move the character controller
        characterController.Move(moveDir * Time.deltaTime);
        
    }

    Vector3 MoveDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * horizontal;
        Vector3 moveVertical = transform.forward * vertical;

        // Final movement direction
        Vector3 desiredMoveDirection = (moveHorizontal + moveVertical).normalized;

        // Apply speed multiplier when running
        float currentSpeed = stats[statIndex].movementSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) { currentSpeed *= stats[statIndex].sprintMultiplier; }

        moveDirection.x = desiredMoveDirection.x * currentSpeed;
        moveDirection.z = desiredMoveDirection.z * currentSpeed;

        return moveDirection;
    }

    void Jump()
    {
        isJumping = true;
        verticalVelocity = Mathf.Sqrt(stats[statIndex].playerJumpHeight * 2f * stats[statIndex].gravity);
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
    public int _health = 3;
    public float movementSpeed = 8f;
    public float sprintMultiplier = 1.5f; // Speed multiplier when running
    public float playerJumpHeight = 3f; // Height the player can jump
    public float gravity = 50f; // Gravity affecting the player
}