using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Wall Jump")]
    public float wallJumpForceX = 8f;
    public float wallJumpForceY = 12f;
    public float wallCheckDistance = 0.4f;
    public LayerMask wallLayer;

    private bool isTouchingWall;
    private int wallDirection;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ReadInput();
        CheckGround();
        CheckWall();
        HandleJump();
    }

    void FixedUpdate()
    {
        Move();
    }

    void ReadInput()
    {
        moveInput = Vector2.zero;

        if (Keyboard.current.aKey.isPressed)
            moveInput.x = -1;

        if (Keyboard.current.dKey.isPressed)
            moveInput.x = 1;
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    void HandleJump()
    {
        if (!Keyboard.current.spaceKey.wasPressedThisFrame)
            return;

        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        else if (isTouchingWall)
        {
            rb.linearVelocity = new Vector2(
                -wallDirection * wallJumpForceX,
                wallJumpForceY
            );
        }
    }


    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }
    void CheckWall()
    {
        RaycastHit2D leftWall = Physics2D.Raycast(
            transform.position,
            Vector2.left,
            wallCheckDistance,
            wallLayer
        );

        RaycastHit2D rightWall = Physics2D.Raycast(
            transform.position,
            Vector2.right,
            wallCheckDistance,
            wallLayer
        );

        if (leftWall)
        {
            isTouchingWall = true;
            wallDirection = -1;
        }
        else if (rightWall)
        {
            isTouchingWall = true;
            wallDirection = 1;
        }
        else
        {
            isTouchingWall = false;
        }
    }
}
