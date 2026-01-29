using UnityEngine;

public class SpeedPad : MonoBehaviour
{
    public float upForce = 10f;
    public float forwardForce = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ApplySpeedBoost(forwardForce, upForce);
            }
        }
    }
}
