using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackFrequency;
    [SerializeField] protected float moveSpeed;

    [HideInInspector] protected GameObject Player;
    [HideInInspector] protected Animator anim;
    [HideInInspector] protected Rigidbody rb;

    protected abstract void Attack();
    protected abstract void Move();

    protected virtual void Initialize() {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Begoned() {
        if (health <= 0f) {
            // Death animation here
            Destroy(this);
        }
    }

    // Checks if the player is in front so they can be fired at
    protected virtual bool AimAtPlayer() {
        Vector3 directionToTarget = transform.position - Player.transform.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);

        // if in range
        if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270) {
            Debug.DrawLine(transform.position, Player.transform.position, Color.cyan);
            return true;
        }
        Debug.DrawLine(transform.position, Player.transform.position, Color.yellow);
        return false;
    }
}
    

