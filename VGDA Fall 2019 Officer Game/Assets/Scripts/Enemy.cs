using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Health totalHealth;
    [SerializeField] protected int health;
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
        totalHealth.HealthTotal = health;
    }
}
    

