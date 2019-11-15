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
        GameManager.StartOccurred += Attack;
        Health.OnDeath += Begoned;
    }
    protected override void OnDisable()
    {
        GameManager.StartOccurred -= Initialize;
        GameManager.UpdateOccurred -= Move;
        GameManager.StartOccurred += Attack;
        Health.OnDeath -= Begoned;
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
        prevPos = currentPos;
        currentPos = player.transform.position;
        calcVel = (currentPos - prevPos) / Time.deltaTime;

        Vector3 playerVelocity = player.transform.forward * ((addForce.speed- offSpeed) / Time.deltaTime);
        Vector3 playerFuturePos = leadShotPos(bulletPool.bullet.Speed, player.transform.position, calcVel);
        transform.LookAt(playerFuturePos);
    }

    private Vector3 leadShotPos(float bulletSpeed, Vector3 targetPos, Vector3 targetVel)
    {
        //Get relative position
        Vector3 targetRelativePos = targetPos - transform.position;

        //Get time
        float time;

        float acceleration = targetVel.sqrMagnitude - Mathf.Pow(bulletSpeed, 2);
        if (Mathf.Abs(acceleration) < 0.001f)
        {
            float t = -targetRelativePos.sqrMagnitude / (2f * Vector3.Dot(targetVel, targetRelativePos));
            time = Mathf.Max(t, 0f); //don't shoot back in time
        }
        else
        {
            float b = 2f * Vector3.Dot(targetVel, targetRelativePos);
            float c = targetRelativePos.sqrMagnitude;
            float determinant = b * b - 4f * acceleration * c;

            if (determinant > 0f)
            { //determinant > 0; two intercept paths (most common)
                float t1 = (-b + Mathf.Sqrt(determinant)) / (2f * acceleration),
                        t2 = (-b - Mathf.Sqrt(determinant)) / (2f * acceleration);
                if (t1 > 0f)
                {
                    if (t2 > 0f)
                        time = Mathf.Min(t1, t2); //both are positive
                    else
                        time = t1; //only t1 is positive
                }
                else
                    time = Mathf.Max(t2, 0f); //don't shoot back in time
            }
            else if (determinant < 0f) //determinant < 0; no intercept path
                time = 0f;
            else //determinant = 0; one intercept path, pretty much never happens
                time = Mathf.Max(-b / (2f * acceleration), 0f); //don't shoot back in time
        }


        //Return results
        return targetPos + (time * targetVel);
    }

}
