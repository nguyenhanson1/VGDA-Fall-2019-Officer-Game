using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private RectTransform cursor = null;
    [SerializeField] private Camera cursorCamera = null;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, 0);
    public Vector3 move;
    public float movementSpeed = 1.0f;
    public int invert = -1;

 
    private void Update()
    {
        move = Vector3.Lerp(transform.position, cursorCamera.ScreenToWorldPoint(cursor.position) + offset, Time.deltaTime * movementSpeed);
        move.z = cursorCamera.transform.position.z + 1f;

        transform.position = move;
        
    }
}
