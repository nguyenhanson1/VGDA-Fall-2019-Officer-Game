using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBot : MonoBehaviour
{
    [Tooltip("Position of reticle of the game.")]
    [SerializeField] private RectTransform cursor = null;
    [Tooltip("Perspective Camera being used by Player.")]
    [SerializeField] private Camera persCam = null;
    [Tooltip("Script of Bullet Prefab being shot.")]
    [SerializeField] private Bullet bullet = null;

    private void LaserPointer()
    {
        Debug.DrawRay(transform.position, transform.forward * (bullet.Distance), Color.red);
    }
    //Shoot Raycast from Cursor to check for targets
    public GameObject getHitPosition()
    {
        //Get raycast origin, direction, and distance
        Vector3 direction = transform.forward;

        Vector3 origin = cursor.position;
        origin.z = direction.z * bullet.Distance;
        origin = persCam.ScreenToWorldPoint(origin);

        Ray cursorSight = new Ray(origin, direction);

        //Shoot raycast and return gameObject if hit something with wanted layer
        if (Physics.Raycast(cursorSight, out RaycastHit hit, bullet.Distance, ~0, QueryTriggerInteraction.Collide))
            if(hit.transform.gameObject.GetComponent<IDamagable>() != null)
                return hit.transform.gameObject;
        return null;
    }
}