using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScreenPosition : MonoBehaviour
{
    [SerializeField] private RectTransform reticle = null;
    [SerializeField] private Camera persCam = null;
    [SerializeField] private float maxHeight = 3.2f;
    [SerializeField] private float movementSpeed = 1.0f;


    private void Update()
    {

        float reticlePosY = ((reticle.position.y / Screen.height) - 0.5f) * 2f * maxHeight;

        

        reticlePosY = Vector3.Lerp(transform.localPosition, Vector3.up * reticlePosY, Time.deltaTime * movementSpeed).y;

        Vector3 move = new Vector3(0f, reticlePosY, 1f);
        //Debug.Log(move);
       

        transform.localPosition = move; 

    }
}
