using UnityEngine;
using UnityEngine.SceneManagement; // Tarvitaan scenejen lataamiseen
using UnityEngine.UI; // Tarvitaan UI-komponenteille

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;
    public string gameSceneName = "GameScene"; 

    void Start()
    {
        playButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitGame);
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
