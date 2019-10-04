using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeTarget : MonoBehaviour
{
    public Vector3 start;
    public Vector3 end;
    public float startTime;
    public float speed;
    public float distance;

    private void OnEnable()
    {
        GameManager.UpdateOccurred += GoTo;
    }

    private void OnDisable()
    {
        GameManager.UpdateOccurred -= GoTo;
    }
    private void GoTo()
    {
        transform.position = Vector3.MoveTowards(transform.position, end, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, end) == 0)
        {
            transform.position = start;
        }
            
    }
}
