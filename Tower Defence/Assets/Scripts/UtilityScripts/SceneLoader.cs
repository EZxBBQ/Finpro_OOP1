using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// SceneLoader used to handle scene loading and quit game
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject transitionImage;  // the animator of canvas (parent)
    [SerializeField] private Animator animator;  // the animator of canvas (parent)

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;    // make sure the game is unpaused when loading new scene
        transitionImage.SetActive(true);    
        StartCoroutine(TransitionAnimation(sceneName));
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }

    private IEnumerator TransitionAnimation(string sceneName)
    {
        animator.SetTrigger("isStarted");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}
