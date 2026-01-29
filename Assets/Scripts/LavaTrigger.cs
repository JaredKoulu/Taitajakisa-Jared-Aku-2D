using UnityEngine;

public class LavaTrigger : MonoBehaviour
{
    public RisingLava lava;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            lava.StartRising();
            gameObject.SetActive(false);
        }
    }
}
