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
    //turning speed
    public float turnSpeed = 2.0f;
    public Rigidbody rigid;
    public GameObject chickenLook;

    //padding for where cursor location is to turn
    public float padding;
    public float activePadding;
    //turn percentage of Horrizontal and Vertical force
    [SerializeField]
    private float turnPercentH = 0;
    private float turnPercentV = 0;
    //Turn force of Horizontal and Verical Direction (Y and X perspectively)
    private float turnForceY = 0;
    private float turnForceX = 0;
    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float turn = turnSpeed;
        float padding;
        if (Input.GetKey(KeyCode.Space))
        {
            rigid.velocity = chickenLook.transform.forward * movementSpeed /2;
            turn = turnSpeed * 2;
            padding =  0;
        }
        else
        {
            rigid.velocity = chickenLook.transform.forward * movementSpeed;
            turn = turnSpeed;
            padding = activePadding;
        }
        
        float ratio = (cursor.position.x + 0.001f) / Screen.width;

        //Check cursor on right
        if (cursor.position.x  <= Screen.width && cursor.position.x > (Screen.width/2))
        {
            turnPercentH = Mathf.Clamp(turnPercentH + 0.5f, 0, 1);
            //Debug.Log("We on da Rite");
            if (cursor.position.x >= (Screen.width * (1 - padding)))
                turnForceY = turn * ratio * turnPercentH;
            else
                turnForceY = 0; 
        }
        //check if cursor on left
        else if (cursor.position.x >= 0 && cursor.position.x < (Screen.width/2))
        {
            turnPercentH = Mathf.Clamp(turnPercentH + 0.05f, 0, 1);
            //Debug.Log("We on da Leff");
            if (cursor.position.x <= (Screen.width*padding) +0.01f)
                turnForceY = -turn* (1 - ratio) * turnPercentH;
            else            
                turnForceY = 0;
        }  
        else
        {
            turnForceY = 0;
            turnPercentH = 0;
            turnPercentH = Mathf.Clamp(turnPercentH - 0.05f, 0, 1);
        }
        rigid.angularVelocity = new Vector3(rigid.angularVelocity.x, turnForceY, 0f);
        //Check for pos at top of screen
        //if (cursor.position.y == Screen.height)
        //{
        //    Debug.Log("We on da Top");

        //    RotateVertical();
        //}
        ////Check position for bottom
        //if (cursor.position.y == 0)
        //{
        //    Debug.Log("We on da Bottom");

        //    RotateVertical();

        //}
        // changes the forward momentum of the Player to what ever direction it's facing
    }
    void RotateHorrizontal()
    {
        //Gets input from cursor movement
        float horizontal = Input.GetAxis("Horizontal");

        //if (horizontal > 0)
        //{
        //    if (turnPercentH < 1)
        //    {
        //        turnPercentH += 0.01f;
        //    }
        //    turnForceY = horizontal * turnSpeed * turnPercentH;
        //}
        //else if (horizontal < 0)
        //{
        //    if (turnPercentH < 1)
        //    {
        //        turnPercentH += 0.01f;
        //    }
        //    turnForceY = turnSpeed * turnPercentH;
        //}
        //else
        //{
        //    turnPercentH = 0;
        //    turnForceY = 0;
        //}

        //if (turnForceY != 0)
        //{
           
        //}
        //else
        //{
        //    rigid.angularVelocity = new Vector3(rigid.angularVelocity.x, 0f, 0f);
        //}
    }
    
    void RotateVertical()
    {
        float vertical = Input.GetAxis("Vertical");

        if (vertical > 0)
        {
            if (turnPercentV < 1)
            {
                turnPercentV += 0.01f;
            }
            turnForceX = vertical * turnSpeed * turnPercentV;
        }
        else if (vertical < 0)
        {
            if (turnPercentV < 1)
            {
                turnPercentV += 0.01f;
            }
            turnForceX = vertical * turnSpeed * turnPercentV;
        }
        else
        {
            turnPercentV = 0;
            turnForceX = 0;
        }

        if (turnForceX != 0)
        {
            rigid.angularVelocity = new Vector3(-turnSpeed *Time.deltaTime, rigid.angularVelocity.y, 0f);
        }
        else
        {
            rigid.angularVelocity = new Vector3(0f, rigid.angularVelocity.y, 0f);
        }
    }
    
    //Rotates whole ass player object to the cursor
    void Rotate()
    {
        
        
        
        //Creates vectors for Quaternion.RotateTowards

        //// Creates the offset for possible player final Direction
        //Vector3 FinalD = new Vector3(horizontal, vertical, 1.0f);

        ////Rotates toward Final direction
        //transform.rotation =  Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(FinalD), Mathf.Deg2Rad * 100.0f);
       

        

    }

}
