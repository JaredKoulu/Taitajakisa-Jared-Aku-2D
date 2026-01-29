using System.Collections;
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

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private bool isGrounded;

    // Spider web slow
    private bool isSlowed;
    private float slowMultiplier = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ReadInput();
        CheckGround();

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void ReadInput()
    {
        moveInput.x = 0;
        if (Keyboard.current.aKey.isPressed) moveInput.x = -1;
        if (Keyboard.current.dKey.isPressed) moveInput.x = 1;
    }

    void Move()
    {
        float speed = moveSpeed * slowMultiplier;
        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }

    // =========================
    // TRIGGER-KUTSUT
    // =========================

    // SpeedPad
    public void ApplySpeedBoost(float forwardForce, float upForce)
    {
        rb.AddForce(new Vector2(forwardForce, upForce), ForceMode2D.Impulse);
    }

    // SpiderWeb
    public void ApplySlow(float multiplier, float duration)
    {
        StartCoroutine(SlowRoutine(multiplier, duration));
    }

    IEnumerator SlowRoutine(float multiplier, float duration)
    {
        slowMultiplier = multiplier;
        yield return new WaitForSeconds(duration);
        slowMultiplier = 1f;
    }
}
