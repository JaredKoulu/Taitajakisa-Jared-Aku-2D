using UnityEngine;

public class SpeedPad : MonoBehaviour
{
    public float upForce;       // ylöspäin
    public float forwardForce;  // eteenpäin

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                // Korvaa vanhan speedBoost-logiikan
                player.ApplySpeedBoost(forwardForce, upForce);
            }

            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Säilytetään vaakaliike, nollataan vain pystysuora
                float currentX = rb.linearVelocity.x;
                rb.linearVelocity = new Vector2(currentX, 0f);

                // Lisätään voima ylöspäin ja eteenpäin
                rb.AddForce(new Vector2(forwardForce, upForce), ForceMode2D.Impulse);
            }
        }
    }
}