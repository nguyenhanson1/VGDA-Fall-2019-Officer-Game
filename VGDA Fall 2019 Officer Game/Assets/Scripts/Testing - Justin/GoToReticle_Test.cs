using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToReticle_Test : MonoBehaviour
{
    [SerializeField] RectTransform reticle = null;
    [SerializeField] Transform player = null;
    [SerializeField] Camera persCam = null;
    [SerializeField] private float offset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 reticlePos = reticle.position;
        reticlePos = persCam.ScreenToWorldPoint(reticlePos);
        //reticlePos.z = player.position.z;
        //reticlePos.z += offset;
        transform.position = reticlePos;
    }
}
