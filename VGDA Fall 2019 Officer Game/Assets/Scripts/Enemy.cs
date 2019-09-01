using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackFrequency;

    [HideInInspector] protected GameObject Player;
    [HideInInspector] protected Animator anim;
    [HideInInspector] protected Rigidbody rb;

    protected abstract void attack();
    protected abstract void move();

    protected virtual void initialize() {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void begoned() {
        if (health <= 0f) {
            // Death animation here
            Destroy(this);
        }
    }
}
    

