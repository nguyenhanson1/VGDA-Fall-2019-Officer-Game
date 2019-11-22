using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Center Cursor")]
    [Tooltip("Speed that cursor moves to the center.")]
    [SerializeField] private float centerSpeed = 1f;
    [Tooltip("Time that cursor needs to be still for centerCursor to activate.")]
    [SerializeField] private float delayTime = 1f;
    private float timer = 0f;

    [Header("Maneuver Ability")]
    [SerializeField] private Image cursorSprite = null;
    [SerializeField] private MeshRenderer laserSight = null;

    [Header("Aiming")]
    [SerializeField] private MeshRenderer playerMesh = null;
    [SerializeField] private Transform playerTrans = null;
    [Tooltip("How close cursor can be to center screen before mading player transparent")]
    [SerializeField] private float fadeDistance = 0.4f;
    [SerializeField] private Material opaquePlayer = null;
    [SerializeField] private Material transPlayer = null;
    [SerializeField] private Material opaqueLaserSight = null;
    [SerializeField] private Material transLaserSight = null;

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
        if (Input.GetButton("Maneuver"))
        {
            rectTrans.position = ChangePosition(Vector3.zero);
            cursorSprite.enabled = false;
            laserSight.enabled = false;
        }
        else
        {
            cursorSprite.enabled = true;
            laserSight.enabled = true;
            Vector3 input = GetInput();
            rectTrans.position = ChangePosition(input);
            CenterCursor();
        }
        
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

        Vector3 centerSceen = new Vector3(Screen.width / 2, Screen.height / 2, rectTrans.position.z);

        if (timer >= delayTime)
        {
            //Debug.Log("Going");
            rectTrans.position = Vector3.Lerp(rectTrans.position, centerSceen, centerSpeed * Time.deltaTime);
        }


        Debug.Log("Distance from Cursor: " + Vector2.Distance(centerSceen, rectTrans.position));
        if (Vector2.Distance(persCam.WorldToScreenPoint(playerTrans.position), rectTrans.position) <= fadeDistance)
        {
            playerMesh.material = transPlayer;
            laserSight.material = opaqueLaserSight;
        }
        else
        {
            playerMesh.material = opaquePlayer;
            laserSight.material = transLaserSight;
        }
    }
}
