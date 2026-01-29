using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // Tarvitaan scenejen lataamiseen
using UnityEngine.UI; // Tarvitaan UI-komponenteille

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;
    public string gameSceneName = "GameScene";
    public TMP_Text bestTimeText;

    void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
        float best = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        bestTimeText.text = best == float.MaxValue
            ? "Best Time: --:--.--"
            : "Best Time: " + FormatTime(best);
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        float seconds = time % 60f;
        return string.Format("{0:00}:{1:00.00}", minutes, seconds);
    }


    void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
