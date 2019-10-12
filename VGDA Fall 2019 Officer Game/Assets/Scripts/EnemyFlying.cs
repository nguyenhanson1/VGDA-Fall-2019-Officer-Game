﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : Enemy
{
    [SerializeField] protected enum pathOptions {
        BackForth,
        Circular,
        Random,
        FollowPlayer
    }

    [Tooltip("The furthest distance the enemy flies (BackForth) or the radius of the path (Circular)")]
    [SerializeField] protected float xDistanceFromOrigin = 10f;
    private bool backForthTurned = false;
    private Vector3 backForthEdge;
    private float backForthJourneyLength;
    private float startTime;

    protected virtual void OnEnable()
    {
        Health.OnDeath += Begoned;
        GameManager.StartOccurred += Initialize;
        GameManager.UpdateOccurred += Move;
    }

    protected virtual void OnDisable()
    {
        Health.OnDeath -= Begoned;
        GameManager.StartOccurred -= Initialize;
        GameManager.UpdateOccurred -= Move;
    }
    protected override void Attack()
    {
        // Do nothing
    }

    protected override void Move()
    {

        pathOptionBackForth();
    }

    protected void pathOptionBackForth() {
        if (!backForthTurned) {
            float travelled = (Time.time - startTime) * moveSpeed;
            float fractionTravelled = travelled / backForthJourneyLength;
            transform.position = Vector3.Lerp(gameSpawnPoint, backForthEdge,fractionTravelled);
        }
    }

    protected override void Initialize()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        totalHealth.HealthTotal = maxHealth;
        transform.position = standbySpawn;
        backForthEdge = new Vector3(gameSpawnPoint.x + xDistanceFromOrigin, gameSpawnPoint.y, gameSpawnPoint.z);
        startTime = Time.time;
        backForthJourneyLength = Vector3.Distance(gameSpawnPoint, backForthEdge);
    }
}