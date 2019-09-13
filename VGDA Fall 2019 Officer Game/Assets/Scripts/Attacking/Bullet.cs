using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] protected int damage = 5;
    [SerializeField] private float despawnTime = 5f;
    [SerializeField] private string target = "Enemy";

    [SerializeField] private Rigidbody rb = null;



    private void OnEnable()
    {
        rb.velocity = speed * transform.forward;
        StartCoroutine(trackBullet());
    }

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
    }

    private IEnumerator trackBullet()
    {
        yield return new WaitForSeconds(despawnTime);

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == target)
        {
            Debug.Log("Hit");
            col.gameObject.GetComponent<EnemyChicken>().totalHealth.subtractHealth(damage); // change this later to work with everyone
            gameObject.SetActive(false);
        }
    }
}
