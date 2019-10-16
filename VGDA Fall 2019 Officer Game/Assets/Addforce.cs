using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addforce : MonoBehaviour
{
    [SerializeField] private RectTransform cursor = null;
    [Tooltip("The RectTransform of the Canvas")]
    [SerializeField] private RectTransform canvas = null;
    public GameObject Player;
    public Transform target;

    public float speed = 1.0f;
    [Tooltip("Size of borders that'll stop the cursor in the x axis. [Left, Right]")]
    [SerializeField] private float[] xBorders = new float[2];
    [Tooltip("Size of borders that'll stop the cursor in the x axis. [Bottom, Top]")]
    [SerializeField] private float[] yBorders = new float[2];
    //Clamp max positions to screen borders
    // Vector2 newPosition = cursor.transform.position;

 

    private void Update()
    {
        // Checks position of cursor.
        //If the cursor is on the edge of screen, rotate towards it
        if(cursor.position.x == Mathf.Clamp(cursor.position.x, xBorders[0], canvas.rect.width - xBorders[1]))
        {
            Debug.Log("We on da Leff/ Rite");


            Vector3 targetDir = target.position - transform.position;

            // The step size is equal to speed times frame time.
            float step = speed * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
          

            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);

        }
        if (cursor.position.y == Mathf.Clamp(cursor.position.y, yBorders[0], canvas.rect.height - yBorders[1]))
        {
            Debug.Log("We on da Top/Bottom");


            Vector3 targetDir = target.position - transform.position;

            // The step size is equal to speed times frame time.
            float step = speed * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);


            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);
        }

    }
}
