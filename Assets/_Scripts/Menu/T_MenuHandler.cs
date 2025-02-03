using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_MenuHandler : MonoBehaviour
{
    public string levelOne;
    public string levelTwo;
    public string levelThree;
    public GameObject creditsPanel;
    
    // Start the game
    public void StartLevelOne()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelOne);
    }
    public void StartLevelTwo()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelTwo);
    }
    public void StartLevelThree()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelThree);
    }
    
    // Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
    
    // Crédits
    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }
    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
    }
}
