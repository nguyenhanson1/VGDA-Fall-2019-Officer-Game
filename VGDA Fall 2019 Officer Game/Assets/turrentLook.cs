using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turrentLook : MonoBehaviour
{
    public Transform target;
    public float rotSpeed = 2f;
    private void OnCollisionEnter(Collision collision)
    {
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - this.transform.position;

        if (direction.z > 0)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                Quaternion.LookRotation(new Vector3(target.position.x, target.position.y, target.position.z)),
                                                Time.deltaTime * rotSpeed);
        }
        else if (direction.z <= 0)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                Quaternion.LookRotation(new Vector3(target.position.x, target.position.y, target.position.z)),
                                                Time.deltaTime * rotSpeed);
        }
        Debug.DrawLine(transform.position, target.transform.position, Color.red);
    }
}
