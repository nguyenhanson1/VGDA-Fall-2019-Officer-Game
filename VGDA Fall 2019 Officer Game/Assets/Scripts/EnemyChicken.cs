using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChicken : Enemy
{
    // Flying checkers & defaults
    [SerializeField] private bool isFlying = false;
    private float oDrag;
    private float oADrag;

    // Prevention of chaging the same values
    private bool flying = false;
    private Transform lastPlayerTransform;

    private float rotationalDamp = 0.5f
;

    protected override void Move(){
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }

    protected void Turn() {
        Vector3 pos = Player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
    }

    protected override void Attack()
    {
        

    }

    

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //lastplayertransform = player.transform;
        //odrag = rb.drag;
        //oadrag = rb.angulardrag;
        Initialize();
        isFlying = true;
    }

    // Update is called once per frame
    void Update()
    {
        /**if (Player.transform != lastPlayerTransform)
            transform.LookAt(Player.transform);**/

        if (isFlying && !flying)
        {
            rb.useGravity = false;
            rb.drag = 0;
            rb.angularDrag = 0;
            flying = true;
        }
        else if (!isFlying && flying)
        {
            rb.useGravity = true;
            rb.drag = oDrag;
            rb.angularDrag = oADrag;
            flying = false;
        }

        Turn();
        Move();
    }

    protected virtual bool PlayerInFront()
    {
        Vector3 directionToTarget = transform.position - Player.transform.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);

        // if in range
        if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
        {
            Debug.DrawLine(transform.position, Player.transform.position, Color.cyan);
            return true;
        }
        Debug.DrawLine(transform.position, Player.transform.position, Color.yellow);
        return false;
    }

    protected virtual bool InLineOfSight()
    {
        RaycastHit hit;
        return false;
    }
}
