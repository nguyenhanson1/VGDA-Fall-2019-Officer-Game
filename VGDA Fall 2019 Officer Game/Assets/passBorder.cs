using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passBorder : MonoBehaviour
{
    public bool outside = false;
    public GameObject warning;
    public PlayerInteract health;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!outside)
            {
                warning.SetActive(true);
                //StartCoroutine("TakeDamage");
                outside = true;
            }
            else
            {
                warning.SetActive(false);
                //StopCoroutine("TakeDamage");
                outside = false;
            }
        }
            
    }
    private IEnumerable TakeDamage()
    {
        health.takeDamage(1);
        yield return new WaitForSeconds(2);
    }
}
