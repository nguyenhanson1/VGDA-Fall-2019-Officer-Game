using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    //public static bool PlayerSelect = false;
  
    public GameObject Main_Menu;
    public static bool MainM = true;
    private Animator Camera;
    public GameObject Cam;
    private void Start()
    {
        Cam = GameObject.Find("Main Camera");
        Camera = Cam.GetComponent<Animator>();
    }
    public void Play()
    {
        Camera.Play("Camera_Pan");
        StartCoroutine(ChangeMenu());
        Main_Menu.SetActive(false);

        StopCoroutine(ChangeMenu());

    }

    IEnumerator ChangeMenu()
    {

        yield return new WaitForSeconds(3);
  

    }
}
