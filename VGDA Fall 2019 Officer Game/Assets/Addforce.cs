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
        Vector3 screenPos = cam.WorldToScreenPoint(cursor.position);

        //If the cursor is on the edge of screen, rotate towards it
        if (cursor.position.x  == Screen.width )
        {
            Debug.Log("We on da Leff/ Rite");

            Vector3 lookAt = perscam.ScreenToWorldPoint(new Vector3(cursor.position.x, cursor.position.y, 0));
            transform.LookAt(lookAt);
            // Move our position closer to the target.
            //Quaternion rotation = Quaternion.LookRotation(screenPos);
            //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
           cursor.transform.position = new Vector3(cursor.position.x -100, cursor.position.y, cursor.position.z);

        }
        if (cursor.position.x == Screen.width/2)
        {
            Debug.Log("We on da Leff/ Rite");

            Vector3 lookAt = perscam.ScreenToWorldPoint(new Vector3(cursor.position.x, cursor.position.y, 0));
            transform.LookAt(lookAt);
            // Move our position closer to the target.
            //Quaternion rotation = Quaternion.LookRotation(screenPos);
            //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
            cursor.transform.position = new Vector3(cursor.position.x + 100, cursor.position.y, cursor.position.z);

        }
        if (cursor.position.y == Screen.height)
        {
            Debug.Log("We on da Top/ Bottom");

            Vector3 lookAt = perscam.ScreenToWorldPoint(new Vector3(cursor.position.x, cursor.position.y, 0));
            transform.LookAt(lookAt);
            cursor.transform.position = new Vector3(cursor.position.x , cursor.position.y - 100, cursor.position.z);
            // Move our position closer to the target.
            //Quaternion rotation = Quaternion.LookRotation(screenPos);
            //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);


        }

        if (cursor.position.y == Screen.height/2)
        {
            Debug.Log("We on da Top/ Bottom");

            Vector3 lookAt = perscam.ScreenToWorldPoint(new Vector3(cursor.position.x, cursor.position.y, 0));
            transform.LookAt(lookAt);
            cursor.transform.position = new Vector3(cursor.position.x, cursor.position.y + 100, cursor.position.z);
            // Move our position closer to the target.
            //Quaternion rotation = Quaternion.LookRotation(screenPos);
            //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);


        }



    }
}
