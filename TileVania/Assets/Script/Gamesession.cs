using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Gamesession : MonoBehaviour
{
    [SerializeField] int playerLive;
    [SerializeField] int Score;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<Gamesession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = playerLive.ToString();
        scoreText.text = Score.ToString();
    }

    public void PlayerDeath()
    {
        if(playerLive > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGame();
        }
    }

    public void AddToScore(int pointsToAdd)
    {
        Score += pointsToAdd;
        scoreText.text = Score.ToString();
    }

    void TakeLife()
    {
        playerLive--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLive.ToString();
    }

    public void ResetGame()
    {  
        playerLive = 3;
        SceneManager.LoadScene(0);
        livesText.text = playerLive.ToString();
    }

    public void ScoreReset()
    {
        Score = 0;
        scoreText.text = Score.ToString();
    }
}
