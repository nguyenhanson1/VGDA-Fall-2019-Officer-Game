using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : Enemy
{
    protected enum PathOptions {
        BackForth,
        Circular,
        Random,
        FollowPlayer
    }

    [SerializeField]protected PathOptions Path;

    [Tooltip("The furthest distance the enemy flies (Linear) or the radius of the path (Circular)")]
    [SerializeField] protected float xDistanceFromOrigin = 10f;
    private bool backForthTurned = false;
    private Vector3 backForthEdge;
    private float backForthJourneyLength;
    private float startTime;
    private Transform reference;

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
        float travelled = (Time.time - startTime) * moveSpeed;
        float fractionTravelled = travelled / backForthJourneyLength;
        if (!backForthTurned)
        {
            smoothLookAt(backForthEdge);
            transform.position = Vector3.Lerp(gameSpawnPoint, backForthEdge, fractionTravelled);
            if (transform.position.Equals(backForthEdge))
            {
                startTime = Time.time;
                backForthTurned = true;
            }
        }
        else {
            smoothLookAt(gameSpawnPoint);
            transform.position = Vector3.Lerp(backForthEdge, gameSpawnPoint, fractionTravelled);
            if (transform.position.Equals(gameSpawnPoint))
            {
                startTime = Time.time;
                backForthTurned = false;
            }
        }
    }

    protected override void Initialize()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        totalHealth.HealthTotal = maxHealth;
        transform.position = gameSpawnPoint;

        if (Path == PathOptions.BackForth)
        {
            backForthEdge = new Vector3(gameSpawnPoint.x + xDistanceFromOrigin, gameSpawnPoint.y, gameSpawnPoint.z);
            transform.LookAt(backForthEdge);
            startTime = Time.time;
            backForthJourneyLength = Vector3.Distance(gameSpawnPoint, backForthEdge);
        }
        else if (Path == PathOptions.Circular) {
        }
    }

    protected void smoothLookAt(Vector3 target) {
        Quaternion tRotation = Quaternion.LookRotation(target - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, tRotation, moveSpeed * Time.deltaTime/3);
    }
}
