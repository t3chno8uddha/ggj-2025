using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float sprintMultiplier = 2.0f; // Speed multiplier when running
    public float sensitivity = 2.0f;
    public float gravity = 9.8f;
    public float staminaMax = 4.0f; // Maximum stamina in seconds
    public float staminaRecoveryRate = 1.0f; // Stamina recovery rate in seconds

    private CharacterController characterController;
    private Vector3 moveDirection;
    private float rotationX = 0;
    private float currentStamina;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentStamina = staminaMax; // Start with full stamina
    }

    void Update()
    {
        // Handle player movement
        HandleMovementInput();

        // Handle player rotation
        HandleMouseLook();

        // Apply gravity
        ApplyGravity();

        // Move the character
        characterController.Move(moveDirection * Time.deltaTime);

        // Update stamina
        UpdateStamina();
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
        //float currentSpeed = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 ? speed * sprintMultiplier : speed;

        // Set the final movement direction with speed
        moveDirection = desiredMoveDirection * speed;
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotate the character horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }

    void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
    }

    void UpdateStamina()
    {
        // Replenish stamina when not running
        if (!Input.GetKey(KeyCode.LeftShift) && currentStamina < staminaMax)
        {
            currentStamina += Time.deltaTime * staminaRecoveryRate;
            currentStamina = Mathf.Clamp(currentStamina, 0, staminaMax);
        }
    }
}
