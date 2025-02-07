using System;
using UnityEngine;
using UnityEngine.UI;

public class T_ScoreHandler : MonoBehaviour
{
    public Image[] scoreImages; // Add references to the score images
    public Image[] starImages; // Add references to the star images
    public GameObject endCanvas; // Reference to the victory canvas
    
    // Score
    private int _score;
    private int _scoreObjects = 3;
    
    // Level identifier
    public string levelIdentifier;
    
    // Audio
    public AudioClip victorySound;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        ApplyAudioSettings(); // Apply audio settings
    }

    private void Update()
    {
        ApplyAudioSettings(); // Apply audio settings
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

    // Increment the score
    public void IncrementScore()
    {
        _score++;
        Debug.Log("Score: " + _score);
        LoseScoreObject(); // Decrease the number of score objects
        CheckWinCondition(); // Check if the player has won
    }

    // Decrease the number of score objects
    private void LoseScoreObject()
    {
        if (_scoreObjects > 0)
        {
            _scoreObjects--;
            if (scoreImages[_scoreObjects] != null)
                scoreImages[_scoreObjects].enabled = false;
        }
    }

    // Check if the player has won
    private void CheckWinCondition()
    {
        if (_scoreObjects == 0)
            ShowVictoryCanvas(); // Show the victory canvas
    }

    // Show the victory canvas
    public void ShowVictoryCanvas()
    {
        if (endCanvas != null)
        {
            endCanvas.SetActive(true); // Show the victory canvas
            ActivateStars(); // Activate stars based on the score

            if (victorySound != null)
                _audioSource.PlayOneShot(victorySound);
        }
    }

    // Activate stars based on the score
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
        
        // Save the score
        string scoreKey = levelIdentifier + "Score";
        int cappedScore = Mathf.Min(_score, 3); // Cap the score at 3
        if (cappedScore > PlayerPrefs.GetInt(scoreKey))
            PlayerPrefs.SetInt(scoreKey, cappedScore);
    }
}