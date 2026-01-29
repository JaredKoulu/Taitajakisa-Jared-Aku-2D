using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject winPanel;
    public GameObject losePanel;

    public TMP_Text timeText;
    public TMP_Text bestTimeText;

    public TimeManager timeManager;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Win()
    {
        timeManager.StopTimer();
        SaveBestTime();
        ShowTimes();
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Lose()
    {
        timeManager.StopTimer();
        ShowTimes();
        losePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void SaveBestTime()
    {
        float best = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        if (timeManager.CurrentTime < best)
        {
            PlayerPrefs.SetFloat("BestTime", timeManager.CurrentTime);
        }
    }

    void ShowTimes()
    {
        timeText.text = "Time: " + timeManager.GetFormattedTime();

        float best = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        bestTimeText.text = best == float.MaxValue
            ? "Best: --:--.--"
            : "Best: " + FormatTime(best);
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        float seconds = time % 60f;
        return string.Format("{0:00}:{1:00.00}", minutes, seconds);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
