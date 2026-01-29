using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TMP_Text timerText; 
    private float timer = 0f;
    private bool running = false;
    public float CurrentTime => timer;

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
        MusicManager.Instance.PlayActionMusic();
    }

    public void StopTimer()
    {
        running = false;
    }
    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        float seconds = timer % 60f;
        return string.Format("{0:00}:{1:00.00}", minutes, seconds);
    }

}