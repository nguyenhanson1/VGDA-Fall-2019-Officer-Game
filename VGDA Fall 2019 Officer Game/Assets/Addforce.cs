using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addforce : MonoBehaviour
{
    [SerializeField] private RectTransform cursor = null;

    public float speed = 1.0f;
    public bool DirChange = false;
    public Vector3 move;
    public float movementSpeed = 1.0f;
    private void Update()
    {
      

        //If the cursor is on the edge of screen, rotate towards it

        if (cursor.position.x  == Screen.width )
        {
            Debug.Log("We on da Rite");

            Rotate();
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        }
        //check if cursor on left
        if (cursor.position.x == 0)
        {
            Debug.Log("We on da Leff");

            Rotate();
            transform.position -= transform.forward * Time.deltaTime * movementSpeed;
        }
        //Check for pos at top of screen
        if (cursor.position.y == Screen.height)
        {
            Debug.Log("We on da Top");
            
            Rotate();
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        }
        //Check position for bottom
        if (cursor.position.y == 0)
        {
            Debug.Log("We on da Bottom");
            transform.position -= transform.forward * Time.deltaTime * movementSpeed;
            Rotate();

        }

    }
    //Rotates whole ass player object to the cursor
    void Rotate()
    {
        transform.position += transform.forward * Time.deltaTime * movementSpeed;
        DirChange = true;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, vertical, 0);
        Vector3 FinalD = new Vector3(horizontal, vertical, 1.0f);
        transform.position += direction * speed * Time.deltaTime;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(FinalD), Mathf.Deg2Rad * 50.0f);
    }

}
