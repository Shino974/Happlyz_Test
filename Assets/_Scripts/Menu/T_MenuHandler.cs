using System;
using UnityEngine;

public class T_MenuHandler : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject settingsPanel;

    [Header("Levels")]
    [SerializeField] private string levelOne;
    [SerializeField] private string levelTwo;
    [SerializeField] private string levelThree;

    [Header("Audio")]
    [SerializeField] private AudioClip buttonSound;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        ApplyAudioSettings();
    }

    private void Update()
    {
        ApplyAudioSettings();
    }

    // Apply audio settings
    private void ApplyAudioSettings()
    {
        bool isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        _audioSource.mute = isMuted;
        if (isMuted)
            _audioSource.volume = 0;
        else
            _audioSource.volume = 1;
    }

    // Start the game
    public void StartLevelOne()
    {
        _audioSource.PlayOneShot(buttonSound);
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelOne);
    }
    public void StartLevelTwo()
    {
        _audioSource.PlayOneShot(buttonSound);
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelTwo);
    }
    public void StartLevelThree()
    {
        _audioSource.PlayOneShot(buttonSound);
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelThree);
    }

    // Quit the game
    public void QuitGame()
    {
        _audioSource.PlayOneShot(buttonSound);
        Application.Quit();
    }

    // Crédits
    public void ShowCredits()
    {
        _audioSource.PlayOneShot(buttonSound);
        creditsPanel.SetActive(true);
    }
    public void CloseCredits()
    {
        _audioSource.PlayOneShot(buttonSound);
        creditsPanel.SetActive(false);
    }

    // Settings
    public void ShowSettings()
    {
        _audioSource.PlayOneShot(buttonSound);
        settingsPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        _audioSource.PlayOneShot(buttonSound);
        settingsPanel.SetActive(false);
    }
}