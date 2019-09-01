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

    protected override void move()
    {

    }

    protected override void attack()
    {
        
    }

    private void Awake()
    {
        oDrag = rb.drag;
        oADrag = rb.angularDrag;
    }
    // Start is called before the first frame update
    void Start()
    {
        lastPlayerTransform = Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform != lastPlayerTransform)
            transform.LookAt(Player.transform);

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
    }
}
