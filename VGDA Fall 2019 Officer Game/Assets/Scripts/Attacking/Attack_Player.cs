using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Player : Attack_Base
{
    [Tooltip("How fast the Player shoots (Higher for faster).")]
    [SerializeField] private float dps = 1f;
    //Seconds between each shot
    private float shotDelay = 0;
    //Player can only shoo when it's true
    private bool shotDelayed = false;

    private void OnEnable()
    {
        shotDelay = 1f / dps;
        GameManager.UpdateOccurred += checkforShot;
    }
    private void OnDisable()
    {
        GameManager.UpdateOccurred -= checkforShot;
    }

    //Check if Fire button was pressed.
    private void checkforShot()
    {
        if (Input.GetButton("Fire1"))
            if (!shotDelayed)
            {
                StartCoroutine(DelayShots());
                Shoot();
            }
    }
    private IEnumerator DelayShots()
    {
        shotDelayed = true;
        yield return new WaitForSeconds(shotDelay);
        shotDelayed = false;
    }
}
