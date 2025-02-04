using UnityEngine;

public class T_MenuHandler : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject creditsPanel;

    [Header("Levels")]
    [SerializeField] private string levelOne;
    [SerializeField] private string levelTwo;
    [SerializeField] private string levelThree;

    [Header("Audio")]
    private AudioSource _audioSource;
    [SerializeField] private AudioClip buttonSound;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
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
}