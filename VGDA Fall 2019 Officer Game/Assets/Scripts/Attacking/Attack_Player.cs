using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Player : Attack_Base
{
    [SerializeField] private float dps = 1f;
    private float shotDelay = 0;
    private bool shotDelayed = false;

    [SerializeField] private bool spawnAtCursor = false;
    [SerializeField] private RectTransform cursor = null;
    [SerializeField] private Camera cursorCamera = null;
    private void Start()
    {
        shotDelay = 1f / dps;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * cursorCamera.farClipPlane*2);

        if (Input.GetButton("Fire1"))
        {
            if (!shotDelayed)
            {
                StartCoroutine(DelayShots());
                if (spawnAtCursor)
                {
                    Vector3 cursorLocation = cursorCamera.ScreenToWorldPoint(cursor.position);
                    Shoot(null, Quaternion.identity);
                }
                else
                    Shoot();
            }
        }
    }

    private IEnumerator DelayShots()
    {
        shotDelayed = true;
        yield return new WaitForSeconds(shotDelay);
        shotDelayed = false;
    }
}
