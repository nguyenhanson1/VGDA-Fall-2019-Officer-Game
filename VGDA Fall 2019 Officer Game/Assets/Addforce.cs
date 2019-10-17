using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addforce : MonoBehaviour
{
    [SerializeField] private RectTransform cursor = null;

    // rotate Speed
    public float speed = 1.0f;
    // Foward movement speed
    public float movementSpeed = 3.0f;

    private void Update()
    {

        //If the cursor is on the edge of screen, rotate towards it

        //Check cursor on right
        if (cursor.position.x  == Screen.width )
        {
            Debug.Log("We on da Rite");

            Rotate();
            // changes the forward momentum of the Player to what ever direction it's facing
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        }
        //check if cursor on left
        if (cursor.position.x == 0)
        {
            Debug.Log("We on da Leff");

            Rotate();
            // changes the forward momentum of the Player to what ever direction it's facing
            transform.position -= transform.forward * Time.deltaTime * movementSpeed;
        }
        //Check for pos at top of screen
        if (cursor.position.y == Screen.height)
        {
            Debug.Log("We on da Top");
            
            Rotate();
            // changes the forward momentum of the Player to what ever direction it's facing
            transform.position += transform.forward * Time.deltaTime * movementSpeed;
        }
        //Check position for bottom
        if (cursor.position.y == 0)
        {
            Debug.Log("We on da Bottom");

            Rotate();
            // changes the forward momentum of the Player to what ever direction it's facing
            transform.position -= transform.forward * Time.deltaTime * movementSpeed;
        }

    }
    //Rotates whole ass player object to the cursor
    void Rotate()
    {

        //Gets input from cursor movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //Creates vectors for Quaternion.RotateTowards

        //Creates initial direction that the player is facing
        Vector3 direction = new Vector3(horizontal, vertical, 0);
        // Creates the offset for possible player final Direction
        Vector3 FinalD = new Vector3(horizontal, vertical, 1.0f);
        //Rotates toward Final direction
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(FinalD), Mathf.Deg2Rad * 100.0f);
       
    }

}
