using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    private int healthToSet;
    private Image healthBar;

    private Health shipHealth = new Health() ;


    void updateHealthBar()
    {
        healthBar.fillAmount = (float)shipHealth.HealthTotal/healthToSet;
    }
    private void Awake()
    {
        shipHealth.HealthTotal = healthToSet;
        healthBar = GetComponent<Image>();
    }

    private void OnEnable()
    {
        Health.OnDeath += DestroyShip;
        GameManager.UpdateOccurred += updateHealthBar;
    }
    private void OnDisable()
    {
        Health.OnDeath -= DestroyShip;
        GameManager.UpdateOccurred -= updateHealthBar;
    }

    private void DestroyShip(Health ship)
    {
        if(shipHealth == ship)
        {
            Destroy(player);
        }
    }
}
