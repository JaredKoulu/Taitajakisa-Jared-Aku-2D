using UnityEngine;
using TMPro; // <-- tärkeää!

public class TimeManager : MonoBehaviour
{
    public TMP_Text timerText; // TMP_Text komponentti inspectorissa
    private float timer = 0f;
    private bool running = false;

    void Update()
    {
        if (running)
        {
            timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60f);
            float seconds = timer % 60f;
            timerText.text = string.Format("{0:00}:{1:00.00}", minutes, seconds);
        }
    }

    public void StartTimer()
    {
        timer = 0f;
        running = true;
    }

    public void StopTimer()
    {
        running = false;
    }
}