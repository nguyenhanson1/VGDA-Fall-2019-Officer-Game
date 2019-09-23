using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Tooltip("Script of Bullet Prefab being shot.")]
    [SerializeField] private Bullet bullet;
    [Tooltip("Reticle of the game.")]
    [SerializeField] private RectTransform cursor = null;
    [Tooltip("Perspective Camera being used by Player.")]
    [SerializeField] private Camera cursorCam = null;

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
        //Bugtest to see where the bullets are going to fly to
        Debug.DrawRay(transform.position, transform.forward * (bullet.Speed * bullet.DespawnTime + 20f), Color.red);

        //Have Player look at the point where the cursor is pointing at the depth of how far it can shoot
        Vector3 lookAt = cursorCam.ScreenToWorldPoint(new Vector3(cursor.position.x, cursor.position.y, (bullet.Speed * bullet.DespawnTime) + 20f));
        transform.LookAt(lookAt);
    }

}
