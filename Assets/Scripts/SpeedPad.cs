using UnityEngine;

public class SpeedPad : MonoBehaviour
{
    [Header("Boost Settings")]
    public float forwardSpeed = 12f;   // eteenpäin
    public float upSpeed = 5f;         // ylöspäin
    public float boostDuration = 0.3f; // estää Move()-ylikirjoituksen

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerController player = collision.GetComponent<PlayerController>();
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

        if (player == null || rb == null) return;

        // direction riippuu hahmon scale.x:stä
        float dir = Mathf.Sign(player.transform.localScale.x);

        // Aseta täsmällinen velocity
        rb.linearVelocity = new Vector2(forwardSpeed * dir, upSpeed);

        // ilmoitetaan PlayerControllerille että boost on päällä
        player.StartSpeedBoost(boostDuration);
    }
}
