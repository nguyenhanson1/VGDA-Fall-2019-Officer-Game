using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private RectTransform cursor = null;
    [SerializeField] private Camera cursorCamera = null;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, 0);
    public Vector3 move;
    public float movementSpeed = 3.25f;
    public int invert = -1;


 
    private void Update()
    {
        Vector3 cursorPos = cursor.position;
        cursorPos.x = Screen.width / 2;

        move = Vector3.Lerp(transform.position, cursorCamera.ScreenToWorldPoint(cursorPos) + offset, Time.deltaTime * movementSpeed);
        move.z = cursorCamera.transform.position.z;



        transform.position = move;
        move += transform.forward * Time.deltaTime * movementSpeed;
    }
}
