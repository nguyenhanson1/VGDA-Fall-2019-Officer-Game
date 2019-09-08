﻿using System.Collections;
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

    [Tooltip("Using controller controls? (Uses mouse if false.")]
    [SerializeField] private bool controllerEnabled = false;

    [Tooltip("Speed that cursor moves.")]
    [SerializeField] private float speed = 700f;


    private RectTransform cursor = null;

    private void Awake()
    {
        cursor = GetComponent<RectTransform>();

        //Hides cursor if using mouse controls.
        if (!controllerEnabled)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("Press ctrl + P to toggle the play button.  Ctrl + shift + P to pause.");
        }
    }

    private void Update()
    {
        //Controller controls
        if (controllerEnabled)
        {
            //Create new Vector to easily change reticle position
            Vector2 newPosition = cursor.position;
            newPosition.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            newPosition.y += Input.GetAxis("Vertical") * Time.deltaTime * speed;

            newPosition.x = Mathf.Clamp(newPosition.x, xBorders[0], canvas.rect.width - xBorders[1]);
            newPosition.y = Mathf.Clamp(newPosition.y, yBorders[0], canvas.rect.height - yBorders[1]);

            //Sets origin to the middle of the screen, and moves cursor to position needed
            cursor.position = newPosition;
        }
        //Mouse Controls
        else
        {
            //Create new Vector to easily change reticle position
            Vector2 newPosition = cursor.position;
            newPosition.x += Input.GetAxis("Mouse X") * Time.deltaTime * speed;
            newPosition.y += Input.GetAxis("Mouse Y") * Time.deltaTime * speed;

            newPosition.x = Mathf.Clamp(newPosition.x, xBorders[0], canvas.rect.width - xBorders[1]);
            newPosition.y = Mathf.Clamp(newPosition.y, yBorders[0], canvas.rect.height - yBorders[1]);

            //Sets origin to the middle of the screen, and moves cursor to position needed
            cursor.position = newPosition;
        }
        gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, new Vector2(269.5f, 151.5f), 0.2f); 
    }
}