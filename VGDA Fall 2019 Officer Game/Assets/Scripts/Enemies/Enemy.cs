using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamagable
{

    //Interface property that access allowws other scripts access the health attribute
    Health IDamagable.health { get => totalHealth;}
    public Factions.Faction myFaction { get => Factions.Faction.Evil; }

    public Health totalHealth = new Health();
    [SerializeField] protected int maxHealth = 15;
    //[SerializeField] protected float damage;
    //[SerializeField] protected float attackFrequency;
    [SerializeField] protected float moveSpeed;

    protected GameObject Player;
    protected Animator anim;
    protected Rigidbody rb;

    //[SerializeField] protected Vector3 standbySpawn;
    //[SerializeField] protected Vector3 gameSpawnPoint;
    protected bool inited = false;

    protected abstract void EnemyAttack();
    protected abstract void Move();
    protected virtual void Begoned(Health h) {
        if (totalHealth.HealthTotal <= 0)
        { // totalHealth == h
            // Play Death Animation
            Score.score = Score.score + 1;
            Destroy(gameObject);
        }
    }
    protected virtual void SpawnEnemyInGame()
    {
        //transform.position = gameSpawnPoint;
        inited = true;
    }
    protected virtual void OnEnable()
    {
        Health.OnDeath += Begoned;
        Initialize();
    }

    protected virtual void OnDisable()
    {
        Health.OnDeath -= Begoned;
        GameManager.StartOccurred -= Initialize;
    }
    protected void Awake()
    {
        totalHealth.myFaction = Factions.Faction.Evil;
    }

    protected virtual void Initialize() {
        Player = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
        totalHealth.HealthTotal = maxHealth;
        //transform.position = standbySpawn;
    }
}
    

