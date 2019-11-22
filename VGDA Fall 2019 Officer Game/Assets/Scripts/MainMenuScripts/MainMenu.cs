using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   
    public GameObject Main_Menu;
    public static bool MainM = true;
    public static bool CamPan;
    public static bool Static;
    private Animator Camera;
    public GameObject Cam;
    private void Start()
    {
        CamPan = false;
        Static = true;
        Camera = Cam.GetComponent<Animator>();
    }
    public void Play()
    {
        CamPan = true;
        Static = false;
        Camera.SetBool("Static", Static);
        Camera.SetBool("Cam_Scroll", CamPan);
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
