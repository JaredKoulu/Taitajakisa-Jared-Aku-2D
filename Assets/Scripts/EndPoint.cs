using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public TimeManager timeManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timeManager.StopTimer();
            Debug.Log("Aika p‰‰ttyi: " + timeManager.timerText.text);
        }
    }
}