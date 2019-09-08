using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public int invert = -1;
    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), invert * Input.GetAxis("Vertical"), 0);
        Vector3 finalDirection = new Vector3(horizontal, invert * vertical, 5.0f);
        transform.position += direction *movementSpeed * Time.deltaTime;

        transform.position += direction * movementSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(finalDirection), Mathf.Deg2Rad * 50.0f);
        
        
    }
}
