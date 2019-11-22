using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DisplayHealth : MonoBehaviour
{
    private Image healthBar;
    [SerializeField] private PlayerInteract player;

    void updateHealthBar()
    {
        Debug.Log("CurrentHP: " + player.displayHealth + " MaxHP: " + player.maxHealth);
        if (player.displayHealth >= 1)
            healthBar.fillAmount = (float)player.displayHealth / player.maxHealth;
        else
            healthBar.fillAmount = 0;
    }
    private void Awake()
    {
        healthBar = GetComponent<Image>();
    }

    private void OnEnable()
    {
        GameManager.UpdateOccurred += updateHealthBar;
    }
    private void OnDisable()
    {
        GameManager.UpdateOccurred -= updateHealthBar;
    }
    /*
    private void DestroyShip(Health ship)
    {
        if(shipHealth == ship)
        {
            SceneManager.LoadScene("EndMenu");
        }
    }
    */
}
