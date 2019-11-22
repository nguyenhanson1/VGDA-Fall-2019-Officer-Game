using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public Canvas pauseMenu;
    public Animator anim;
    public bool gamePaused = false;
    private void OnEnable()
    {
        GameManager.UpdateOccurred += checkForPause;
    }

    private void OnDisable()
    {
        GameManager.UpdateOccurred -= checkForPause;
    }
    void checkForPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {

                Resume();
            }
            else
            {

                Pause();
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        anim.SetBool("Open", false);
        gamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        Time.timeScale = 0f;
        anim.SetBool("Open", true);
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Application.LoadLevel("MVP"); 
    }
}
