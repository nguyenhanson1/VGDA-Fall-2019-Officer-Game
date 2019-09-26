using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Tooltip("Script of Bullet Prefab being shot.")]
    [SerializeField] private Bullet bullet;
    [Tooltip("Transform of reticle of the game.")]
    [SerializeField] private RectTransform cursor = null;
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
        Vector3 lookAt = persCam.ScreenToWorldPoint(new Vector3(cursor.position.x, cursor.position.y, (bullet.Speed * bullet.DespawnTime) + 20f));
        transform.LookAt(lookAt);
    }
}
