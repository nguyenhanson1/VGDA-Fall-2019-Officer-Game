using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Player : Attack_Base
{
    [SerializeField] private float dps = 1f;
    private bool shotDelayed = false;

    [SerializeField] private bool spawnAtCursor = false;
    [SerializeField] private RectTransform cursor = null;
    [SerializeField] private Camera cursorCamera = null;

    private void Update()
    {
        if (Input.GetButton("Fire1") && !shotDelayed)
        {
            if (spawnAtCursor)
            {
                Vector3 cursorLocation = cursorCamera.ScreenToWorldPoint(cursor.position);
                Shoot(cursorLocation);
            }
            else
                Shoot();

            StartCoroutine(DelayShots());
        }
    }

    private IEnumerator DelayShots()
    {
        shotDelayed = true;
        yield return new WaitForSeconds(dps);
        shotDelayed = false;
    }
}
