using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveReticle : MonoBehaviour
{
    [Tooltip("The RectTransform of the Canvas")]
    [SerializeField] private RectTransform canvas = null;

    [Tooltip("Size of borders that'll stop the cursor in the x axis [Left, Right]")]
    [SerializeField] private float[] xBorders = new float[2];
    [Tooltip("Size of borders that'll stop the cursor in the x axis [Bottom, Top]")]
    [SerializeField] private float[] yBorders = new float[2];

    private float xCanvas;
    private float yCanvas;

    private RectTransform cursor = null;

    private void Awake()
    {
        cursor = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //Set position to go to the left or right depending on which direction is pressed
        if (Input.GetAxis("Horizontal") > 0)
            xCanvas = Input.GetAxis("Horizontal") * ((canvas.rect.width / 2) - xBorders[1]);
        else
            xCanvas = Input.GetAxis("Horizontal") * ((canvas.rect.width / 2) - xBorders[0]);
        //Set position to go to the up or down depending on which direction is pressed
        if ((Input.GetAxis("Vertical") > 0))
            yCanvas = Input.GetAxis("Vertical") * ((canvas.rect.height / 2) - yBorders[1]);
        else
            yCanvas = Input.GetAxis("Vertical") * ((canvas.rect.height / 2) - yBorders[0]);

        //Sets origin to the middle of the screen, and moves cursor to position needed
        cursor.position = new Vector2(xCanvas + canvas.rect.width / 2f, yCanvas + canvas.rect.height / 2);
    }
}
