using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 1;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int score = 0;
    
    //creates a game manager object that tracks levels and lives
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
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
        //turns the player lives into text on screen
        livesText.text = playerLives.ToString();
        //displays the score on the screen
        scoreText.text = score.ToString();
    }

    //resets the game when player runs out of lives
    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            Invoke(nameof(TakeLife), 1);
        }
        else
        {
            Invoke(nameof(ResetGameSession),1);
        }
    }

    //add points to the score counter
    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    //reduces the amount of lives a player had and resets the level
    void TakeLife()
    {
       playerLives--;
       int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
       SceneManager.LoadScene(currentSceneIndex);

        //properly updates player lives text to the screen
       livesText.text = playerLives.ToString();
    }

    //resets the game and refreshes the game manager
    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
