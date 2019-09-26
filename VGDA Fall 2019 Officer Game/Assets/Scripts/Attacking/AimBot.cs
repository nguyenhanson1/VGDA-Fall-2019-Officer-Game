using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBot : MonoBehaviour
{
    [Tooltip("Position of reticle of the game.")]
    [SerializeField] private RectTransform cursor = null;
    [Tooltip("Orthographic Camera being used by Player.")]
    [SerializeField] private Camera orthCam = null;
    [Tooltip("Script of Bullet Prefab being shot.")]
    [SerializeField] private Bullet bullet = null;

    private void LaserPointer()
    {
        Debug.DrawRay(transform.position, transform.forward* (bullet.Speed * bullet.DespawnTime + 20f), Color.red);
    }
    //Shoot Raycast from Cursor to check for targets
    public GameObject getHitPosition()
    {
        //Get raycast origin, direction, and distance
        Vector3 origin = orthCam.ScreenToWorldPoint(cursor.position);
        Vector3 direction = transform.forward;
        Ray cursorSight = new Ray(origin, direction);

        float distance = bullet.Speed * bullet.DespawnTime + 20f;
        //Shoot raycast and return gameObject if hit something with wanted layer
        if (Physics.Raycast(cursorSight, out RaycastHit hit, distance, bullet.Targets.value, QueryTriggerInteraction.Collide))
            return hit.transform.gameObject;
        return null;
    }
}
