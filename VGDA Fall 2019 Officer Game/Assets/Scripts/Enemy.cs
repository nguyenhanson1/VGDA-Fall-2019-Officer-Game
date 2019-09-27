using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Health totalHealth = new Health();
    [SerializeField] protected int health = 15;
    [SerializeField] protected float damage;
    [SerializeField] protected float attackFrequency;
    [SerializeField] protected float moveSpeed;

    [HideInInspector] protected GameObject Player;
    [HideInInspector] protected Animator anim;
    [HideInInspector] protected Rigidbody rb;

    protected abstract void Attack();
    protected abstract void Move();
    protected abstract void Begoned(Health h);

    protected virtual void OnEnable()
    {
        Health.OnDeath += Begoned;
    }

    protected virtual void OnDisable()
    {
        Health.OnDeath -= Begoned;
    }

    protected void Awake()
    {
        totalHealth.myFaction = Factions.Faction.Evil;
    }

    protected virtual void Initialize() {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        totalHealth.HealthTotal = health;
    }



}
    

