using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 1;
    
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

    //reduces the amount of lives a player had and resets the level
    void TakeLife()
    {
       playerLives--;
       int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
       SceneManager.LoadScene(currentSceneIndex);
    }

    //resets the game and refreshes the game manager
    void ResetGameSession()
    {
       SceneManager.LoadScene(0);
       Destroy(gameObject);
    }
}
