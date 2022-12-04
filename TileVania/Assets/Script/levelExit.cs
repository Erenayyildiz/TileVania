using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelExit : MonoBehaviour
{
    [SerializeField] float delay;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());
        }
     }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(delay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextsceneýndex = currentSceneIndex + 1;

        if(nextsceneýndex == SceneManager.sceneCountInBuildSettings)
        {
            nextsceneýndex = 0;
        }
        SceneManager.LoadScene(nextsceneýndex);
    }


}
