using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveNormal : MonoBehaviour
{
    public CharacterController player;
    public Transform playerTransform;
    private float moveDistance = 0.23f; // Fixed distance to move left or right
    private float smoothTime = 0.1f;    // Smooth time for lerp
    private float jumpHeight = 0.15f;   // Height of the jump

    private bool isJumping = false;
    private float verticalSpeed = 0f;
    private float targetZPosition = 0f;  // Target Z position for smooth movement
    private float initialYPosition;      // Initial y position of the player

    public Animator m_Animator;

    private void Start()
    {
        player = GetComponent<CharacterController>();
        playerTransform = transform;
        initialYPosition = playerTransform.localPosition.y;
    }

    private void Update()
    {
        HandleInput();

        // Smooth side movement
        Vector3 currentPosition = playerTransform.localPosition;
        Vector3 newPosition = new Vector3(currentPosition.x, currentPosition.y, targetZPosition);
        playerTransform.localPosition = Vector3.Lerp(currentPosition, newPosition, smoothTime);

        // Jump movement
        if (isJumping)
        {
            verticalSpeed += Physics.gravity.y * 0.5f * Time.deltaTime;
            Vector3 verticalMovement = new Vector3(0, verticalSpeed, 0) * Time.deltaTime;
            player.Move(verticalMovement);

            if (player.isGrounded && verticalSpeed < 0)
            {
                isJumping = false;
                verticalSpeed = 0f;
                Vector3 groundedPosition = playerTransform.localPosition;
                groundedPosition.y = initialYPosition;
                playerTransform.localPosition = groundedPosition;
                m_Animator.ResetTrigger("Jump");
            }
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerJump();
        }
    }

    private void MoveLeft()
    {
        targetZPosition = Mathf.Clamp(targetZPosition - moveDistance, -moveDistance, moveDistance);
    }

    private void MoveRight()
    {
        targetZPosition = Mathf.Clamp(targetZPosition + moveDistance, -moveDistance, moveDistance);
    }

    private void TriggerJump()
    {
        if (!isJumping)
        {
            isJumping = true;
            verticalSpeed = CalculateJumpSpeed(jumpHeight);
            m_Animator.SetTrigger("Jump");
        }
    }

    private float CalculateJumpSpeed(float jumpHeight)
    {
        return Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);
    }
}
