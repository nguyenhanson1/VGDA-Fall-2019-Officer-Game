using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScreenPosition : MonoBehaviour
{
    [SerializeField] private RectTransform cursor = null;
    [SerializeField] private Camera orthCamera = null;
    [SerializeField] private RectTransform canvas = null;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, 0);

    public float movementSpeed = 1.0f;


    private void Update()
    {
        Vector3 cursorPos = cursor.position;
        cursorPos.x = canvas.rect.width / 2;
        Vector3 move = Vector3.Lerp(transform.position, orthCamera.ScreenToWorldPoint(cursorPos) + offset, Time.deltaTime * movementSpeed);
        move.z = orthCamera.transform.position.z + 1f;

        transform.position = move;

    }
}
