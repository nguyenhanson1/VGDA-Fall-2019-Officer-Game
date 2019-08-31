using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Object References
    private Rigidbody rb;

    // Flying checkers & defaults
    [SerializeField] private bool isFlying = false;
    private bool flying = false;
    private float oDrag;
    private float oADrag;

    // Set up Enemy Object first
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        oDrag = rb.drag;
        oADrag = rb.angularDrag;
    }

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (isFlying && !flying) {
            rb.useGravity = false;
            rb.drag = 0;
            rb.angularDrag = 0;
            flying = true;
        }
        else if (!isFlying && flying){
            rb.useGravity = true;
            rb.drag = oDrag;
            rb.angularDrag = oADrag;
            flying = false;
        }
    }
}
