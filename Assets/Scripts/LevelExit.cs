using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;

    //loads the next level
  void OnTriggerEnter2D(Collider2D other)
  {
      //allows only the player to trigger the exit
      if (other.tag == "Player")
      {
          StartCoroutine(LoadNextLevel());
      }
  }

    //adds delay before loading the next level
  IEnumerator LoadNextLevel()
  {
    yield return new WaitForSecondsRealtime(levelLoadDelay);

    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex + 1;

    //loads level 1 when player reaches the final level of the game
    if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
    {
        nextSceneIndex = 0;
    }
    
    FindObjectOfType<ScenePersist>().ResetScenePersist();
    SceneManager.LoadScene(nextSceneIndex);
  }

}
