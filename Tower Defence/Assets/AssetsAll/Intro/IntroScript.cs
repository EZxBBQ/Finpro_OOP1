using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    [SerializeField] private Animator introAnimator;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntroScene());
    }

    private IEnumerator IntroScene()
    {
        introAnimator.SetTrigger("introTrigger");
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("MainMenu");
    }
}
