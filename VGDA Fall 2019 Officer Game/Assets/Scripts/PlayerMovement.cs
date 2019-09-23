using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private RectTransform cursor = null;
    [SerializeField] private Camera cursorCamera = null;
    [SerializeField] private Camera mainCamera = null;
    [SerializeField] private Bullet bullet;

    [SerializeField] private Vector3 offset = new Vector3(0, 0, 0);

    public float movementSpeed = 1.0f;
    public int invert = -1;
    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * (bullet.Speed * bullet.DespawnTime + 20f), Color.red);

        Vector3 move = Vector3.Lerp(transform.position, cursorCamera.ScreenToWorldPoint(cursor.position) + offset, Time.deltaTime * movementSpeed);
        move.z = cursorCamera.transform.position.z + 1f;

        transform.position = move;

        Vector3 lookAt = mainCamera.ScreenToWorldPoint(new Vector3(cursor.position.x,cursor.position.y, (bullet.Speed * bullet.DespawnTime) + 20f));
        transform.LookAt(lookAt);
        
        
    }
}
