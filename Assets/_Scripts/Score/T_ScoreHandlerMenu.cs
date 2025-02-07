using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_ScoreHandlerMenu : MonoBehaviour
{
    [Header("LevelOne")]
    [SerializeField] private GameObject[] noStarsLevelOne;
    [SerializeField] private GameObject[] starsLevelOne;

    [Header("LevelTwo")]
    [SerializeField] private GameObject[] noStarsLevelTwo;
    [SerializeField] private GameObject[] starsLevelTwo;

    [Header("LevelThree")]
    [SerializeField] private GameObject[] noStarsLevelThree;
    [SerializeField] private GameObject[] starsLevelThree;

    private void Start()
    {
        InitializeStars(noStarsLevelOne, starsLevelOne);
        InitializeStars(noStarsLevelTwo, starsLevelTwo);
        InitializeStars(noStarsLevelThree, starsLevelThree);

        SetActiveStars(PlayerPrefs.GetInt("LevelOneScore"), noStarsLevelOne, starsLevelOne);
        SetActiveStars(PlayerPrefs.GetInt("LevelTwoScore"), noStarsLevelTwo, starsLevelTwo);
        SetActiveStars(PlayerPrefs.GetInt("LevelThreeScore"), noStarsLevelThree, starsLevelThree);
    }

    // Initialize the stars
    private void InitializeStars(GameObject[] noStars, GameObject[] stars)
    {
        foreach (var star in stars)
            star.SetActive(false);
    }

    // Set the active stars
    private void SetActiveStars(int score, GameObject[] noStars, GameObject[] stars)
    {
        for (int i = 0; i < noStars.Length; i++)
            noStars[i].SetActive(i >= score);

        for (int i = 0; i < score; i++)
            stars[i].SetActive(true);
    }
}