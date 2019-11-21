using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
    public GameObject Main_Menu;
    public static bool MainM = true;
    private Animator Camera;
    public GameObject Cam;
    private void Start()
    {
        
        Camera = Cam.GetComponent<Animator>();
    }
    public void Play()
    {
        Camera.SetBool("Static", false);
        Camera.SetBool("Cam_Scroll", true);
        Main_Menu.GetComponent<Canvas>().enabled = false;
        StartCoroutine(ChangeMenu());
        

    }

    IEnumerator ChangeMenu()
    {

        yield return new WaitForSeconds(4);
        Debug.Log("Changing Menu");
        StopCoroutine(ChangeMenu());
        SceneManager.LoadScene("MVP");

    }
}
