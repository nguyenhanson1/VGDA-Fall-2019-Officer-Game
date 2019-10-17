using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSight_Test : MonoBehaviour
{
    [Tooltip("Script of Bullet Prefab being shot.")]
    [SerializeField] private Bullet bullet;
    [Tooltip("Perspective Camera being used by Player.")]
    [SerializeField] private Camera persCam = null;

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

        //Have Player look at the point where the cursor is pointing at the depth of how far it can shoot
        Vector3 lookAt = persCam.ScreenToWorldPoint(new Vector3(transform.position.x, transform.position.y, (bullet.Speed * bullet.DespawnTime) / 2));
        Debug.DrawLine(persCam.ScreenToWorldPoint(transform.position), lookAt*100);
    }
}
