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
}
    

