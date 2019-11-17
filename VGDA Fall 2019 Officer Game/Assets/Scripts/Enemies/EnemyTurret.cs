using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Enemy
{
    [SerializeField] private GameObject player;
    [SerializeField] private ObjectPooler bulletPool;
    [SerializeField] private Addforce addForce;
    [SerializeField] private float rotateSpeed = 50f;
    [SerializeField] private float time = 0.1f;
    [SerializeField] private float offSpeed = 0f;
    public int health;
    private Attack attack = new Attack();

    [Header("Position and Velocity Test")]
    [SerializeField] private Vector3 prevPos;
    [SerializeField] private Vector3 currentPos;
    [SerializeField] private Vector3 calcVel;

    private void Update()
    {
        health = totalHealth.HealthTotal;
    }

    protected override void OnEnable()
    {
        GameManager.StartOccurred += Initialize;
        GameManager.UpdateOccurred += Move;
        GameManager.StartOccurred += EnemyAttack;
        Health.OnDeath += Begoned;
    }
    protected override void OnDisable()
    {
        GameManager.StartOccurred -= Initialize;
        GameManager.UpdateOccurred -= Move;
        GameManager.StartOccurred -= EnemyAttack;
        Health.OnDeath -= Begoned;
    }

    protected override void EnemyAttack()
    {
        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(time);
        attack.Shoot(gameObject, bulletPool);
        StartCoroutine(Fire());
    }

    protected override void Move()
    {
        prevPos = currentPos;
        currentPos = player.transform.position;
        calcVel = (currentPos - prevPos) / Time.deltaTime;

        Vector3 playerVelocity = player.transform.forward * ((addForce.speed- offSpeed) / Time.deltaTime);
        Vector3 playerFuturePos = Attack.leadShotPos(transform.position, bulletPool.bullet.Speed, player.transform.position, calcVel);
        transform.LookAt(playerFuturePos);
    }
}
