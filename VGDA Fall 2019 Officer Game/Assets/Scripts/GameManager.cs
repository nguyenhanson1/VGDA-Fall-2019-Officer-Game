using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //onStart Delegates
    public delegate void onStart();
    public static event onStart StartOccurred;

    //onUpdate Delegates
    public delegate void onUpdate();
    public static event onUpdate UpdateOccurred;

    //Variables
     /*Public variables*/ 
    public Canvas pauseMenu;
    public PanelManager menuManager;
    public Animator anim;

    /*Private variables*/
    private bool gamePaused = false;

    private void OnEnable()
    {
        UpdateOccurred += checkForPause;
    }

    private void OnDisable()
    {
        UpdateOccurred -= checkForPause;
    }

    void Start()
    {
        if (StartOccurred != null)
            StartOccurred();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (UpdateOccurred != null)
            UpdateOccurred();
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
    public void Resume()
    {
        menuManager.CloseCurrent();
        Time.timeScale = 1f;
        gamePaused = false;
    }

    void Pause()
    {
        anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        menuManager.OpenPanel(menuManager.initiallyOpen);
        Time.timeScale = 0f;
        gamePaused = true;
    }
}
