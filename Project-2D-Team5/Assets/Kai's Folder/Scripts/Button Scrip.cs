using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScrip : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject helpPanel;
    public GameObject pausePanel;

    [Header("Main Menu Buttons")]
    public Button playButton;
    public Button helpButton;
    public Button creditsButton;
    public Button quitButton;

    [Header("Pause Menu Buttons")]
    public Button resumeButton;
    public Button mainMenuButton;

    [Header("Help Menu Buttons")]
    public Button backButton;

    private bool isPaused = false;

    void Start()
    {
        if (playButton != null)
            playButton.onClick.AddListener(PlayGame);

        if (helpButton != null)
            helpButton.onClick.AddListener(OpenHelpPanel);

        if (creditsButton != null)
            creditsButton.onClick.AddListener(OpenCredits);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);

        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);

        if (backButton != null)
            backButton.onClick.AddListener(CloseHelpPanel);

        if (helpPanel != null)
            helpPanel.SetActive(false);
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level One");
    }

    public void OpenHelpPanel()
    {
        if (helpPanel != null)
            helpPanel.SetActive(true);
    }

    public void CloseHelpPanel()
    {
        if (helpPanel != null)
            helpPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            if (pausePanel != null)
                pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            if (pausePanel != null)
                pausePanel.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}