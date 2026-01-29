using UnityEngine;
using System.Collections;

public class SpeedPad : MonoBehaviour
{
    public float upForce = 10f;
    public float forwardForce = 5f;
    public float boostDuration = 0.3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

            if (player != null && rb != null)
            {
                player.speedBoost = true; // kerro Move()-funktiolle, ettei se ylikirjoita velocitya
                rb.AddForce(new Vector2(forwardForce, upForce), ForceMode2D.Impulse);

                // Poistetaan boost pienen ajan p‰‰st‰
                player.StartCoroutine(ResetSpeedBoost(player, boostDuration));
            }
        }
    }

    private IEnumerator ResetSpeedBoost(PlayerController player, float delay)
    {
        yield return new WaitForSeconds(delay);
        player.speedBoost = false;
    }
}
