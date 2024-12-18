using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    private GameObject pauseMenu;

    // Start is called before the first frame update
    private void Start()
    {
        pauseMenu = GameObject.Find("Canvas/PauseMenu");
        pauseMenu.SetActive(false);
    }

    public void ActivatePauseMenu()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    // this should be called regardless of continue or main menu button is clicked so that the game is unpaused first
    public void DeactivatePauseMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
