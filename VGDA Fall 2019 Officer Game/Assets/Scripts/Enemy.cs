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

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start() {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void begoned() {
        if (health <= 0f) {
            // Death animation here
            Destroy(this);
        }
    }
}
    

