using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Enemy
{
    [Header("Turret Enemy")]
    [SerializeField] private GameObject player;
    [SerializeField] private ObjectPooler bulletPool;
    [Header("Movement")]
    [Tooltip("Speed that Turret rotates")]
    [SerializeField] private float rotateSpeed = 50f;
    [Header("Attack")]
    [SerializeField] Rigidbody targetRB = null;
    [Tooltip("Time between Turret shots")]
    [SerializeField] private float fireDelay = 0.1f;
    [Tooltip("Magnitude bullets miss target")]
    [SerializeField] private float scatterMagnitude = 1f;
    private Attack attack = new Attack();

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
        yield return new WaitForSeconds(fireDelay);
        if (FindObjectOfType<AudioManager>() != null)
            FindObjectOfType<AudioManager>().PlaySound("EnemyShoot");
        attack.ScatterShoot(gameObject, bulletPool, scatterMagnitude);
        StartCoroutine(Fire());
    }

    protected override void Move()
    {
        Vector3 playerFuturePos = Attack.leadShotPos(transform.position, bulletPool.bullet.Speed, player.transform.position, targetRB.velocity);
        transform.LookAt(playerFuturePos);
    }
}
