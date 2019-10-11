using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("Velocity of the Bullet")]
    [SerializeField] private float speed = 1f;
    [Tooltip("Damage dealt by the Bullet")]
    [SerializeField] protected int damage = 5;
    [Tooltip("How many units Bullet travels before disappearing")]
    [SerializeField] private float distance = 5f;
    [Tooltip("Rigidbody of Bullet")]
    [SerializeField] private Rigidbody rb = null;

    [Tooltip("Debug Testing")]
    public bool lockOn = false;

    //Properties that give access to the following variables
    public float Speed
    {
        get => speed;
    }
    public float Distance
    {
        get => distance;
    }
    public float DespawnTime
    {
        get => distance/speed;
    }

    //Set the bullet's speed and direction when it's created, 
    private void OnEnable()
    {
        rb.velocity = speed * transform.forward;
        //Start timer to deactivate the bullet
        StartCoroutine(trackBullet());
    }
    //Reset the bullet's speed when it's deactivated
    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
    }

    //Deactivate the bullet after a certain time
    private IEnumerator trackBullet()
    {
        yield return new WaitForSeconds(distance/speed);

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider col)
    {
        //Check if the gameObject's layer is in the Owner's Faction
        if (col.gameObject.GetComponent<IDamagable>() != null)
            if (col.gameObject.GetComponent<IDamagable>().health.myFaction == Factions.Faction.Evil)
            {
                Debug.Log("Hit");
                col.gameObject.GetComponent<IDamagable>().health.subtractHealth(1);
                //gameObject.SetActive(false);
            }
    }
}
