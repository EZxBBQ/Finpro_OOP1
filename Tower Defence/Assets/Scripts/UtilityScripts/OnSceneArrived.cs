using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSceneArrived : MonoBehaviour
{
    [SerializeField] private GameObject transitionImage;
    [SerializeField] private Animator animator;

    private void Start()
    {
        StartCoroutine(TransitionAnimation());
    }

    private IEnumerator TransitionAnimation()
    {
        animator.SetTrigger("isEnded");
        yield return new WaitForSeconds(1.5f);
        transitionImage.SetActive(false);
    }
}
