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
    public float wallJumpCooldown = 0.2f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 moveInput;
    private bool isGrounded;


    private bool isTouchingWall;
    private int wallDirection;
    private int lastWallDirection;
    private float lastWallTouchTime;

    private bool isWallJumping;
    private float wallJumpTimer;
    public float wallJumpDuration = 0.25f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        ReadInput();
        CheckGround();
        CheckWall();
        HandleJump();
        UpdateWallJumpTimer();
        UpdateAnimations();
    }

    void FixedUpdate()
    {
        Move();
    }
    void ReadInput()
    {
        moveInput = Vector2.zero;
        if (Keyboard.current.aKey.isPressed) moveInput.x = -1;
        if (Keyboard.current.dKey.isPressed) moveInput.x = 1;
    }

    void Move()
    {
        if (isWallJumping) return; 
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void CheckWall()
    {
        if (isGrounded)
        {
            isTouchingWall = false;
            return;
        }

        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, wallCheckDistance, wallLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance, wallLayer);

        if (hitLeft)
        {
            isTouchingWall = true;
            wallDirection = -1;
            lastWallDirection = -1;
            lastWallTouchTime = Time.time;
        }
        else if (hitRight)
        {
            isTouchingWall = true;
            wallDirection = 1;
            lastWallDirection = 1;
            lastWallTouchTime = Time.time;
        }
        else
        {
            isTouchingWall = false;
        }

        // Debug
        Debug.DrawRay(transform.position, Vector2.left * wallCheckDistance, Color.red);
        Debug.DrawRay(transform.position, Vector2.right * wallCheckDistance, Color.blue);
    }

    void HandleJump()
    {
        if (!Keyboard.current.spaceKey.wasPressedThisFrame) return;

        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        else if (isTouchingWall && Time.time - lastWallTouchTime <= 0.15f) 
        {
            PerformWallJump();
        }
    }

    void PerformWallJump()
    {
        isWallJumping = true;
        wallJumpTimer = wallJumpDuration;

        rb.linearVelocity = Vector2.zero;

        Vector2 force = new Vector2(
            -lastWallDirection * wallJumpForceX,
            wallJumpForceY
        );

        rb.AddForce(force, ForceMode2D.Impulse);
    }

    void UpdateWallJumpTimer()
    {
        if (!isWallJumping) return;

        wallJumpTimer -= Time.deltaTime;
        if (wallJumpTimer <= 0)
            isWallJumping = false;
    }


    void UpdateAnimations()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsWallJumping", isWallJumping);

        bool isTouchingWallAnim = isTouchingWall && !isGrounded;
        animator.SetBool("IsTouchingWall", isTouchingWallAnim);

        if (moveInput.x != 0)
        {
            spriteRenderer.flipX = moveInput.x < 0;
        }
        else if (isTouchingWallAnim)
        {

            spriteRenderer.flipX = wallDirection > 0; 
        }
    }
}
