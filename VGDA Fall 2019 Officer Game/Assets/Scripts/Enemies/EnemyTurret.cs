using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Enemy
{
    [SerializeField] private GameObject player;
    [SerializeField] private ObjectPooler bulletPool;
    [SerializeField] private float rotateSpeed = 50f;
    [SerializeField] private float time = 0.1f;

    private Attack attack = new Attack();

    protected override void OnEnable()
    {
        GameManager.UpdateOccurred += Move;
        GameManager.StartOccurred += Attack;
    }
    protected override void OnDisable()
    {
        GameManager.UpdateOccurred -= Move;
        GameManager.StartOccurred += Attack;
    }

    protected override void Attack()
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
        transform.LookAt(player.transform);
    }
}
