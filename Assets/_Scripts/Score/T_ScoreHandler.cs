using UnityEngine;
using UnityEngine.UI;

public class T_ScoreHandler : MonoBehaviour
{
    private int _score;
    public Image[] scoreImages; // Add references to the score images
    private int _scoreObjects = 3; // Initialize the number of score objects
    public GameObject endCanvas; // Reference to the victory canvas
    public Image[] starImages; // Add references to the star images
    public string levelIdentifier; // Add a level identifier

    public void IncrementScore()
    {
        _score++;
        Debug.Log("Score: " + _score);
        LoseScoreObject(); // Decrease the number of score objects
        CheckWinCondition(); // Check if the player has won
    }

    private void LoseScoreObject()
    {
        if (_scoreObjects > 0)
        {
            _scoreObjects--;
            if (scoreImages[_scoreObjects] != null)
            {
                scoreImages[_scoreObjects].enabled = false; // Disable the score image
            }
        }
    }

    private void CheckWinCondition()
    {
        if (_scoreObjects == 0)
        {
            ShowVictoryCanvas();
        }
    }

    public void ShowVictoryCanvas()
    {
        if (endCanvas != null)
        {
            endCanvas.SetActive(true); // Show the victory canvas
            ActivateStars(); // Activate stars based on the score
        }
    }

    private void ActivateStars()
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            if (i < _score)
            {
                starImages[i].enabled = true; // Activate the star image
            }
            else
            {
                starImages[i].enabled = false; // Deactivate the star image
            }
        }
        string scoreKey = levelIdentifier + "Score";
        int cappedScore = Mathf.Min(_score, 3); // Cap the score at 3
        if (cappedScore > PlayerPrefs.GetInt(scoreKey))
        {
            PlayerPrefs.SetInt(scoreKey, cappedScore);
        }
    }
}