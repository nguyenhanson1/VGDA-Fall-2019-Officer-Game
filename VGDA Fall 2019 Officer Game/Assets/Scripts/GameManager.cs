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
    public Canvas pauseMenu;
    public PanelManager menuManager;



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
            menuManager.enabled = true;
            Time.timeScale = 0f;
        }
    }
}
