using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addforce : MonoBehaviour
{
    [SerializeField] private RectTransform cursor = null;
    [SerializeField] private Camera cursorCamera = null;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, 0);
    [SerializeField] public GameObject Player;

    public float movementSpeed = 1.0f;
    public int invert = -1;


    private void Update()
    {
        Vector3 move = Vector3.Lerp(transform.position, cursorCamera.ScreenToWorldPoint(cursor.position) + offset, Time.deltaTime * movementSpeed);
        move.z = cursorCamera.transform.position.z + 1f;

        transform.position = move;
        if(cursor.position.x == Screen.width || cursor.position.y == Screen.height )
        {
            Vector3 PlayerRotate = Vector3.Lerp(transform.position, cursorCamera.ScreenToWorldPoint(cursor.position) + offset, Time.deltaTime * movementSpeed);
            PlayerRotate.x = Player.transform.position.x* movementSpeed;
            PlayerRotate.y = Player.transform.position.y * movementSpeed;
        }

    }
}
