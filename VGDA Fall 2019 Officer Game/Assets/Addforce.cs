using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addforce : MonoBehaviour
{
    [SerializeField] private RectTransform cursor = null;
    [SerializeField] private Camera cursorCamera = null;
    public GameObject Player;

    public float rotSpeed = 1.0f;


    private void Update()
    {


        if(cursor.position.x == Screen.width || cursor.position.y == Screen.height )
        {
            Debug.Log("We on da Edge");
            Vector3 direction = cursor.position - transform.position;
      
            Quaternion q = Quaternion.LookRotation(direction);
            Player.transform.rotation = Quaternion.Lerp(transform.rotation, q, rotSpeed * Time.deltaTime);
           
        }

    }
}
