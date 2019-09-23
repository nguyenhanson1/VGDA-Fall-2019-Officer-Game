using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBois : MonoBehaviour
{
    public Vector2 spot;
    public GameObject thing;
    public Camera cam;
    public RectTransform cursor;
    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Debug.Log("Spawn");
            Instantiate(thing, cam.ScreenToWorldPoint(new Vector3(cursor.position.x, cursor.position.y, 59)), Quaternion.Euler(0f, 0f, 0f));
            Instantiate(thing, cam.ScreenToWorldPoint(new Vector3(cursor.position.x, cursor.position.y, cam.farClipPlane)), Quaternion.Euler(0f, 0f, 0f));

        }
    }
}
