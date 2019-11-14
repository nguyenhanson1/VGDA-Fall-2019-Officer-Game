﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour, IDamagable
{
    public Factions.Faction myFaction => Factions.Faction.Good;
    public Health health => totalHealth;

    private Health totalHealth = new Health();

    [Header("Health")]
    [SerializeField] private int displayHealth = 0;
    [SerializeField] private int maxHealth = 5;

    [Header("Attacking")]
    [Tooltip("Script where object will get their bullets from.")]
    [SerializeField] private ObjectPooler bulletPool = null;
    [Tooltip("Main Camera of the Game")]
    [SerializeField] private Camera persCam = null;
    [Tooltip("How fast the Player shoots (Higher for faster).")]
    [SerializeField] private float dps = 1f;

    //Seconds between each shot
    private float shotDelay = 0;
    //Player can only shoo when it's true
    private bool shotDelayed = false;
    private Attack attack = new Attack();

    private void OnEnable()
    {
        shotDelay = 1f / dps;
        GameManager.StartOccurred += Initialize;
        GameManager.UpdateOccurred += checkforShot;
        Health.OnDeath += Begoned;
        GameManager.UpdateOccurred += DisplayHealth;
    }
    private void OnDisable()
    {
        GameManager.StartOccurred -= Initialize;
        GameManager.UpdateOccurred -= checkforShot;
        Health.OnDeath -= Begoned;
        GameManager.UpdateOccurred -= DisplayHealth;
    }


    private void Initialize()
    {
        totalHealth.HealthTotal = maxHealth;
    }

    private void DisplayHealth()
    {
        displayHealth = totalHealth.HealthTotal;
    }

    //Destroys Player when its health goes to 0
    private void Begoned(Health h)
    {
        if (totalHealth.HealthTotal <= 0)
        { // totalHealth == h
            // Play Death Animation
            Destroy(gameObject);
        }
    }


    //Check if Fire button was pressed.
    private void checkforShot()
    {
        if (Input.GetButton("Fire1"))
            if (!shotDelayed)
            {
                attack.Shoot(gameObject, bulletPool);


                /*
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
                */
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