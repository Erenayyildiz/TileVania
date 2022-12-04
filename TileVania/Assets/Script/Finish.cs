using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] float delay;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           FindObjectOfType<Gamesession>().ScoreReset();
           FindObjectOfType<Gamesession>().ResetGame();
           StartCoroutine(LoadNextLevel());
        }
     }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(delay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextscene�ndex = currentSceneIndex + 1;

        if (nextscene�ndex == SceneManager.sceneCountInBuildSettings)
        {
            nextscene�ndex = 0;
        }
        SceneManager.LoadScene(nextscene�ndex);
    }



}
