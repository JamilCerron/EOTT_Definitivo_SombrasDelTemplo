using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class Codigo_Pausa : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private bool isPaused = false;
    [SerializeField] private AudioSource fxSource;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private string SceneName;

    private void Awake()
    {
        pausePanel.SetActive(false);
        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(LoadMainMenu);
        quitButton.onClick.AddListener(QuitGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        PlaySoundButton();
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        isPaused = false;
    }

    public void PauseGame()
    {
        PlaySoundButton();
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadMainMenu()
    {
        PlaySoundButton();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneName);
    }

    public void QuitGame()
    {
        PlaySoundButton();
        Application.Quit();
    }

    private void PlaySoundButton()
    {
        fxSource.PlayOneShot(clickSound);
    }
}
