using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float jumpForce = 12f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public float wallJumpForceX = 8f;
    public float wallJumpForceY = 12f;
    public float wallCheckDistance = 0.4f;
    public LayerMask wallLayer;
    public float wallJumpDuration = 0.25f;
    public bool speedBoost = false;
    private float boostTimer = 0f;
    private float speedMultiplier = 1f;
    private float originalSpeedMultiplier = 1f;
    private Coroutine slowRoutine;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private int lastWallDirection;
    private bool isWallJumping;
    private float wallJumpTimer;

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
        UpdateBoostTimer();
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
        if (isWallJumping || speedBoost) return;
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed * speedMultiplier, rb.linearVelocity.y);
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    void CheckWall()
    {
        if (isGrounded)
        {
            isTouchingWall = false;
            return;
        }
        if (Physics2D.Raycast(transform.position, Vector2.left, wallCheckDistance, wallLayer))
        {
            isTouchingWall = true;
            lastWallDirection = -1;
        }
        else if (Physics2D.Raycast(transform.position, Vector2.right, wallCheckDistance, wallLayer))
        {
            isTouchingWall = true;
            lastWallDirection = 1;
        }
        else
        {
            isTouchingWall = false;
        }
    }

    void HandleJump()
    {
        if (!Keyboard.current.spaceKey.wasPressedThisFrame) return;
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        else if (isTouchingWall)
        {
            PerformWallJump();
        }
    }

    void PerformWallJump()
    {
        isWallJumping = true;
        wallJumpTimer = wallJumpDuration;
        rb.linearVelocity = Vector2.zero;
        Vector2 force = new Vector2(-lastWallDirection * wallJumpForceX, wallJumpForceY);
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    void UpdateWallJumpTimer()
    {
        if (!isWallJumping) return;
        wallJumpTimer -= Time.deltaTime;
        if (wallJumpTimer <= 0) isWallJumping = false;
    }

    public void StartSpeedBoost(float duration)
    {
        speedBoost = true;
        boostTimer = duration;
    }

    void UpdateBoostTimer()
    {
        if (!speedBoost) return;
        boostTimer -= Time.deltaTime;
        if (boostTimer <= 0) speedBoost = false;
    }

    public void ApplySlow(float multiplier, float duration)
    {
        if (slowRoutine != null)
            StopCoroutine(slowRoutine);
        slowRoutine = StartCoroutine(SlowRoutine(multiplier, duration));
    }

    private IEnumerator SlowRoutine(float multiplier, float duration)
    {
        originalSpeedMultiplier = speedMultiplier;
        speedMultiplier = multiplier;
        yield return new WaitForSeconds(duration);
        speedMultiplier = originalSpeedMultiplier;
        slowRoutine = null;
    }

    void UpdateAnimations()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsWallJumping", isWallJumping);
        animator.SetBool("IsTouchingWall", isTouchingWall && !isGrounded);
        if (moveInput.x != 0) spriteRenderer.flipX = moveInput.x < 0;
    }
}
