using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : Enemy
{
    [Header("Flying Enemy")]
    [SerializeField] private Transform target = null;
    [SerializeField] private ObjectPooler bulletPool = null;
    private Attack attack = new Attack();
    [Header("Attack")]
    [Tooltip("Seconds between bullets")]
    [SerializeField] private float attackDelay = 2f;
    [Tooltip("Magnitude that bullets fly away from target")]
    [SerializeField] private float scatterMagnitude = 3f;
    [Header("Movement Speed")]
    [Tooltip("Speed when Enemy goes out of range")]
    [SerializeField] private float rotateSpeed;
    [Tooltip("Speed when Enemy goes out of range")]
    [SerializeField] private float uTurnRotate;
    [Tooltip("Speed when Enemy is turning around to target")]
    [SerializeField] private float uTurnSpeed;
    [Tooltip("Speed when Enemy is flying away from target")]
    [SerializeField] private float boostSpeed;
    [Header("Range")]
    [Tooltip("Time Enemy has to shoot at target and make a U-turn")]
    [SerializeField] private float aiDelay = 2f;
    [Tooltip("Distance from target before Enemy makes a U-turn")]
    [SerializeField] private float tooFar = 400f;
    [Tooltip("Distance from target before Enemy starts shooting")]
    [SerializeField] private float tooClose = 100f;
    [Header("Movement")]
    [SerializeField] private Rigidbody targetRB = null;
    private Vector3 prevPos = Vector3.zero;
    private Vector3 currentPos = Vector3.zero;
    private Vector3 calcVel = Vector3.zero;
    [Header("Eyes on Target")]
    [Tooltip("Whether or not obstacle is in front of Enemy")]
    [SerializeField] private bool lineOfSight = true;
    [Tooltip("Layers that Enemy will avoid")]
    [SerializeField] private LayerMask obstacleLayers = new LayerMask();
    [Tooltip("How far Enemy can detect Layers")]
    [SerializeField] private float fieldOfView = 200f;
    [Tooltip("Whether or not Enemy has line of sight on Target")]
    [SerializeField] private bool eyesOnTarget = false;
    [Tooltip("Layer of the Target")]
    [SerializeField] private LayerMask targetLayer = new LayerMask();
    [Header("AI")]
    [SerializeField] private AI path;

    private enum AI
    {
        OpenFire = -2,
        Bail = -1,
        OutofBounds = 0,
        Strike = 1
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        GameManager.UpdateOccurred += EyesOnTarget;
        GameManager.UpdateOccurred += Move;
        GameManager.StartOccurred += EnemyAttack;
        GameManager.UpdateOccurred += LineOfSight;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        GameManager.UpdateOccurred -= EyesOnTarget;
        GameManager.UpdateOccurred -= Move;
        GameManager.StartOccurred -= EnemyAttack;
        GameManager.UpdateOccurred -= LineOfSight;
    }

    protected override void Initialize()
    {
        base.Initialize();
        StartCoroutine(AIManager());
    }

    private IEnumerator AIManager()
    {
        if (path == AI.Strike)
        {
            yield return new WaitUntil(() => eyesOnTarget && (target.position - transform.position).magnitude <= tooClose);
        }
        path = AI.OpenFire;
        yield return new WaitForSeconds(aiDelay);
        path = AI.Bail;
        yield return new WaitUntil(() => (target.position - transform.position).magnitude >= tooFar);
        path = AI.OutofBounds;
        yield return new WaitForSeconds(aiDelay);
        path = AI.Strike;
        StartCoroutine(AIManager());
    }

    private void EyesOnTarget()
    {
        if (!lineOfSight)
        {
            Vector3 rotateTo = Vector3.RotateTowards(transform.forward, transform.up, rotateSpeed * Time.deltaTime, 0.0f);
            gameObject.transform.rotation = Quaternion.LookRotation(rotateTo);
        }
        else if (path == AI.OutofBounds)
        {
            Vector3 targetDir = target.position - transform.position;
            Vector3 rotateTo = Vector3.RotateTowards(transform.forward, targetDir, uTurnRotate *Time.deltaTime, 0.0f);
            gameObject.transform.rotation = Quaternion.LookRotation(rotateTo);
        }
        else if(path == AI.Strike || path == AI.OpenFire && lineOfSight)
        {
            Vector3 targetVelocity = targetRB.velocity;
            Vector3 targetFuturePos = Attack.leadShotPos(transform.position, bulletPool.bullet.Speed, target.position, targetVelocity);

            Vector3 targetDir = targetFuturePos - transform.position;
            Vector3 rotateTo = Vector3.RotateTowards(transform.forward, targetDir, rotateSpeed * Time.deltaTime, 0.0f);
            if(!Physics.Raycast(transform.position, rotateTo, fieldOfView, obstacleLayers, QueryTriggerInteraction.Collide))
                gameObject.transform.rotation = Quaternion.LookRotation(rotateTo);

            Debug.DrawLine(transform.position, transform.position + rotateTo * fieldOfView);
        }
    }

    private Quaternion LeadShotRotation()
    {
        Vector3 targetDir = TargetDirection();
        Vector3 rotateTo = Vector3.RotateTowards(transform.forward, targetDir, 100f, 0.0f);

        return Quaternion.LookRotation(rotateTo);
    }
    private Vector3 TargetDirection()
    {
        Vector3 targetVelocity = targetRB.velocity;
        Vector3 targetFuturePos = Attack.leadShotPos(transform.position, bulletPool.bullet.Speed, target.position, targetVelocity);
        return targetFuturePos - transform.position;
    }

    private void LineOfSight()
    {
        Debug.Log("Running");

        lineOfSight = (Physics.Raycast(transform.position, transform.forward, fieldOfView, obstacleLayers, QueryTriggerInteraction.Collide)) ? false : true;

        if ((Physics.Raycast(transform.position, TargetDirection(), out RaycastHit frontalTarget, fieldOfView, targetLayer + obstacleLayers, QueryTriggerInteraction.Collide)))
        {
            if (targetLayer == (targetLayer | 1 <<frontalTarget.collider.gameObject.layer))
            {
                eyesOnTarget = true;
            }
            else
                eyesOnTarget = false;
        }
        else
            eyesOnTarget = false;
    }

    protected override void EnemyAttack()
    {
        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {

        yield return new WaitForSeconds(attackDelay);
        if (path == AI.OpenFire)
        {
            
            attack.ScatterShoot(gameObject, bulletPool, scatterMagnitude, LeadShotRotation());
        }
        StartCoroutine(Fire());
    }

    protected override void Move()
    {
        if (path == AI.Bail)
            rb.velocity = transform.forward * boostSpeed;
        else if (path == AI.OutofBounds)
            rb.velocity = transform.forward * uTurnSpeed;
        else if (path == AI.Strike)
            rb.velocity = transform.forward * moveSpeed;
    }
}
