using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int healthToSet;

    private Health shipHealth = new Health() ;

    private void Awake()
    {
        shipHealth.HealthTotal = healthToSet;
    }

    private void OnEnable()
    {
        //Health.OnDeath += DestroyShip;
    }
    private void OnDisable()
    {
        //Health.OnDeath -= DestroyShip;
    }
    private void DestroyShip(Health ship)
    {
        if(shipHealth == ship)
        {
            Destroy(this.gameObject);
        }
    }
}
