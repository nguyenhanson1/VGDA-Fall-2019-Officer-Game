using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addforce : MonoBehaviour
{
    [SerializeField] private RectTransform cursor = null;
    public Transform target;

    public float speed = 1.0f;


    private void Update()
    {
        
        if(cursor.position.x == Screen.width || cursor.position.y == Screen.height )
        {
            Debug.Log("We on da Edge");
            Vector3 direction = cursor.position - transform.position;
            float step = speed * Time.deltaTime;
            Vector3 targetDir = target.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
            PlayerMovement playerMovement = GetComponent<PlayerMovement>();
            playerMovement.move = playerMovement.move * -1;

        }

    }
}
