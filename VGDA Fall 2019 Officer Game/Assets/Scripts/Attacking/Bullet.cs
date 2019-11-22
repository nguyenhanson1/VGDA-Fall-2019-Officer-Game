using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("Checks if bulllet is moving forward")]
    [SerializeField] private bool lerp = true;
    [Tooltip("Velocity of the Bullet")]
    [SerializeField] private float speed = 1f;
    [Tooltip("Damage dealt by the Bullet")]
    [SerializeField] protected float damage = 5;
    [Tooltip("How many units Bullet travels before disappearing")]
    [SerializeField] private float distance = 5f;
    [Tooltip("Rigidbody of Bullet")]
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private Factions.Faction factionTarget;

    [Tooltip("Debug Testing")]
    public bool lockOn = false;

    private Vector3 lerpDestination;

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
    public bool Lerping(bool? input = null, Vector3? destination = null)
    {
        if(input.HasValue)
        {
            lerp = input.Value;
            if (lerp == true)
            {
                GameManager.UpdateOccurred += LerpMove;
                lerpDestination = destination.Value;
            }
            else
            {
                Debug.Log("Stopped'em");
                GameManager.UpdateOccurred -= LerpMove;
                rb.velocity = speed * transform.forward;
            } 
            
        }

        return lerp;
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
        GameManager.UpdateOccurred -= LerpMove;
    }

    //Deactivate the bullet after a certain time
    private IEnumerator trackBullet()
    {
        yield return new WaitForSeconds(distance/speed);

        gameObject.SetActive(false);
    }

    private void LerpMove()
    {
        rb.velocity = Vector3.zero;
        Vector3 travel = Vector3.Lerp(transform.position, lerpDestination, speed * Time.deltaTime);
        transform.position = travel;
        Debug.Log(Vector3.Distance(lerpDestination, transform.position));

        if (Vector3.Distance(lerpDestination, transform.position) <= 0.1f)
        {
            Lerping(false);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        //Check if the gameObject's layer is in the Owner's Faction
        if (col.gameObject.GetComponent<IDamagable>() != null)
        {
            Debug.Log("Hit");
            if (col.gameObject.GetComponent<IDamagable>().myFaction == factionTarget)
            {
                Debug.Log("Damage");
                col.gameObject.GetComponent<IDamagable>().health.subtractHealth(1);
                gameObject.SetActive(false);

                if (FindObjectOfType<AudioManager>() != null)
                {
                    if(col.GetComponent<IDamagable>().health.HealthTotal <= 0)
                        FindObjectOfType<AudioManager>().PlaySound("ChickenDeath");
                    else if(col.GetComponent<IDamagable>().myFaction == Factions.Faction.Evil)
                        FindObjectOfType<AudioManager>().PlaySound("EnemyHurt");
                    else if(col.GetComponent<IDamagable>().myFaction == Factions.Faction.Good)
                        FindObjectOfType<AudioManager>().PlaySound("PlayerHurt");
                }
                    
            }
            else if (col.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                gameObject.SetActive(false);
            }
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Debug.Log("Something hit");
            gameObject.SetActive(false);
        }

    }
}
