﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Player : MonoBehaviour
{
    [Tooltip("AimBot script used to get target.")]
    [SerializeField] private AimBot aimBot = null;
    [Tooltip("How fast the Player shoots (Higher for faster).")]
    [SerializeField] private float dps = 1f;
    [Tooltip("Script where object will get their bullets from.")]
    [SerializeField] private ObjectPooler bulletPool = null;

    //Seconds between each shot
    private float shotDelay = 0;
    //Player can only shoo when it's true
    private bool shotDelayed = false;
    private Attack attack = new Attack();
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
                //Shoot Raycast to find if target in line of sight
                GameObject target = aimBot.getHitPosition();

                if (target != null)
                {
                    //Get direction to get rotation to look at enemy target (if any)
                    Vector3 direction = target.transform.position - transform.position;
                    attack.Shoot(gameObject, bulletPool, Quaternion.LookRotation(direction, transform.up));
                    Debug.Log("Gottem!");
                }
                else
                {
                    attack.Shoot(gameObject, bulletPool);
                }
                StartCoroutine(DelayShots());
            }
    }
    private IEnumerator DelayShots()
    {
        shotDelayed = true;
        yield return new WaitForSeconds(shotDelay);
        shotDelayed = false;
    }
}
