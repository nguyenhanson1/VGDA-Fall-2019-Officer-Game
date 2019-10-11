using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addforce : MonoBehaviour
{
    [SerializeField] private RectTransform cursor = null;
    [SerializeField] private Camera cursorCamera = null;


    public float rotSpeed = 1.0f;
    public float movementSpeed = 1.0f;

    private void Update()
    {

        Vector3 move = Vector3.Lerp(transform.position, cursorCamera.ScreenToWorldPoint(cursor.position), Time.deltaTime * movementSpeed);
        move.z = cursorCamera.transform.position.z + 1f;

        transform.position = move;
        if (cursor.position.y == Screen.height )
        {
            Debug.Log("We on da Top");
            Vector3 direction = cursorCamera.ScreenToWorldPoint(cursor.position );
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.left);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, rotSpeed * Time.deltaTime);


        }
        if(cursor.position.x == Screen.width)
        {
            Debug.Log("We on da Sides");
            Vector3 direction = cursorCamera.ScreenToWorldPoint(cursor.position);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, rotSpeed * Time.deltaTime);
        }

    }
}
