using UnityEngine;

public class SpiderWeb : MonoBehaviour
{
    public float slowMultiplier = 0.5f;
    public float slowDuration = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ApplySlow(slowMultiplier, slowDuration);
            }
        }
    }
}