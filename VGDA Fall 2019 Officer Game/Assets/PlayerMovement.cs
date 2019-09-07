using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float xySpeed = 10;
  
    // Update is called once per frame
    void Update()
    {
        float YAxis = Input.GetAxis("Mouse Y");
        float XAxis = Input.GetAxis("Mouse X");

        LocalMove(YAxis, XAxis, xySpeed);
    }

    void LocalMove(float x, float y, float speed)
    {
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
    }
    void StaticPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
        pos.y = Mathf.Clamp01(pos.y);
    }
}
