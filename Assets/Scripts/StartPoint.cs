using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public TimeManager timeManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timeManager.StartTimer();
        }
    }
}