using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addforce : MonoBehaviour
{
    [SerializeField] private RectTransform cursor = null;
    [Tooltip("The RectTransform of the Canvas")]
    [SerializeField] private RectTransform canvas = null;
    public Camera cam;

    public float speed = 1.0f;  
    public Camera perscam = null;


    private void Update()
    {
        //Gets position of cursor to world
       // Vector3 screenPos = cam.WorldToScreenPoint(cursor.position);

       // Vector3 lookAt = perscam.ScreenToWorldPoint(new Vector3(cursor.position.x, cursor.position.y, 0));
        //If the cursor is on the edge of screen, rotate towards it

        // check is cursor on right
        if (cursor.position.x  == Screen.width )
        {
            Debug.Log("We on da Rite");

            Rotate();

            // stops infinite loop of turning
            //cursor.transform.position = new Vector3(cursor.position.x - 100, cursor.position.y, cursor.position.z);

        }
        //check if cursor on left
        if (cursor.position.x == 0)
        {
            Debug.Log("We on da Leff");
            //transform.Rotate(0, -90, 0);

            Rotate();

            // stops infinite loop of turning
            //cursor.transform.position = new Vector3(cursor.position.x + 100, cursor.position.y, cursor.position.z);


        }
        //Check for pos at top of screen
        if (cursor.position.y == Screen.height)
        {
            Debug.Log("We on da Top");
            //transform.Rotate(90, 0, 0);
            Rotate();


            // stops infinite loop of turning
            //cursor.transform.position = new Vector3(cursor.position.x , cursor.position.y - 100, -cursor.position.z);


        }
        //Check position for bottom
        if (cursor.position.y == 0)
        {
            Debug.Log("We on da Bottom");
            //transform.Rotate(-90, 0, 0);
            Rotate();
            //transform.LookAt(lookAt);

            // stops infinite loop of turning
            //cursor.transform.position = new Vector3(cursor.position.x, cursor.position.y + 100, cursor.position.z);
        }

    }
    void Rotate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 1 * vertical, 0);
        Vector3 FinalD = new Vector3(horizontal, 1 * vertical, 1.0f);
        transform.position += direction * speed * Time.deltaTime;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(FinalD), Mathf.Deg2Rad * 50.0f);
    }

}
