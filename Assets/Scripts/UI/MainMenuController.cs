using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class MainMenuController : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject optionsPanel;

    [Header("Botones")]
    [SerializeField] private Button playButton;
    [SerializeField] private string playName;
    [SerializeField] private Button creditsButton;
    [SerializeField] private string creditsName;
    [SerializeField] private Button OptionsButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button quitButton;

    [Header("Fuente")]
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource fxSource;

    [Header("Sonido")]
    [SerializeField] private AudioClip clickSound;

    [Header("Volumen")]
    [SerializeField] private Slider volumenMaster;
    [SerializeField] private Slider volumenFX;

    private void Awake()
    {
        volumenMaster.onValueChanged.AddListener(ChangeVolumenMaster);
        volumenFX.onValueChanged.AddListener(ChangeVolumenFX);

        menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
        playButton.onClick.AddListener(PlayGame);
        OptionsButton.onClick.AddListener(Options);
        backButton.onClick.AddListener(BackButton);
        creditsButton.onClick.AddListener(Credits);
        quitButton.onClick.AddListener(QuitGame);
    }
    private void PlayGame()
    {
        PlaySoundButton();
        SceneManager.LoadScene(playName);
    }

    private void Options()
    {
        PlaySoundButton();
        menuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    private void BackButton()
    {
        PlaySoundButton();
        menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    private void Credits()
    {
        PlaySoundButton();
        SceneManager.LoadScene(creditsName);
    }

    private void QuitGame()
    {
        PlaySoundButton();
        Application.Quit();
    }

    private void ChangeVolumenMaster(float v)
    {
        mixer.SetFloat("VolMaster", v);
    }

    private void ChangeVolumenFX(float v)
    {
        mixer.SetFloat("VolFX", v);
    }

    private void PlaySoundButton()
    {
        fxSource.PlayOneShot(clickSound);
    }
}