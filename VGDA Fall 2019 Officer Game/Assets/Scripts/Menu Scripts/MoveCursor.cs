using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCursor : MonoBehaviour
{
    [Tooltip("The RectTransform of the Canvas")]
    [SerializeField] private RectTransform canvas = null;

    [Tooltip("Size of borders that'll stop the cursor in the x axis. [Left, Right]")]
    [SerializeField] private float[] xBorders = new float[2];
    [Tooltip("Size of borders that'll stop the cursor in the x axis. [Bottom, Top]")]
    [SerializeField] private float[] yBorders = new float[2];

    [Tooltip("Using controller controls? (Uses mouse if false.)")]
    [SerializeField] private bool controllerEnabled = false;
    [Tooltip("Have cursor stay in the middle of the screen when no input? (Controller Only.)")]
    [SerializeField] private bool keepCursorCentered = false;

    [Tooltip("Speed that cursor moves.")]
    [SerializeField] private float speed = 700f;


    private RectTransform cursor = null;

    private void OnEnable()
    {
        GameManager.UpdateOccurred += FunctionHandler;

    }
    //Removes need of adding all 4 functions to delegate
    private void FunctionHandler()
    {
        if (controllerEnabled)
        {
            ShowCursor();
            ControllerControls();
        }
        else
        {
            HideCursor();
            MouseControls();
        }
        if (Input.GetButtonDown("CenterCursor"))
            CenterCursor();
    }

    //Hides cursor if using mouse controls.
    private void HideCursor()
    {
        if (Cursor.visible == true)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("Press ctrl + P to toggle the play button.  Ctrl + shift + P to pause.");
        }
    }
    //Shows cursor if not using mouse controls.
    private void ShowCursor()
    {
        if(Cursor.visible == false)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    //Returns the Cursor to center screen.
    private void CenterCursor()
    {
        cursor.position = new Vector2(canvas.rect.width / 2, canvas.rect.height / 2);
    }
    //Set position of Cursor depending on Horizontal and Vertical axis inputs.
    private void ControllerControls()
    {
        if (controllerEnabled)
        {
            if (keepCursorCentered)
                cursor.position = new Vector2(GetX_Centered(), GetY_Centered());
            else
                cursor.position = new Vector2(GetX_Free(), GetY_Free());
        }
    }
    //Centered Functions return the cursor to center screen if there's no input
    private float GetX_Centered()
    {
        float centerScreen = (canvas.rect.width / 2);
        //Times input by centerScreen to move Cursor through the other half of screen (0 = center, 1 = edge of screen)
        float input = Input.GetAxis("Horizontal") * centerScreen;
        //Have borders to accomodate HUD or other restrictions
        float border = (input > 0) ? xBorders[1] : xBorders[0];

        return centerScreen + input - border;
    }
    private float GetY_Centered()
    {
        float centerScreen = (canvas.rect.height / 2);
        //Times input by centerScreen to move Cursor through the other half of screen (0 = center, 1 = edge of screen)
        float input = Input.GetAxis("Vertical") * centerScreen;
        //Have borders to accomodate HUD or other restrictions
        float border = (input > 0) ? yBorders[1] : yBorders[0];

        return centerScreen + input - border;
    }
    //Free Functions let's Cursor travel around screen
    private float GetX_Free()
    {
        float xPos = cursor.position.x;
        //Move Cursor position
        xPos += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        //Clamp max position to screen borders
        xPos = Mathf.Clamp(xPos, xBorders[0], canvas.rect.width - xBorders[1]);
        return xPos;
    }
    private float GetY_Free()
    {
        float yPos = cursor.position.y;
        //Move Cursor position
        yPos += Input.GetAxis("Vertical") * Time.deltaTime * speed;
        //Clamp max position to screen borders
        yPos = Mathf.Clamp(yPos, yBorders[0], canvas.rect.height - yBorders[1]);
        return yPos;
    }
    //Have mouse replace the cursor
    private void MouseControls()
    {
        //Create new Vector to easily change reticle position
        Vector2 newPosition = cursor.position;
        newPosition.x += Input.GetAxis("Mouse X") * Time.deltaTime * speed;
        newPosition.y += Input.GetAxis("Mouse Y") * Time.deltaTime * speed;

        //Clamp max positions to screen borders
        newPosition.x = Mathf.Clamp(newPosition.x, xBorders[0], canvas.rect.width - xBorders[1]);
        newPosition.y = Mathf.Clamp(newPosition.y, yBorders[0], canvas.rect.height - yBorders[1]);

        //Sets origin to the middle of the screen, and moves cursor to position needed
        cursor.position = newPosition;
    }
}
