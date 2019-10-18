using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] private float rotateStability = 2f;
    [SerializeField] private MoveCursor cursor;
 
    private void OnEnable()
    {
        GameManager.UpdateOccurred += RotatePlayer;
    }
    private void OnDisable()
    {
        GameManager.UpdateOccurred -= RotatePlayer;
    }

    private void RotatePlayer()
    {
        Vector2 input = cursor.CursorPosition;
        Vector3 finalDirection = new Vector3(input.x, input.y, rotateStability);

        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.LookRotation(finalDirection), Mathf.Deg2Rad * 50f);
    }
}
