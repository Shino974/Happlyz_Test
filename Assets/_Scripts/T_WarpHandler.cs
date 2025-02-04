using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_WarpHandler : MonoBehaviour
{
    public string nextLevel;
    public string menu;
    
    public void LoadNextLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextLevel);
    }
    
    public void LoadMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(menu);
    }
}
