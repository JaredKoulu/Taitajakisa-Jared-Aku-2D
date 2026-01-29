using UnityEngine;

public class LavaKill : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Pelaaja osui lavaan, kuoleminen tapahtuu tässä
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                //player.Die();
            }
            else
            {
                // Jos sinulla ei ole Die-metodia pelaajassa, voit myös poistaa pelaajan:
                // Destroy(other.gameObject);
                Debug.Log("Player touched lava but no Die() method found!");
            }
        }
    }
}
