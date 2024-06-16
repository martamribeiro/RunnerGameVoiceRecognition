using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController player;
    public Transform playerTransform;
    private float moveDistance = 0.23f; // Fixed distance to move left or right
    private float smoothTime = 0.1f;    // Smooth time for lerp
    private float jumpHeight = 0.15f;   // Height of the jump

    private bool isJumping = false;
    private float verticalSpeed = 0f;
    private float targetZPosition = 0f;  // Target Z position for smooth movement

    private void Start()
    {
        player = GetComponent<CharacterController>();
        playerTransform = transform; // Using transform directly since it's attached to the same GameObject
    }

    void Update()
    {
        // Smoothly update the targetZPosition based on input
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetZPosition = Mathf.Clamp(targetZPosition - moveDistance, -moveDistance, moveDistance);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetZPosition = Mathf.Clamp(targetZPosition + moveDistance, -moveDistance, moveDistance);
        }

        // Smoothly move player towards the target position
        Vector3 currentPosition = playerTransform.localPosition;
        Vector3 newPosition = new Vector3(currentPosition.x, currentPosition.y, targetZPosition);
        playerTransform.localPosition = Vector3.Lerp(currentPosition, newPosition, smoothTime);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            verticalSpeed = CalculateJumpSpeed(jumpHeight);
        }

        // Apply gravity and move player vertically
        if (isJumping)
        {
            verticalSpeed += Physics.gravity.y * 0.5f * Time.deltaTime;
            Vector3 verticalMovement = new Vector3(0, verticalSpeed, 0) * Time.deltaTime;
            player.Move(verticalMovement);

            // Check if the player has landed
            if (player.isGrounded && verticalSpeed < 0)
            {
                isJumping = false;
                verticalSpeed = 0f;
            }
        }
    }

    float CalculateJumpSpeed(float jumpHeight)
    {
        // Calculate the initial upward speed required to achieve the desired jump height
        return Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);
    }
}
