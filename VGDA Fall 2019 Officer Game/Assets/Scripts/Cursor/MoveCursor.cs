using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCursor : MonoBehaviour
{
    [SerializeField] Camera persCam = null;
    [SerializeField] private Bullet bullet = null;
    [SerializeField] private RectTransform rectTrans = null;

    [Tooltip("Size of borders that'll stop the cursor in the x axis. [Left, Right]")]
    [SerializeField] private float[] xBorders = new float[2];
    [Tooltip("Size of borders that'll stop the cursor in the x axis. [Bottom, Top]")]
    [SerializeField] private float[] yBorders = new float[2];

    [Tooltip("Using controller controls? (Uses mouse if false.)")]
    [SerializeField] private bool controllerEnabled = false;

    [Tooltip("Speed that cursor moves.")]
    [SerializeField] private float speed = 700f;

    [SerializeField] private float centerSpeed = 1f;
    [SerializeField] private float delayTime = 1f;

    public Vector2 CursorPosition
    {
        get
        {
            Vector2 screenPos = persCam.ScreenToViewportPoint(rectTrans.position);
            screenPos.x = (screenPos.x - 0.5f) * 2;
            screenPos.y = (screenPos.y - 0.5f) * 2;

            return screenPos;
        }
    }

    [SerializeField]private float timer = 0f;

    private void OnEnable()
    {
        GameManager.StartOccurred += BootUp;
        GameManager.UpdateOccurred += FunctionHandler;

    }
    private void OnDisable()
    {
        GameManager.StartOccurred -= BootUp;
        GameManager.UpdateOccurred -= FunctionHandler;

    }


    //Removes need of adding multiple functions to delegates
    private void BootUp()
    {
        if (controllerEnabled)
            HideCursor(false);
        else
            HideCursor(true);

        rectTrans.position = new Vector3(Screen.width/2, Screen.height/2, 1f);
    }
    private void FunctionHandler()
    {
        Vector3 input = GetInput();
        rectTrans.position = ChangePosition(input);
        CenterCursor();
    }

    private Vector3 GetInput()
    {
        Vector3 input = Vector3.zero;
        input.x = controllerEnabled ? Input.GetAxis("Horizontal") : Input.GetAxis("Mouse X");
        input.y = controllerEnabled ? Input.GetAxis("Vertical") : Input.GetAxis("Mouse Y");

        return input * Time.deltaTime * speed;
    }

    //Hides cursor
    private void HideCursor(bool locked)
    {
        Cursor.visible = false;
        if(locked)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;

        Debug.Log("Press ctrl + P to toggle the play button.  Ctrl + shift + P to pause.");
    }
    private Vector3 ChangePosition(Vector3 speed)
    {
        Vector3 newPosition = rectTrans.transform.position + speed;
        newPosition.x = Mathf.Clamp(newPosition.x, xBorders[0], Screen.width - xBorders[1]);
        newPosition.y = Mathf.Clamp(newPosition.y, yBorders[0], Screen.height - yBorders[1]);

        return newPosition;

    }
    private void CenterCursor()
    {
        if (!(Input.GetButton("Horizontal") || Input.GetButton("Vertical")))
            timer += (timer <= delayTime) ? Time.deltaTime : 0f;
        else
            timer = 0f;

        if(timer >= delayTime)
        {
            //Debug.Log("Going");
            Vector3 centerSceen = new Vector3(Screen.width/2, Screen.height/2, rectTrans.position.z);
            rectTrans.position = Vector3.Lerp(rectTrans.position, centerSceen, centerSpeed * Time.deltaTime);
        }
    }
}
